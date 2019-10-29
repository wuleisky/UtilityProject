using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UtilityResolution.Utility
{
    public static class Extenstion
    {
        static List<ShowLoadingContext> list = new List<ShowLoadingContext>();

        static void OnFrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Loaded -= OnFrameworkElementLoaded;
            ShowLoadingContext p = null;
            lock (list)
            {
                foreach (var item in list)
                {
                    if (item.Target == sender)
                    {
                        p = item;
                        break;
                    }
                }

                if (p != null)
                {
                    list.Remove(p);
                }
            }

            if (p != null)
            {
                p.Target.ShowLoading(p.percent, p.loadingName, p.LoadingSize, p.HorizontalAlignment, p.VerticalAlignment, p.Margin, p.Size);
            }

        }

        public static void ShowLoading(this UIElement element, string Percent = "", string LoadingName = "")
        {
            ShowLoading(element, Percent, LoadingName, 60, HorizontalAlignment.Center, VerticalAlignment.Center, null, null, false, Brushes.Gray, null);
        }

        //public static void ShowLoading(this UIElement element, bool isAutoClose=false)
        //{
        //    ShowLoading(element, string.Empty, string.Empty, 60, HorizontalAlignment.Center, VerticalAlignment.Center, null, null, false, Brushes.Gray, null);
        //    Task.Factory.StartNew(() =>
        //    {
        //        Task.Delay(10 * 1000);
        //        if (element == null || element.Dispatcher == null) return;
        //        element.Dispatcher.BeginInvoke(new Action(() => { element.HideLoading(); }));
        //    });
        //}

        //public static void UpdateLoading(this UIElement element, string Percent = "", string LoadingName = "")
        //{
        //    ShowLoading(element, Percent, LoadingName, 60, HorizontalAlignment.Center, VerticalAlignment.Center, null, null, false, Brushes.Gray, null);
        //}

        public static void HideLoading(this UIElement element, bool disableElement = false)
        {

            if (element == null || element.Dispatcher == null) return;
            if (element.Dispatcher.CheckAccess())
            {
                element.IsEnabled = !disableElement;
                element.IsHitTestVisible = true;
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
                if (layer == null)
                {
                    var win = element as Window;
                    if (win != null && win.Content != null && win.Content is UIElement)
                    {
                        element = win.Content as UIElement;
                        layer = AdornerLayer.GetAdornerLayer(element);
                    }
                    else if (element is FrameworkElement)
                    {
                        FrameworkElement fe = element as FrameworkElement;
                        if (!fe.IsLoaded)
                        {
                            lock (list)
                            {
                                ShowLoadingContext target = null;
                                foreach (var p in list)
                                {
                                    if (p.Target == fe)
                                    {
                                        target = p;
                                        break;
                                    }
                                }

                                if (target != null)
                                {
                                    list.Remove(target);
                                }
                            }

                            fe.Loaded -= OnFrameworkElementLoaded;
                            return;
                        }
                    }
                }

                if (layer != null)
                {
                    LoadingAdorner target = null;
                    var items = layer.GetAdorners(element);
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            if (item is LoadingAdorner)
                            {
                                target = item as LoadingAdorner;
                                if (target != null)
                                {
                                    layer.Remove(target);
                                    target.Dispose();
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                Action<UIElement, Boolean> action = HideLoading;
                element.Dispatcher.BeginInvoke(action, element, disableElement);
            }
        }

        public static void ShowLoading(this UIElement element,
            string Percent,
            string LoadingName,
            double loadingSize = 60,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            Thickness? margin = null,
            Size? size = null,
            bool disableElement = false,
            Brush background = null,
            Brush foreground = null)
        {
            element.IsEnabled = !disableElement;

            if (element == null || element.Dispatcher == null) return;
            if (element.Dispatcher.CheckAccess())
            {

                AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
                if (layer == null)
                {
                    var win = element as Window;
                    element.IsHitTestVisible = false;
                    if (win != null && win.Content != null && win.Content is UIElement)
                    {
                        element = win.Content as UIElement;
                        //var tag = ((Grid) element).Tag as UIElement;
                        //if (tag != null)
                        //{
                        //    layer = AdornerLayer.GetAdornerLayer(tag);
                        //    element = tag;
                        //}
                        //else
                        //{
                        //    layer = AdornerLayer.GetAdornerLayer(element);
                        //}
                        layer = AdornerLayer.GetAdornerLayer(element);
                    }
                    else if (element is FrameworkElement)
                    {
                        FrameworkElement fe = element as FrameworkElement;
                        if (!fe.IsLoaded)
                        {
                            //此处用匿名委托的好处是ShowLoading的参数可以直接传到匿名委托中，但是却没有办法在HideLoading时注销此匿名委托
                            //在控件未 load 之前调用 ShowLoading 再立刻调用HideLoading时 就会发现控件在Loaded之后会显示遮罩,却无法消失
                            //RoutedEventHandler handler = null;
                            //handler = (s, e) =>
                            //{
                            //    fe.Loaded -= handler;
                            //    (s as FrameworkElement).ShowLoading(loadingSize, horizontalAlignment, verticalAlignment, margin, size);
                            //};
                            //fe.Loaded += handler;

                            ShowLoadingContext p = new ShowLoadingContext {Target = fe, LoadingSize = loadingSize, HorizontalAlignment = horizontalAlignment, VerticalAlignment = verticalAlignment, Margin = margin, Size = size, percent = Percent, loadingName = LoadingName};
                            lock (list)
                            {
                                var exist = false;
                                foreach (var item in list)
                                {
                                    if (item.Target == fe)
                                    {
                                        item.LoadingSize = loadingSize;
                                        item.HorizontalAlignment = horizontalAlignment;
                                        item.VerticalAlignment = verticalAlignment;
                                        item.Margin = margin;
                                        item.Size = size;
                                        exist = true;
                                        break;
                                    }
                                }

                                if (!exist)
                                {
                                    list.Add(p);
                                    fe.Loaded += OnFrameworkElementLoaded;
                                }
                            }

                            return;
                        }
                    }
                }

                if (layer != null)
                {
                    var ads = layer.GetAdorners(element);
                    bool hasFound = false;
                    if (ads != null)
                    {
                        foreach (var ad in ads)
                        {
                            if (ad is LoadingAdorner)
                            {
                                var loading = ad as LoadingAdorner;
                                loading.UpdateLoading(Percent, LoadingName);
                                hasFound = true;
                            }
                        }
                    }

                    if (!hasFound)
                    {
                        layer.Add(new LoadingAdorner(element, Percent, LoadingName)
                        {
                            LoadingSize = loadingSize,
                            LoadingVerticalAlignment = verticalAlignment,
                            LoadingHorizontalAlignment = horizontalAlignment,
                            LoadingMargin = margin ?? new Thickness(0, 0, 0, 0),
                            Size = size ?? Size.Empty,
                            Foreground = foreground,
                            Background = background,
                        });
                    }
                }
            }
            else
            {
                Action<UIElement, string, string, Double, HorizontalAlignment, VerticalAlignment, Thickness?, Size?, bool, Brush, Brush> action = ShowLoading;
                element.Dispatcher.BeginInvoke(action, element, loadingSize, horizontalAlignment, verticalAlignment, margin, size, disableElement, background, foreground);
            }
        }

        public static BitmapSource ToBitmapSource(this UIElement element, Rect originrect, Rect destRect)
        {


            element.Measure(originrect.Size);
            element.Arrange(originrect);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int) System.Math.Round(destRect.Width), (int) System.Math.Round(destRect.Height), 96.0, 96.0, PixelFormats.Default);
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, destRect);
                VisualBrush brush = new VisualBrush(element);

                drawingContext.DrawRectangle(brush, null, destRect);
            }

            renderTargetBitmap.Render(drawingVisual);
            //renderTargetBitmap.Freeze();
            return renderTargetBitmap;
        }



        public class ShowLoadingContext
        {
            public FrameworkElement Target { get; set; }

            public double LoadingSize { get; set; }

            public HorizontalAlignment HorizontalAlignment { get; set; }

            public VerticalAlignment VerticalAlignment { get; set; }

            public Thickness? Margin { get; set; }

            public Size? Size { get; set; }

            public string percent { get; set; }
            public string loadingName { get; set; }

        }
    }
}

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;


namespace System.Windows.Controls
{
    public class LoadingAdorner : Adorner, IDisposable
    {
        private VisualCollection visualChildren;
        LoadingMask loading;

        private double _loadingSize = 0;
        public double LoadingSize
        {
            get
            {
                return _loadingSize;
            }
            set
            {
                this._loadingSize = value;
                this.loading.pr.Width = _loadingSize;
                this.loading.pr.Height = _loadingSize;
            }
        }

        public VerticalAlignment LoadingVerticalAlignment
        {
            get
            {
                return this.loading.pr.VerticalAlignment;
            }
            set
            {
                this.loading.pr.VerticalAlignment = value;
            }
        }

        public HorizontalAlignment LoadingHorizontalAlignment
        {
            get
            {
                return this.loading.pr.HorizontalAlignment;
            }
            set
            {
                this.loading.pr.HorizontalAlignment = value;
            }
        }

        public Thickness LoadingMargin
        {
            get
            {
                return this.loading.pr.Margin;
            }
            set
            {
                this.loading.pr.Margin = value;
            }
        }

        private Size _Size = Size.Empty;
        public Size Size
        {
            get
            {
                return _Size;
            }
            set
            {
                if (value == Size.Empty)
                {
                    Binding bd = new Binding("Width");
                    bd.Source = base.AdornedElement;
                    bd.Mode = BindingMode.OneWay;
                    loading.SetBinding(FrameworkElement.WidthProperty, bd);

                    Binding bd2 = new Binding("Height");
                    bd2.Source = base.AdornedElement;
                    bd2.Mode = BindingMode.OneWay;
                    loading.SetBinding(FrameworkElement.HeightProperty, bd2);
                }
                else
                {
                    loading.SetBinding(FrameworkElement.WidthProperty, new Binding());
                    loading.SetBinding(FrameworkElement.HeightProperty, new Binding());
                    loading.Width = value.Width;
                    loading.Height = value.Height;
                }
                _Size = value;
            }
        }

        private Brush _Background;
        public Brush Background
        {
            get { return _Background; }
            set
            {
                _Background = value;
                this.loading.Back = value;
            }
        }

        private Brush _Foreground;
        public Brush Foreground
        {
            get { return _Foreground; }
            set
            {

                if (value == null)
                {
                    value = new SolidColorBrush(Colors.Orange);
                }
                _Foreground = value;
                this.loading.Foreground = value;
            }
        }

        public LoadingAdorner(UIElement adornedElement,string Percent,string LoadingName)
            : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);
            loading = new LoadingMask();
            loading.UpdateLoadingInfo(Percent, LoadingName);

            Binding bd = new Binding("Width");
            bd.Source = adornedElement;
            bd.Mode = BindingMode.OneWay;
            loading.SetBinding(FrameworkElement.WidthProperty, bd);

            Binding bd2 = new Binding("Height");
            bd2.Source = adornedElement;
            bd2.Mode = BindingMode.OneWay;
            loading.SetBinding(FrameworkElement.HeightProperty, bd2);

            visualChildren.Add(loading);
        }

        public void UpdateLoading(string percent, string name)
        {
            loading.UpdateLoadingInfo(percent,name);
        }

        protected override int VisualChildrenCount
        {
            get { return visualChildren.Count; }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            loading.Arrange(new Rect(new Point(0, 0), finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return visualChildren[index];
        }

        public void Dispose()
        {
            if (loading != null)
            {
                loading.SetBinding(FrameworkElement.WidthProperty, new Binding());
                loading.SetBinding(FrameworkElement.HeightProperty, new Binding());
            }
        }
    }

}

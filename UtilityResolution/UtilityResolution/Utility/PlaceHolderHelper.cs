using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using UtilityResolution.Convert;

namespace UtilityResolution.Utility
{
    public class PlaceHolderHelper
    {
        #region PlaceHolder

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(PlaceHolderHelper),
                new UIPropertyMetadata(string.Empty, new PropertyChangedCallback(OnPlaceholderChanged)));


        public static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox)
            {
                PasswordBox txt = d as PasswordBox;

                if (txt == null || e.NewValue.ToString().Trim().Length == 0) return;

                RoutedEventHandler loadHandler = null;
                loadHandler = (s1, e1) =>
                {
                    txt.Loaded -= loadHandler;

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    Adorner[] ar = lay.GetAdorners(txt);
                    if (ar != null)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (ar[i] is PlaceholderAdorner)
                            {
                                lay.Remove(ar[i]);
                            }
                        }
                    }

                    if (txt.Password.Length == 0)
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));

                };
                txt.Loaded += loadHandler;
                txt.PasswordChanged += (s1, e1) =>
                {
                    bool isShow = txt.Password.Length == 0;

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    if (isShow)
                    {
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));
                    }
                    else
                    {
                        Adorner[] ar = lay.GetAdorners(txt);
                        if (ar != null)
                        {
                            for (int i = 0; i < ar.Length; i++)
                            {
                                if (ar[i] is PlaceholderAdorner)
                                {
                                    lay.Remove(ar[i]);
                                }
                            }
                        }
                    }
                };
            }
            else if (d is TextBox)
            {
                TextBox txt = d as TextBox;

                if (txt == null || e.NewValue.ToString().Trim().Length == 0) return;

                RoutedEventHandler loadHandler = null;
                loadHandler = (s1, e1) =>
                {

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null)
                    {
                        return;
                    }
                    else
                    {
                        txt.Loaded -= loadHandler;
                    }

                    Adorner[] ar = lay.GetAdorners(txt);
                    if (ar != null)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (ar[i] is PlaceholderAdorner)
                            {
                                lay.Remove(ar[i]);
                            }
                        }
                    }

                    if (txt.Text.Length == 0)
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));

                };
                txt.Loaded += loadHandler;
                txt.TextChanged += (s1, e1) =>
                {
                    bool isShow = txt.Text.Length == 0;

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    if (isShow)
                    {
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));
                    }
                    else
                    {
                        Adorner[] ar = lay.GetAdorners(txt);
                        if (ar != null)
                        {
                            for (int i = 0; i < ar.Length; i++)
                            {
                                if (ar[i] is PlaceholderAdorner)
                                {
                                    lay.Remove(ar[i]);
                                }
                            }
                        }
                    }
                };
             
            }
            else if (d is ComboBox)
            {
                ComboBox txt = d as ComboBox;

                if (txt == null || e.NewValue.ToString().Trim().Length == 0) return;

                RoutedEventHandler loadHandler = null;
                loadHandler = (s1, e1) =>
                {
                    txt.Loaded -= loadHandler;

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    Adorner[] ar = lay.GetAdorners(txt);
                    if (ar != null)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (ar[i] is PlaceholderAdorner)
                            {
                                lay.Remove(ar[i]);
                            }
                        }
                    }

                    if (txt.Text == null || txt.Text.Length == 0)
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));

                };
                txt.Loaded += loadHandler;

                RoutedEventHandler textChangedHandler = (s1, e1) =>
                {
                    bool isShow = txt.Text != null && txt.Text.Length == 0;

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    if (isShow)
                    {
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));
                    }
                    else
                    {
                        Adorner[] ar = lay.GetAdorners(txt);
                        if (ar != null)
                        {
                            for (int i = 0; i < ar.Length; i++)
                            {
                                if (ar[i] is PlaceholderAdorner)
                                {
                                    lay.Remove(ar[i]);
                                }
                            }
                        }
                    }
                };

                txt.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, textChangedHandler);
            }
            else if (d is RichTextBox)
            {
                RichTextBox txt = d as RichTextBox;
                if(e.NewValue==null)return;
                if (txt == null ||  e.NewValue.ToString().Trim().Length == 0) return;

                RoutedEventHandler loadHandler = null;
                loadHandler = (s1, e1) =>
                {
                    txt.Loaded -= loadHandler;

                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    Adorner[] ar = lay.GetAdorners(txt);
                    if (ar != null)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (ar[i] is PlaceholderAdorner)
                            {
                                lay.Remove(ar[i]);
                            }
                        }
                    }
                    
                    TextRange textRange = new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd);
                    if (txt.Document == null || string.IsNullOrWhiteSpace(textRange.Text))
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));

                };
                txt.Loaded += loadHandler;

                RoutedEventHandler textChangedHandler = (s1, e1) =>
                {
                   // bool isShow = txt.Document != null && txt.Document.Blocks.Count == 0;
                    TextRange textRange = new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd);
                   bool  isShow=string.IsNullOrWhiteSpace(textRange.Text);
                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    if (isShow)
                    {
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));
                    }
                    else
                    {
                        Adorner[] ar = lay.GetAdorners(txt);
                        if (ar != null)
                        {
                            for (int i = 0; i < ar.Length; i++)
                            {
                                if (ar[i] is PlaceholderAdorner)
                                {
                                    lay.Remove(ar[i]);
                                }
                            }
                        }
                    }
                };

                txt.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, textChangedHandler);

                RoutedEventHandler  getFocusedChangedHandler = (s1, e1) =>
                {
                    // bool isShow = txt.Document != null && txt.Document.Blocks.Count == 0;
                    TextRange textRange = new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd);
                    bool isShow = string.IsNullOrWhiteSpace(textRange.Text);
                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    if (isShow)
                    {
                        Adorner[] ar = lay.GetAdorners(txt);
                        if (ar != null)
                        {

                            for (int i = 0; i < ar.Length; i++)
                            {
                                if (ar[i] is PlaceholderAdorner)
                                {
                                    lay.Remove(ar[i]);
                                }
                            }
                        }
                    }
                    
                };

                txt.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.GotFocusEvent, getFocusedChangedHandler);

                RoutedEventHandler  lostFocusedChangedHandler = (s1, e1) =>
                {
                    // bool isShow = txt.Document != null && txt.Document.Blocks.Count == 0;
                    TextRange textRange = new TextRange(txt.Document.ContentStart, txt.Document.ContentEnd);
                    bool isShow = string.IsNullOrWhiteSpace(textRange.Text);
                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null) return;

                    if (isShow)
                    {
                        lay.Add(new PlaceholderAdorner(txt, e.NewValue.ToString()));
                    }

                };

                txt.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.LostFocusEvent, lostFocusedChangedHandler);
            }
        }

        #endregion





        #region PlaceholderVerticalAlignment

        public static VerticalAlignment GetPlackholderVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(PlaceholderVerticalAlignmentProperty);
        }

        public static void SetPlackholderVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(PlaceholderVerticalAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for PlackholderVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("PlaceholderVerticalAlignment", typeof(VerticalAlignment), typeof(PlaceHolderHelper), new PropertyMetadata(VerticalAlignment.Center,new PropertyChangedCallback(OnVericalAlignmentChanged)));

        public static void OnVericalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control con = d as Control;

            RoutedEventHandler loadHandler = null;
            loadHandler = (s1, e1) =>
            {

                var lay = AdornerLayer.GetAdornerLayer(con);
                if (lay == null)
                {
                    return;
                }
                else
                {
                    con.Loaded -= loadHandler;
                }

                Adorner[] ar = lay.GetAdorners(con);
                if (ar != null)
                {
                    for (int i = 0; i < ar.Length; i++)
                    {
                        if (ar[i] is PlaceholderAdorner)
                        {
                            PlaceholderAdorner pa = ar[i] as PlaceholderAdorner;
                            pa.SetVerticalAlign((VerticalAlignment)e.NewValue);
                        
                        }
                    }
                }



            };
            con.Loaded += loadHandler;


        }



        #endregion


      


        class PlaceholderAdorner : Adorner
        {
            private VisualCollection _visCollec;
            private TextBlock _tbPlaceholder;

            private Control _txt;
            private bool _showMaxLengthTips = false;

            public PlaceholderAdorner(UIElement ele, string placeholder)
                : base(ele)
            {
                if (ele is PasswordBox)
                {
                    _txt = ele as PasswordBox;
                }
                else if (ele is TextBox)
                {
                    _txt = ele as TextBox;                    
                }
                else if (ele is ComboBox)
                {
                    _txt = ele as ComboBox;
                }
                else if (ele is RichTextBox)
                {
                    _txt = ele as RichTextBox;
                }

                //else if (ele is PasswordTextBox)
                //{
                //    _txt = ele as PasswordTextBox;
                //}
                if (_txt == null) return;

                Binding bd = new Binding("IsVisible");

                bd.Source = _txt;
                bd.Mode = BindingMode.OneWay;
                bd.Converter = new BoolToVisibilityConverter();
                this.SetBinding(TextBox.VisibilityProperty, bd);
                _visCollec = new VisualCollection(this);
                _tbPlaceholder = new TextBlock();
                //_tbPlaceholder.VerticalAlignment = System.Windows.VerticalAlignment.Top;
               

                _tbPlaceholder.Style = null;

                Typeface face = new Typeface(_txt.FontFamily, _txt.FontStyle, _txt.FontWeight, _txt.FontStretch);
                FormattedText formattedText = new FormattedText(placeholder, Thread.CurrentThread.CurrentCulture, FlowDirection.LeftToRight, face, _txt.FontSize, Brushes.Gray);

                double width = formattedText.WidthIncludingTrailingWhitespace;
                double heiht = formattedText.Height;

                if ((double.IsNaN(_txt.ActualHeight) || _txt.ActualHeight == 0) &&
                    (double.IsNaN(_txt.Height) || _txt.Height == 0))
                {
                    _tbPlaceholder.Margin = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    double topPadding = _txt.Padding.Top;
                    double marginTop = _txt.Margin.Top;

                    if ((double.IsNaN(_txt.ActualHeight) || _txt.ActualHeight == 0))
                    {
                       // _tbPlaceholder.Margin = new Thickness(3, (_txt.Height - topPadding - marginTop - heiht) / 2.0, 0, 0);
                        _tbPlaceholder.Margin = new Thickness(3, 0, 0, 0);
                    }
                    else
                    {
                       _tbPlaceholder.Margin = new Thickness(3 + _txt.Padding.Left, 0, 0, 0);
                       // _tbPlaceholder.Margin = new Thickness(3 + _txt.Padding.Left, (_txt.ActualHeight - topPadding - marginTop - heiht) / 2.0, 0, 0);
                    }
                }

                _tbPlaceholder.FontSize = _txt.FontSize;
                _tbPlaceholder.FontFamily = _txt.FontFamily;
                _tbPlaceholder.FontWeight = _txt.FontWeight;
                _tbPlaceholder.VerticalAlignment = VerticalAlignment.Center;
                _tbPlaceholder.FontStretch = _txt.FontStretch;
                _tbPlaceholder.FontStyle = _txt.FontStyle;
                _tbPlaceholder.Foreground = Brushes.LightGray;
                _tbPlaceholder.Text = placeholder;
                _tbPlaceholder.IsHitTestVisible = false;
                if (_txt.Tag != null)
                {
                    switch (_txt.Tag.ToString())
                    {
                        case "1":
                            _tbPlaceholder.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                            _tbPlaceholder.HorizontalAlignment = HorizontalAlignment.Center;
                            _tbPlaceholder.FontSize = 60;
                            break;
                        case "2":
                            _tbPlaceholder.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            _tbPlaceholder.HorizontalAlignment = HorizontalAlignment.Center;
                            _tbPlaceholder.FontSize = 24;
                            break;
                        case "3":
                            _tbPlaceholder.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            _tbPlaceholder.HorizontalAlignment = HorizontalAlignment.Left;
                            _tbPlaceholder.FontSize = 44;
                            break;
                        case "4":
                            _tbPlaceholder.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            _tbPlaceholder.HorizontalAlignment = HorizontalAlignment.Left;
                            _tbPlaceholder.FontSize = 28;
                            break;
                        case "Top":
                            _tbPlaceholder.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                            break;
                    }
                    _tbPlaceholder.Foreground = Brushes.Black;
                }
                _visCollec.Add(_tbPlaceholder);
            }

            protected override int VisualChildrenCount
            {
                get
                {
                    return _visCollec.Count;
                }
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                _tbPlaceholder.Arrange(new Rect(new Point(4, 2), finalSize));
                return finalSize;
            }

            protected override Visual GetVisualChild(int index)
            {
                return _visCollec[index];
            }

            public void SetVerticalAlign(VerticalAlignment vertical) {
                _tbPlaceholder.VerticalAlignment = vertical;
            }

          
        }
    }

    public class TextBoxCharacterHelper
    {

        public static bool GetShowCharacter(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowCharacterProperty);
        }

        public static void SetShowCharacter(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowCharacterProperty, value);
        }


        public static readonly DependencyProperty ShowCharacterProperty =
            DependencyProperty.RegisterAttached("ShowCharacter", typeof(bool), typeof(TextBoxCharacterHelper),
                new UIPropertyMetadata(false, new PropertyChangedCallback(OnShowCharacterChanged)));

        public static void OnShowCharacterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox txt = d as TextBox;
            RoutedEventHandler loadHandler = null;
            loadHandler = (s1, e1) =>
            {
                var lay = AdornerLayer.GetAdornerLayer(txt);
                if (lay == null)
                {
                    return;
                }
                else
                {
                    txt.Loaded -= loadHandler;
                }

                Adorner[] ar = lay.GetAdorners(txt);
                if (ar != null)
                {
                    for (int i = 0; i < ar.Length; i++)
                    {
                        if (ar[i] is TextBoxCharacterAdorner)
                        {
                            lay.Remove(ar[i]);
                        }
                    }
                }

                lay.Add(new TextBoxCharacterAdorner(txt));
            };

            if ((bool)e.NewValue)
            {
                if (txt.IsLoaded)
                {
                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null)
                    {
                        return;
                    }

                    Adorner[] ar = lay.GetAdorners(txt);
                    if (ar != null)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (ar[i] is TextBoxCharacterAdorner)
                            {
                                lay.Remove(ar[i]);
                            }
                        }
                    }

                    lay.Add(new TextBoxCharacterAdorner(txt));

                }
                else
                {
                    txt.Loaded += loadHandler;
                }
            }
            else
            {
                if (txt.IsLoaded)
                {
                    var lay = AdornerLayer.GetAdornerLayer(txt);
                    if (lay == null)
                    {
                        return;
                    }

                    Adorner[] ar = lay.GetAdorners(txt);
                    if (ar != null)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            if (ar[i] is TextBoxCharacterAdorner)
                            {
                                lay.Remove(ar[i]);
                            }
                        }
                    }
                }
                else
                {
                    txt.Loaded -= loadHandler;
                }
            }
        }

        class TextBoxCharacterAdorner : Adorner
        {
            private VisualCollection _visCollec;
            private TextBlock _tbNumberOfCharacter;
            private TextBox _txt;

            public TextBoxCharacterAdorner(UIElement ele)
                : base(ele)
            {

                if (ele is TextBox)
                {
                    _txt = ele as TextBox;
                    _txt.TextWrapping = TextWrapping.Wrap;
                }
                _visCollec = new VisualCollection(this);

                _tbNumberOfCharacter = new TextBlock();
                _tbNumberOfCharacter.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                _tbNumberOfCharacter.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                _tbNumberOfCharacter.Style = null;
                _tbNumberOfCharacter.Text = string.Format("{0}/{1}", _txt.Text.Length, _txt.MaxLength);

                _tbNumberOfCharacter.FontSize = _txt.FontSize;
                _tbNumberOfCharacter.FontFamily = _txt.FontFamily;
                _tbNumberOfCharacter.FontWeight = _txt.FontWeight;
                _tbNumberOfCharacter.FontStretch = _txt.FontStretch;
                _tbNumberOfCharacter.FontStyle = _txt.FontStyle;
                _tbNumberOfCharacter.Foreground = Brushes.Gray;
                _tbNumberOfCharacter.IsHitTestVisible = false;
                _tbNumberOfCharacter.Margin = new Thickness(0, 0, 3, 0);                

                _visCollec.Add(_tbNumberOfCharacter);
                _txt.TextChanged += delegate
                {
                    _tbNumberOfCharacter.Text = string.Format("{0}/{1}", _txt.Text.Length, _txt.MaxLength);
                    if (_txt.Text.Length == _txt.MaxLength)
                    {
                        _tbNumberOfCharacter.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9300"));
                    }
                    _txt.ScrollToEnd();
                };

                if (_txt.Text.Length == _txt.MaxLength)
                {
                    _tbNumberOfCharacter.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9300"));
                }
            }

            

            protected override int VisualChildrenCount
            {
                get
                {
                    return _visCollec.Count;
                }
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                _tbNumberOfCharacter.Arrange(new Rect(new Point(0, 0), finalSize));
                return finalSize;
            }

            protected override Visual GetVisualChild(int index)
            {
                return _visCollec[index];
            }
        }
    }

    
       
}

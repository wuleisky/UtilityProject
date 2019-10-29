using System.Collections.Generic;

namespace System.Windows.Controls
{
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates"), TemplateVisualState(Name = "Active", GroupName = "ActiveStates"), TemplateVisualState(Name = "Small", GroupName = "SizeStates"), TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    public class ProgressRing : Control
    {
        public static readonly DependencyProperty BindableWidthProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty IsLargeProperty;
        public static readonly DependencyProperty MaxSideLengthProperty;
        public static readonly DependencyProperty EllipseDiameterProperty;
        public static readonly DependencyProperty EllipseOffsetProperty;
        public static readonly DependencyProperty LoadingPercentProperty;
        public static readonly DependencyProperty LoadingNameProperty;
        private List<Action> _deferredActions = new List<Action>();
        public double MaxSideLength
        {
            get
            {
                return (double)base.GetValue(ProgressRing.MaxSideLengthProperty);
            }
            private set
            {
                base.SetValue(ProgressRing.MaxSideLengthProperty, value);
            }
        }
        public double EllipseDiameter
        {
            get
            {
                return (double)base.GetValue(ProgressRing.EllipseDiameterProperty);
            }
            private set
            {
                base.SetValue(ProgressRing.EllipseDiameterProperty, value);
            }
        }

        public string LoadingPercent
        {
            get
            {
                return (string)base.GetValue(ProgressRing.LoadingPercentProperty);
            }
             set
            {
                base.SetValue(ProgressRing.LoadingPercentProperty, value);
            }
        }

        public string LoadingName
        {
            get
            {
                return (string)base.GetValue(ProgressRing.LoadingNameProperty);
            }
             set
            {
                base.SetValue(ProgressRing.LoadingNameProperty, value);
            }
        }

        public Thickness EllipseOffset
        {
            get
            {
                return (Thickness)base.GetValue(ProgressRing.EllipseOffsetProperty);
            }
            private set
            {
                base.SetValue(ProgressRing.EllipseOffsetProperty, value);
            }
        }
        public double BindableWidth
        {
            get
            {
                return (double)base.GetValue(ProgressRing.BindableWidthProperty);
            }
            private set
            {
                base.SetValue(ProgressRing.BindableWidthProperty, value);
            }
        }
        public bool IsActive
        {
            get
            {
                return (bool)base.GetValue(ProgressRing.IsActiveProperty);
            }
            set
            {
                base.SetValue(ProgressRing.IsActiveProperty, value);
            }
        }
        public bool IsLarge
        {
            get
            {
                return (bool)base.GetValue(ProgressRing.IsLargeProperty);
            }
            set
            {
                base.SetValue(ProgressRing.IsLargeProperty, value);
            }
        }
        static ProgressRing()
        {
            ProgressRing.BindableWidthProperty = DependencyProperty.Register("BindableWidth", typeof(double), typeof(ProgressRing), new PropertyMetadata(0.0, new PropertyChangedCallback(ProgressRing.BindableWidthCallback)));
            ProgressRing.IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ProgressRing), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ProgressRing.IsActiveChanged)));
            ProgressRing.IsLargeProperty = DependencyProperty.Register("IsLarge", typeof(bool), typeof(ProgressRing), new PropertyMetadata(true, new PropertyChangedCallback(ProgressRing.IsLargeChangedCallback)));
            ProgressRing.MaxSideLengthProperty = DependencyProperty.Register("MaxSideLength", typeof(double), typeof(ProgressRing), new PropertyMetadata(0.0));
            ProgressRing.EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ProgressRing), new PropertyMetadata(0.0));
            ProgressRing.EllipseOffsetProperty = DependencyProperty.Register("EllipseOffset", typeof(Thickness), typeof(ProgressRing), new PropertyMetadata(default(Thickness)));
            ProgressRing.LoadingPercentProperty = DependencyProperty.Register("LoadingPercent", typeof(string), typeof(ProgressRing), new PropertyMetadata(default(string)));
            ProgressRing.LoadingNameProperty = DependencyProperty.Register("LoadingName", typeof(string), typeof(ProgressRing), new PropertyMetadata(default(string)));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
            UIElement.VisibilityProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(delegate (DependencyObject ringObject, DependencyPropertyChangedEventArgs e)
            {
                if (e.NewValue != e.OldValue)
                {
                    ProgressRing progressRing = (ProgressRing)ringObject;
                    if ((Visibility)e.NewValue != Visibility.Visible)
                    {
                        progressRing.SetCurrentValue(ProgressRing.IsActiveProperty, false);
                        return;
                    }
                    progressRing.IsActive = true;
                }
            }));
        }
        public ProgressRing()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }
        private static void BindableWidthCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ProgressRing ring = dependencyObject as ProgressRing;
            if (ring == null)
            {
                return;
            }
            Action action = delegate
            {
                ring.SetEllipseDiameter((double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetEllipseOffset((double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetMaxSideLength((double)dependencyPropertyChangedEventArgs.NewValue);
            };
            if (ring._deferredActions != null)
            {
                ring._deferredActions.Add(action);
                return;
            }
            action();
        }
        private void SetMaxSideLength(double width)
        {
            this.MaxSideLength = ((width <= 20.0) ? 20.0 : width);
        }
        private void SetEllipseDiameter(double width)
        {
            this.EllipseDiameter = width / 8.0;
        }
        private void SetEllipseOffset(double width)
        {
            this.EllipseOffset = new Thickness(0.0, width / 2.0, 0.0, 0.0);
        }
        private static void IsLargeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ProgressRing progressRing = dependencyObject as ProgressRing;
            if (progressRing == null)
            {
                return;
            }
            progressRing.UpdateLargeState();
        }
        private void UpdateLargeState()
        {
            Action action;
            if (this.IsLarge)
            {
                action = delegate
                {
                    VisualStateManager.GoToState(this, "Large", true);
                };
            }
            else
            {
                action = delegate
                {
                    VisualStateManager.GoToState(this, "Small", true);
                };
            }
            if (this._deferredActions != null)
            {
                this._deferredActions.Add(action);
                return;
            }
            action();
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            this.BindableWidth = base.ActualWidth;
        }
        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ProgressRing progressRing = dependencyObject as ProgressRing;
            if (progressRing == null)
            {
                return;
            }
            progressRing.UpdateActiveState();
        }
        private void UpdateActiveState()
        {
            Action action;
            if (this.IsActive)
            {
                action = delegate
                {
                    VisualStateManager.GoToState(this, "Active", true);
                };
            }
            else
            {
                action = delegate
                {
                    VisualStateManager.GoToState(this, "Inactive", true);
                };
            }
            if (this._deferredActions != null)
            {
                this._deferredActions.Add(action);
                return;
            }
            action();
        }
        public override void OnApplyTemplate()
        {
            this.UpdateLargeState();
            this.UpdateActiveState();
            base.OnApplyTemplate();
            if (this._deferredActions != null)
            {
                foreach (Action current in this._deferredActions)
                {
                    current();
                }
            }
            this._deferredActions = null;
        }
    }
}

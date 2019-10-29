using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;


namespace UtilityResolution.Utility
{
    public class CustomPopup : Popup
    {
        public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(CustomPopup), new FrameworkPropertyMetadata(false, OnTopmostChanged));
        public bool Topmost
        {
            get { return (bool)GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }

        /// <summary>Popup的位置</summary>
        public Size PopupLocation = Size.Empty;

        private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as CustomPopup).UpdateWindow();
        }
        protected override void OnOpened(EventArgs e)
        {
            if (PopupLocation.IsEmpty)
            {
                UpdateWindow();
            }
            else
            {
                UpdateWindow(PopupLocation.Width, PopupLocation.Height);
            }
        }
        private void UpdateWindow()
        {
            var fromVisual = (HwndSource)PresentationSource.FromVisual(this.Child);
            if (fromVisual != null)
            {
                var hwnd = fromVisual.Handle;
                User32Manage.RECT rect = new User32Manage.RECT();
                if (User32Manage.GetWindowRect(hwnd, ref rect))
                {
                    User32Manage.SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)this.Width, (int)this.Height, 0);
                }
            }
        }

        private void UpdateWindow(double left, double top)
        {
            var fromVisual = (HwndSource)PresentationSource.FromVisual(this.Child);
            if (fromVisual != null)
            {
                var hwnd = fromVisual.Handle;
                User32Manage.RECT rect = new User32Manage.RECT();
                if (User32Manage.GetWindowRect(hwnd, ref rect))
                {
                    User32Manage.SetWindowPos(hwnd, Topmost ? -1 : -2, (int)left, (int)top, (int)this.Width, (int)this.Height, 0);
                }
            }
        }
    }
}

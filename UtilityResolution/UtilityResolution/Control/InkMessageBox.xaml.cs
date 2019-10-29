using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Threading;

using System.Runtime.InteropServices;
using System.Windows.Interop;
using UtilityResolution.Utility;

namespace System.Windows
{
    /// <summary>
    /// Interaction logic for InkMessageBox.xaml
    /// </summary>
    public partial class InkMessageBox : Window
    {
        private bool closeStoryBoardCompleted = false;
        private Storyboard closeStoryBoard = null;
        private System.Threading.Timer timer;

        public string Message
        {
            set
            {
                if (this.Dispatcher.CheckAccess())
                {
                    this.lblMsg.Text = value;
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        this.lblMsg.Text = value;
                    }), null);
                }
            }
        }

        public int KeepTime
        {
            get;
            set;
        }

        public Action ToastBack { get; set; }

        public InkMessageBox()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OnWindowLoaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(OnWindowClosing);

            this.timer = new Timer(Callback, this, Timeout.Infinite, Timeout.Infinite);

            DoubleAnimation hideWindowAnimation = new DoubleAnimation();
            hideWindowAnimation.From = 0.8;
            hideWindowAnimation.To = 0;
            hideWindowAnimation.Duration = TimeSpan.FromMilliseconds(200);
            closeStoryBoard = new Storyboard();
            closeStoryBoard.Children.Add(hideWindowAnimation);
            Storyboard.SetTarget(hideWindowAnimation, this);
            Storyboard.SetTargetProperty(hideWindowAnimation, new PropertyPath("Opacity"));
            //closeStoryBoard.Completed += OnHideWindowAnimationCompleted;
            this.Loaded += new RoutedEventHandler(InkMessageBox_Loaded);
            //this.Deactivated += new EventHandler(InkMessageBox_Deactivated);
        }

        void InkMessageBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            Window main = Application.Current.MainWindow;
            if (main.WindowState == System.Windows.WindowState.Maximized)
            {
                main.Left = -2.4;
                main.Top = -2.4;
            }
            this.Left = main.Left + (main.ActualWidth - this.ActualWidth) / 2;
            this.Top = main.Top + main.ActualHeight - 300;
        }

    
        void InkMessageBox_Deactivated(object sender, EventArgs e)
        {
            if (!closeStoryBoardCompleted)
            {
                this.timer.Change(Timeout.Infinite, Timeout.Infinite);
                this.timer.Dispose();
                this.timer = null;
                this.closeStoryBoardCompleted = true;
                this.Close();
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.timer.Change(this.KeepTime, Timeout.Infinite);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            if (!closeStoryBoardCompleted)
            {
                this.timer.Change(Timeout.Infinite, Timeout.Infinite);
                this.timer.Dispose();
                this.timer = null;
                this.closeStoryBoardCompleted = true;
                this.Close();
            }
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeStoryBoardCompleted)
            {
                closeStoryBoard.Begin();
                e.Cancel = true;
            }
            if (ToastBack != null)
            {
                ToastBack();
            }
        }
      
        private void OnHideWindowAnimationCompleted(object sender, EventArgs e)
        {
            if (!closeStoryBoardCompleted)
            {
                this.timer.Change(Timeout.Infinite, Timeout.Infinite);
                this.timer.Dispose();
                this.timer = null;
                this.closeStoryBoardCompleted = true;
                this.Close();
            }
        }

        private  void Callback(object state)
        {
            InkMessageBox box = state as InkMessageBox;

            if (box.Dispatcher.CheckAccess())
            {             
                box.Close();
            }
            else
            {
                box.Dispatcher.Invoke(new Action(delegate
                {
                    box.Close();
                }), null);
            }
            
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="keepTime">彈窗顯示持續時間(毫秒)</param>
        public static void Info(string msg, int keepTime, Action callBack = null)
        {
            try
            {
                if (keepTime <= 0) return;
                if (Application.Current == null || Application.Current.MainWindow == null)
                {
                    return;
                }

                if (Application.Current.Dispatcher.CheckAccess())
                {

                    if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                    {
                        return;
                    }
                    InkMessageBox box = new InkMessageBox() { Message = msg, KeepTime = keepTime, ToastBack = callBack };
                    RoutedEventHandler loadHandler = null;
                    loadHandler = (sender, e) =>
                    {
                        Window win = sender as Window;
                        win.Loaded -= loadHandler;
                        IntPtr hwnd = new WindowInteropHelper(win).Handle;
                        if (hwnd != IntPtr.Zero)
                            NativeMethods.SetWindowLong(hwnd, NativeMethods.GwlExstyle, NativeMethods.WsExToolwindow);
                    };
                    box.Loaded += loadHandler;
                    var winformWindow = (HwndSource.FromDependencyObject(Application.Current.MainWindow) as HwndSource);
                    if (winformWindow != null)
                        new WindowInteropHelper(box) { Owner = winformWindow.Handle };
                    box.Show();

                }
                else
                {
                    Action<string, int, Action> func = Info;
                    Application.Current.Dispatcher.Invoke(func, msg, keepTime, callBack);
                }
            }
            catch (Exception ex)
            {
                //LogFactory.Log.Error("MessageBox Info Error", ex);
            }
        }

        //从Handle中获取Window对象
        static Window GetWindowFromHwnd(IntPtr hwnd)
        {
            if (HwndSource.FromHwnd(hwnd)!=null&&HwndSource.FromHwnd(hwnd).RootVisual != null)
            {
                return (Window)HwndSource.FromHwnd(hwnd).RootVisual;
            }
            return null;
        }

        //GetForegroundWindow API
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //调用GetForegroundWindow然后调用GetWindowFromHwnd
        static Window GetTopWindow()
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == null)
                return null;

            return GetWindowFromHwnd(hwnd);
        }



        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="width">彈窗寬度</param>
        /// <param name="height">彈窗高度</param>
        /// <param name="keepTime">彈窗顯示持續時間</param>
        public static void Info(string msg)
        {
            InkMessageBox.Info(msg, 3000);
        }

    }
}

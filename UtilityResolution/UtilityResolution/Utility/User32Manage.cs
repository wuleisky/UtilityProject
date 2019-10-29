using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UtilityResolution.Utility
{
    public class User32Manage
    {

        #region 数确定给定窗口是否是最小化

        public const int SW_SHOWNOMAL = 1;
        public const int SW_RESTORE = 9;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOW = 5;
        /// <summary>
        /// 是否最小化
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int IsIconic(IntPtr hWnd);

        #endregion

        #region 函数是确定窗口是否是最大化的窗口
        /// <summary>
        /// 是否最大化
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int IsZoomed(IntPtr hWnd);

        #endregion

        #region 获得指定窗口的可视状态，即显示或者隐藏

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion

        #region 禁用窗体

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        #endregion

        #region 获取当前激活窗体

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        #endregion

        #region 改变窗体状态最小化、最大化

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public const int SW_SHOWMINIMIZED = 2;
        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        ///<summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        #endregion

        #region 查找窗体

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        #endregion

        #region 可以用来从过滤器添加或删除的消息

        public const int WM_COPYDATA = 0x004A;
        public const int WM_COPYDATA2 = 0x004B;
        [DllImport("user32")]
        public static extern bool ChangeWindowMessageFilter(uint msg, int flags);

        #endregion

        #region 向窗体发送消息

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            int hWnd, // 目标窗体句柄 
            uint Msg, // WM_COPYDATA 
            int wParam, // 自定义数值
            int lParam // 结构体
        );

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            int Msg,
            IntPtr wParam,
            IntPtr lParam
            );

        /// <summary>
        /// 定义用户要传递的消息的数据
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;//字符串长度
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;//字符串
        }

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
        IntPtr hWnd,                   //目标窗体句柄
        int Msg,                       //WM_COPYDATA
        int wParam,                //自定义数值
        ref  CopyDataStruct lParam             //结构体
        );

        #endregion

        #region 异步向窗体发送消息

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);

        #endregion

        #region 移动窗体

        public const int LEFTDOWN = 0x0002; /* left button down */
        public const int LEFTUP = 0x0004; /* left button up */
        public const int MOVE = 0x0001; /* mouse move */
        public const int RIGHTDOWN = 0x0008;
        public const int RIGHTUP = 0x0010;
        public const int MIDDLEDOWN = 0x0020;
        public const int MIDDLEUP = 0x0040;
        public const int ABSOLUTE = 0x8000;
        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        #endregion

        #region 获取指定窗体的属性

        public const UInt32 WS_DISABLED = 0x8000000;

        /// <summary>
        /// Specifies we wish to retrieve window styles.
        /// </summary>
        public const int GWL_STYLE = -16;
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int value);

        #endregion

        #region 获取窗体对应坐标

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        #endregion

        #region 获取鼠标位子
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(Point pt)
            {
                X = System.Convert.ToInt32(pt.X);
                Y = System.Convert.ToInt32(pt.Y);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        #endregion

        #region 设置窗体坐标

        [DllImport("user32", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        #endregion

        #region 获取指定点所在窗口的句柄

        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        #endregion

        #region 函数功能是该函数从当前线程中的窗口释放鼠标捕获，并恢复通常的鼠标输入处理

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        #region 第一次击键和第二次击键之间的最大毫秒数

        [DllImport("user32.dll")]
        public static extern uint GetDoubleClickTime();

        #endregion

        #region XP下隐藏窗口在任务栏中的右键菜单中的关闭菜单

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        public static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        public static extern int GetMenuItemCount(IntPtr hmenu);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        public static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        public static extern int DrawMenuBar(IntPtr hwnd);

        public const int MF_BYPOSITION = 0x0400;
        public const int MF_DISABLED = 0x0002;

        #endregion

        #region 监控鼠标按键状态

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int vKey);

        #endregion

        #region 隐藏和显示光标

        [DllImport("user32.dll")]
        public static extern int HideCaret(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "ShowCaret")]
        public static extern long ShowCaret(IntPtr hwnd);

        [DllImport("user32.dll")]
        protected static extern bool DestroyIcon(IntPtr hIcon);

        #endregion

        #region APP中客户区的坐标点信息转换为整个屏幕的坐标

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        #endregion

        #region 功能是改变指定窗口的位置和大小

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        #endregion
    }
}

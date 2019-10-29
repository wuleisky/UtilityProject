using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UtilityResolution.Utility
{
    public static class NativeMethods
    {
        public const int GwlExstyle = -20;
        public const int WsExToolwindow = 0x00000080;  //不显示在alt + tab中
        public const int WsExNormalwindow = 0x00080008;//显示在alt + tab中

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool ShowWindow(IntPtr hWnd, short State);

        public enum WindowShowStatus
        {
            /// <summary>
            /// 隐藏窗口
            /// </summary>
            SW_HIDE = 0,
            /// <summary>
            /// 最大化窗口
            /// </summary>
            SW_MAXIMIZE = 3,
            /// <summary>
            /// 最小化窗口
            /// </summary>
            SW_MINIMIZE = 6,
            /// <summary>
            /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态
            /// </summary>
            SW_RESTORE = 9,
            /// <summary>
            /// 用当前的大小和位置显示一个窗口，同时令其进入活动状态
            /// </summary>
            SW_SHOW = 5,
            /// <summary>
            /// 最大化窗口，并将其激活
            /// </summary>
            SW_SHOWMAXIMIZED = 3,
            /// <summary>
            /// 最小化窗口，并将其激活
            /// </summary>
            SW_SHOWMINIMIZED = 2,
            /// <summary>
            /// 最小化一个窗口，同时不改变活动窗口
            /// </summary>
            SW_SHOWMINNOACTIVE = 7,
            /// <summary>
            /// 用当前的大小和位置显示一个窗口，不改变活动窗口
            /// </summary>
            SW_SHOWNA = 8,
            /// <summary>
            /// 用最近的大小和位置显示一个窗口，同时不改变活动窗口
            /// </summary>
            SW_SHOWNOACTIVATE = 4,
            /// <summary>
            /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态，与SW_RESTORE 相同
            /// </summary>
            SW_SHOWNORMAL = 1,
            /// <summary>
            /// 关闭窗体
            /// </summary>
            WM_CLOSE = 0x10
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetLongPathName(string ShortPath, StringBuilder sb, int buffer);

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
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, uint value);

        /// <summary>
        /// 桌面刷新
        /// </summary>
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(HChangeNotifyEventID wEventId, HChangeNotifyFlags uFlags, IntPtr dwItem1, IntPtr dwItem2);

        #region public enum HChangeNotifyFlags
        [Flags]
        public enum HChangeNotifyFlags
        {
            SHCNF_DWORD = 0x0003,
            SHCNF_IDLIST = 0x0000,
            SHCNF_PATHA = 0x0001,
            SHCNF_PATHW = 0x0005,
            SHCNF_PRINTERA = 0x0002,
            SHCNF_PRINTERW = 0x0006,
            SHCNF_FLUSH = 0x1000,
            SHCNF_FLUSHNOWAIT = 0x2000
        }
        #endregion//enum HChangeNotifyFlags
        #region enum HChangeNotifyEventID
        [Flags]
        public enum HChangeNotifyEventID
        {
            SHCNE_ALLEVENTS = 0x7FFFFFFF,

            SHCNE_ASSOCCHANGED = 0x08000000,

            SHCNE_ATTRIBUTES = 0x00000800,

            SHCNE_CREATE = 0x00000002,

            SHCNE_DELETE = 0x00000004,

            SHCNE_DRIVEADD = 0x00000100,

            SHCNE_DRIVEADDGUI = 0x00010000,

            SHCNE_DRIVEREMOVED = 0x00000080,

            SHCNE_EXTENDED_EVENT = 0x04000000,

            SHCNE_FREESPACE = 0x00040000,

            SHCNE_MEDIAINSERTED = 0x00000020,

            SHCNE_MEDIAREMOVED = 0x00000040,

            SHCNE_MKDIR = 0x00000008,

            SHCNE_NETSHARE = 0x00000200,

            SHCNE_NETUNSHARE = 0x00000400,

            SHCNE_RENAMEFOLDER = 0x00020000,

            SHCNE_RENAMEITEM = 0x00000001,

            SHCNE_RMDIR = 0x00000010,

            SHCNE_SERVERDISCONNECT = 0x00004000,

            SHCNE_UPDATEDIR = 0x00001000,

            SHCNE_UPDATEIMAGE = 0x00008000,
        }
        #endregion

    }
}
#region

using System;
using System.Runtime.InteropServices;
using System.Windows;

#endregion

namespace Gemini.Framework.Win32
{
    internal static class NativeMethods
    {
        #region Constants

        public const int GwlStyle = -16;
        public const int GwlExstyle = -20;

        public const int WsMaximizebox = 0x10000;
        public const int WsMinimizebox = 0x20000;

        public const int WsExDlgmodalframe = 0x00000001;

        public const int SwpNosize = 0x0001;
        public const int SwpNomove = 0x0002;
        public const int SwpNozorder = 0x0004;
        public const int SwpFramechanged = 0x0020;

        public const uint WmSeticon = 0x0080;

        // Define the window styles we use
        public const int WsChild = 0x40000000;
        public const int WsVisible = 0x10000000;

        // Define the Windows messages we will handle
        public const int WmMousemove = 0x0200;
        public const int WmLbuttondown = 0x0201;
        public const int WmLbuttonup = 0x0202;
        public const int WmLbuttondblclk = 0x0203;
        public const int WmRbuttondown = 0x0204;
        public const int WmRbuttonup = 0x0205;
        public const int WmRbuttondblclk = 0x0206;
        public const int WmMbuttondown = 0x0207;
        public const int WmMbuttonup = 0x0208;
        public const int WmMbuttondblclk = 0x0209;
        public const int WmMousewheel = 0x020A;
        public const int WmXbuttondown = 0x020B;
        public const int WmXbuttonup = 0x020C;
        public const int WmXbuttondblclk = 0x020D;
        public const int WmMouseleave = 0x02A3;

        // Define the values that let us differentiate between the two extra mouse buttons
        public const int MkXbutton1 = 0x020;
        public const int MkXbutton2 = 0x040;

        // Define the cursor icons we use
        public const int IdcArrow = 32512;

        // Define the TME_LEAVE value so we can register for WM_MOUSELEAVE messages
        public const uint TmeLeave = 0x00000002;

        #endregion

        #region Delegates and Structs

        public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static readonly WndProc DefaultWindowProc = DefWindowProc;

        [StructLayout(LayoutKind.Sequential)]
        public struct Trackmouseevent
        {
            public int cbSize;
            public uint dwFlags;
            public IntPtr hWnd;
            public uint dwHoverTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Wndclassex
        {
            public uint cbSize;
            public uint style;
            [MarshalAs(UnmanagedType.FunctionPtr)] public WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativePoint
        {
            public int X;
            public int Y;
        }

        #endregion

        #region DllImports

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int value);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
            int x, int y, int width, int height, uint flags);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint msg,
            IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowEx(
            int exStyle,
            string className,
            string windowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Auto)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string module);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        public static extern int TrackMouseEvent(ref Trackmouseevent lpEventTrack);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern short RegisterClassEx([In] ref Wndclassex lpwcx);

        [DllImport("user32.dll")]
        public static extern int ScreenToClient(IntPtr hWnd, ref NativePoint pt);

        [DllImport("user32.dll")]
        public static extern int SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref NativePoint point);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);

        [DllImport("user32.dll")]
        public static extern uint GetDoubleClickTime();

        #endregion

        #region Helpers

        public static int GetXlParam(int lParam)
        {
            return LowWord(lParam);
        }

        public static int GetYlParam(int lParam)
        {
            return HighWord(lParam);
        }

        public static int GetWheelDeltaWParam(int wParam)
        {
            return HighWord(wParam);
        }

        public static int LowWord(int input)
        {
            return (short) (input & 0xffff);
        }

        public static int HighWord(int input)
        {
            return (short) (input >> 16);
        }

        public static bool SetCursorPos(Point pos)
        {
            return SetCursorPos((int) pos.X, (int) pos.Y);
        }

        #endregion
    }
}
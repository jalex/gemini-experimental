#region

using System.Runtime.InteropServices;

#endregion

namespace Gemini.Modules.Inspector.Win32
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern bool GetCursorPos(out NativePoint point);

        [StructLayout(LayoutKind.Sequential)]
        public struct NativePoint
        {
            public int X;
            public int Y;
        }
    }
}
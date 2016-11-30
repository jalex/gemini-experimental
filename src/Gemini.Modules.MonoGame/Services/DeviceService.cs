#region

using System;
using System.Windows;
using System.Windows.Interop;
using Gemini.Modules.MonoGame.Util;
using SharpDX.Direct3D9;

#endregion

namespace Gemini.Modules.MonoGame.Services
{
    internal static class DeviceService
    {
        private static int _activeClients;
        private static Direct3DEx _d3DContext;
        private static DeviceEx _d3DDevice;

        public static DeviceEx D3DDevice => _d3DDevice;

        public static void StartD3D(Window parentWindow)
        {
            _activeClients++;

            if (_activeClients > 1)
                return;

            _d3DContext = new Direct3DEx();

            var presentParameters = new PresentParameters
            {
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                DeviceWindowHandle = new WindowInteropHelper(parentWindow).Handle,
                PresentationInterval = PresentInterval.Default
            };

            _d3DDevice = new DeviceEx(_d3DContext, 0, DeviceType.Hardware, IntPtr.Zero,
                CreateFlags.HardwareVertexProcessing | CreateFlags.Multithreaded | CreateFlags.FpuPreserve,
                presentParameters);
        }

        public static void EndD3D()
        {
            _activeClients--;
            if (_activeClients < 0)
                throw new InvalidOperationException();

            if (_activeClients != 0)
                return;

            Disposer.RemoveAndDispose(ref _d3DDevice);
            Disposer.RemoveAndDispose(ref _d3DContext);
        }
    }
}
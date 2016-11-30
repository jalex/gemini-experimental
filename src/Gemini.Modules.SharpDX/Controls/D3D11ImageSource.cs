#region

using System;
using System.Windows;
using System.Windows.Interop;
using Gemini.Modules.SharpDX.Services;
using Gemini.Modules.SharpDX.Util;
using SharpDX.Direct3D11;
using SharpDX.Direct3D9;

#endregion

namespace Gemini.Modules.SharpDX.Controls
{
    public class D3D11ImageSource : D3DImage, IDisposable
    {
        private Texture _renderTarget;

        public D3D11ImageSource(Window parentWindow)
        {
            DeviceService.StartD3D(parentWindow);
        }

        public void Dispose()
        {
            SetRenderTargetDx10(null);
            Disposer.RemoveAndDispose(ref _renderTarget);
            DeviceService.EndD3D();
        }

        internal void InvalidateD3DImage()
        {
            if (_renderTarget == null)
                return;

            Lock();
            AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));
            Unlock();
        }

        internal void SetRenderTargetDx10(Texture2D renderTarget)
        {
            if (_renderTarget != null)
            {
                _renderTarget = null;

                Lock();
                SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
                Unlock();
            }

            if (renderTarget == null)
                return;

            if (!renderTarget.IsShareable())
                throw new ArgumentException("Texture must be created with ResourceOptionFlags.Shared");

            var format = renderTarget.GetTranslatedFormat();
            if (format == Format.Unknown)
                throw new ArgumentException("Texture format is not compatible with OpenSharedResource");

            var handle = renderTarget.GetSharedHandle();
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Handle could not be retrieved");

            _renderTarget = new Texture(DeviceService.D3DDevice,
                renderTarget.Description.Width,
                renderTarget.Description.Height,
                1, Usage.RenderTarget, format,
                Pool.Default, ref handle);

            using (var surface = _renderTarget.GetSurfaceLevel(0))
            {
                Lock();
                SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
                Unlock();
            }
        }
    }
}
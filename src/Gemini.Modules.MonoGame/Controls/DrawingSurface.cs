#region

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Gemini.Modules.MonoGame.Services;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using Texture = SharpDX.Direct3D9.Texture;

#endregion

namespace Gemini.Modules.MonoGame.Controls
{
    public class DrawingSurface : ContentControl, IDisposable
    {
        private readonly D3DImage _d3DImage;
        private readonly Image _image;

        private bool _contentNeedsRefresh;

        private GraphicsDeviceService _graphicsDeviceService;
        private RenderTarget2D _renderTarget;
        private Texture _renderTargetD3D9;

        public DrawingSurface()
        {
            _d3DImage = new D3DImage();

            _image = new Image {Source = _d3DImage, Stretch = Stretch.None};
            AddChild(_image);

            _d3DImage.IsFrontBufferAvailableChanged += OnD3DImageIsFrontBufferAvailableChanged;

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this control will redraw every time the CompositionTarget.Rendering event
        ///     is fired.
        ///     Defaults to false.
        /// </summary>
        public bool AlwaysRefresh { get; set; }

        public GraphicsDevice GraphicsDevice => _graphicsDeviceService.GraphicsDevice;

        /// <summary>
        ///     Occurs when the control has initialized the GraphicsDevice.
        /// </summary>
        public event EventHandler<GraphicsDeviceEventArgs> LoadContent;

        /// <summary>
        ///     Occurs when the DrawingSurface has been invalidated.
        /// </summary>
        public event EventHandler<DrawEventArgs> Draw;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            RemoveBackBufferReference();
            _contentNeedsRefresh = true;

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_graphicsDeviceService == null)
            {
                DeviceService.StartD3D(Window.GetWindow(this));

                // We use a render target, so the back buffer dimensions don't matter.
                _graphicsDeviceService = GraphicsDeviceService.AddRef(1, 1);
                _graphicsDeviceService.DeviceResetting += OnGraphicsDeviceServiceDeviceResetting;

                // Invoke the LoadContent event
                RaiseLoadContent(new GraphicsDeviceEventArgs(_graphicsDeviceService.GraphicsDevice));

                EnsureRenderTarget();

                CompositionTarget.Rendering += OnCompositionTargetRendering;

                _contentNeedsRefresh = true;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_graphicsDeviceService != null)
            {
                RemoveBackBufferReference();

                CompositionTarget.Rendering -= OnCompositionTargetRendering;

                _graphicsDeviceService.DeviceResetting -= OnGraphicsDeviceServiceDeviceResetting;
                _graphicsDeviceService = null;

                DeviceService.EndD3D();
            }
        }

        private void OnGraphicsDeviceServiceDeviceResetting(object sender, EventArgs e)
        {
            RemoveBackBufferReference();
            _contentNeedsRefresh = true;
        }

        /// <summary>
        ///     If we didn't do this, D3DImage would keep an reference to the backbuffer that causes the device reset below to
        ///     fail.
        /// </summary>
        private void RemoveBackBufferReference()
        {
            if (_renderTarget != null)
            {
                _renderTarget.Dispose();
                _renderTarget = null;
            }

            if (_renderTargetD3D9 != null)
            {
                _renderTargetD3D9.Dispose();
                _renderTargetD3D9 = null;
            }

            _d3DImage.Lock();
            _d3DImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
            _d3DImage.Unlock();
        }

        private void EnsureRenderTarget()
        {
            if (_renderTarget == null)
            {
                _renderTarget = new RenderTarget2D(GraphicsDevice, (int) ActualWidth, (int) ActualHeight,
                    false, SurfaceFormat.Bgra32, DepthFormat.Depth24Stencil8, 1,
                    RenderTargetUsage.PlatformContents, true);

                var handle = _renderTarget.GetSharedHandle();
                if (handle == IntPtr.Zero)
                    throw new ArgumentException("Handle could not be retrieved");

                _renderTargetD3D9 = new Texture(DeviceService.D3DDevice,
                    _renderTarget.Width, _renderTarget.Height,
                    1, Usage.RenderTarget, Format.A8R8G8B8,
                    Pool.Default, ref handle);

                using (var surface = _renderTargetD3D9.GetSurfaceLevel(0))
                {
                    _d3DImage.Lock();
                    _d3DImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
                    _d3DImage.Unlock();
                }
            }
        }

        private void OnD3DImageIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_d3DImage.IsFrontBufferAvailable)
                _contentNeedsRefresh = true;
        }

        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            if ((_contentNeedsRefresh || AlwaysRefresh) && BeginDraw())
            {
                _contentNeedsRefresh = false;

                _d3DImage.Lock();

                EnsureRenderTarget();
                GraphicsDevice.SetRenderTarget(_renderTarget);

                SetViewport();

                RaiseDraw(new DrawEventArgs(this, _graphicsDeviceService.GraphicsDevice));

                _graphicsDeviceService.GraphicsDevice.Flush();

                _d3DImage.AddDirtyRect(new Int32Rect(0, 0, (int) ActualWidth, (int) ActualHeight));

                _d3DImage.Unlock();

                GraphicsDevice.SetRenderTarget(null);
            }
        }

        protected virtual void RaiseLoadContent(GraphicsDeviceEventArgs args)
        {
            var handler = LoadContent;
            if (handler != null)
                handler(this, args);
        }

        protected virtual void RaiseDraw(DrawEventArgs args)
        {
            var handler = Draw;
            if (handler != null)
                handler(this, args);
        }

        private bool BeginDraw()
        {
            // If we have no graphics device, we must be running in the designer.
            if (_graphicsDeviceService == null)
                return false;

            if (!_d3DImage.IsFrontBufferAvailable)
                return false;

            // Make sure the graphics device is big enough, and is not lost.
            if (!HandleDeviceReset())
                return false;

            return true;
        }

        private void SetViewport()
        {
            // Many GraphicsDeviceControl instances can be sharing the same
            // GraphicsDevice. The device backbuffer will be resized to fit the
            // largest of these controls. But what if we are currently drawing
            // a smaller control? To avoid unwanted stretching, we set the
            // viewport to only use the top left portion of the full backbuffer.
            _graphicsDeviceService.GraphicsDevice.Viewport = new Viewport(
                0, 0, Math.Max(1, (int) ActualWidth), Math.Max(1, (int) ActualHeight));
        }

        private bool HandleDeviceReset()
        {
            var deviceNeedsReset = false;

            switch (_graphicsDeviceService.GraphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    // If the graphics device is lost, we cannot use it at all.
                    return false;

                case GraphicsDeviceStatus.NotReset:
                    // If device is in the not-reset state, we should try to reset it.
                    deviceNeedsReset = true;
                    break;
            }

            if (deviceNeedsReset)
            {
                Debug.WriteLine("Resetting Device");
                _graphicsDeviceService.ResetDevice((int) ActualWidth, (int) ActualHeight);
                return false;
            }

            return true;
        }

        public void Invalidate()
        {
            _contentNeedsRefresh = true;
        }

        #region IDisposable

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (_renderTarget != null)
                    _renderTarget.Dispose();
                if (_renderTargetD3D9 != null)
                    _renderTargetD3D9.Dispose();
                if (_graphicsDeviceService != null)
                    _graphicsDeviceService.Release(disposing);
                IsDisposed = true;
            }
        }

        ~DrawingSurface()
        {
            Dispose(false);
        }

        #endregion
    }
}
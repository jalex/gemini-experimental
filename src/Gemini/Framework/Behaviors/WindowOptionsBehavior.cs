#region

using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;
using Gemini.Framework.Win32;

#endregion

namespace Gemini.Framework.Behaviors
{
    public class WindowOptionsBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register(
            "ShowIcon", typeof(bool), typeof(WindowOptionsBehavior),
            new PropertyMetadata(true, OnWindowOptionChanged));

        public static readonly DependencyProperty ShowMinimizeBoxProperty = DependencyProperty.Register(
            "ShowMinimizeBox", typeof(bool), typeof(WindowOptionsBehavior),
            new PropertyMetadata(true, OnWindowOptionChanged));

        public static readonly DependencyProperty ShowMaximizeBoxProperty = DependencyProperty.Register(
            "ShowMaximizeBox", typeof(bool), typeof(WindowOptionsBehavior),
            new PropertyMetadata(true, OnWindowOptionChanged));

        public bool ShowIcon
        {
            get { return (bool) GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }

        public bool ShowMinimizeBox
        {
            get { return (bool) GetValue(ShowMinimizeBoxProperty); }
            set { SetValue(ShowMinimizeBoxProperty, value); }
        }

        public bool ShowMaximizeBox
        {
            get { return (bool) GetValue(ShowMaximizeBoxProperty); }
            set { SetValue(ShowMaximizeBoxProperty, value); }
        }

        private static void OnWindowOptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WindowOptionsBehavior) d).UpdateWindowStyle();
        }

        private void UpdateWindowStyle()
        {
            if (AssociatedObject == null)
                return;

            var handle = new WindowInteropHelper(AssociatedObject).Handle;

            var windowStyle = NativeMethods.GetWindowLong(handle, NativeMethods.GwlStyle);

            if (ShowMinimizeBox)
                windowStyle |= NativeMethods.WsMinimizebox;
            else
                windowStyle &= ~NativeMethods.WsMinimizebox;

            if (ShowMaximizeBox)
                windowStyle |= NativeMethods.WsMaximizebox;
            else
                windowStyle &= ~NativeMethods.WsMaximizebox;

            NativeMethods.SetWindowLong(handle, NativeMethods.GwlStyle, windowStyle);

            if (ShowIcon)
            {
                // TODO
            }
            else
            {
                var exWindowStyle = NativeMethods.GetWindowLong(handle, NativeMethods.GwlExstyle);
                NativeMethods.SetWindowLong(handle, NativeMethods.GwlExstyle,
                    exWindowStyle | NativeMethods.WsExDlgmodalframe);

                NativeMethods.SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0,
                    NativeMethods.SwpNomove | NativeMethods.SwpNosize | NativeMethods.SwpNozorder |
                    NativeMethods.SwpFramechanged);

                NativeMethods.SendMessage(handle, NativeMethods.WmSeticon, IntPtr.Zero, IntPtr.Zero);
            }
        }

        protected override void OnAttached()
        {
            AssociatedObject.SourceInitialized += OnSourceInitialized;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SourceInitialized -= OnSourceInitialized;
            base.OnDetaching();
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            UpdateWindowStyle();
        }
    }
}
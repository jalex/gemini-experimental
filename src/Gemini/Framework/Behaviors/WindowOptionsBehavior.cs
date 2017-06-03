#region

using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;
using Gemini.Framework.Win32;

#endregion

namespace Gemini.Framework.Behaviors
{
    /// <summary>
    ///     Represents a <see cref="Behavior{T}" /> for <see cref="Window" /> which provides common window options.
    /// </summary>
    public class WindowOptionsBehavior : Behavior<Window>
    {
        /// <summary>
        ///     Specifies the <see cref="DependencyProperty" /> for <see cref="ShowIcon" />.
        /// </summary>
        public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register(
            nameof(ShowIcon), typeof(bool), typeof(WindowOptionsBehavior),
            new PropertyMetadata(true, OnWindowOptionChanged));

        /// <summary>
        ///     Specifies the <see cref="DependencyProperty" /> for <see cref="ShowMinimizeBox" />.
        /// </summary>
        public static readonly DependencyProperty ShowMinimizeBoxProperty = DependencyProperty.Register(
            nameof(ShowMinimizeBox), typeof(bool), typeof(WindowOptionsBehavior),
            new PropertyMetadata(true, OnWindowOptionChanged));

        /// <summary>
        ///     Specifies the <see cref="DependencyProperty" /> for <see cref="ShowMaximizeBox" />.
        /// </summary>
        public static readonly DependencyProperty ShowMaximizeBoxProperty = DependencyProperty.Register(
            nameof(ShowMaximizeBox), typeof(bool), typeof(WindowOptionsBehavior),
            new PropertyMetadata(true, OnWindowOptionChanged));

        /// <summary>
        ///     Gets or sets whether the window icon should be displayed.
        /// </summary>
        public bool ShowIcon
        {
            get { return (bool) GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }

        /// <summary>
        ///     Gets or sets whether the minimize box should be displayed.
        /// </summary>
        public bool ShowMinimizeBox
        {
            get { return (bool) GetValue(ShowMinimizeBoxProperty); }
            set { SetValue(ShowMinimizeBoxProperty, value); }
        }

        /// <summary>
        ///     Gets or sets whether the maximize box should be displayed.
        /// </summary>
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

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            AssociatedObject.SourceInitialized += OnSourceInitialized;
            base.OnAttached();
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
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

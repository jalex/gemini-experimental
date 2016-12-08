#region

using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;
using MahApps.Metro.Controls;

#endregion

namespace Gemini.Framework.Behaviors
{
    // Copied from MahApp's GlowWindowBehavior, because that one has a bug if GlowBrush is set in a style, rather than directly.
    public class CustomGlowWindowBehavior : Behavior<MetroWindow>
    {
        private GlowWindow _bottom;
        private GlowWindow _left;
        private GlowWindow _right;
        private GlowWindow _top;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var metroWindow = AssociatedObject;
            if ((metroWindow != null) && metroWindow.UseNoneWindowStyle)
                return;
            _left = new GlowWindow(AssociatedObject, GlowDirection.Left);
            _right = new GlowWindow(AssociatedObject, GlowDirection.Right);
            _top = new GlowWindow(AssociatedObject, GlowDirection.Top);
            _bottom = new GlowWindow(AssociatedObject, GlowDirection.Bottom);
            Show();
            Update();

            metroWindow.LocationChanged += (s, e) => Update();
            metroWindow.SizeChanged += (s, e) => Update();

            if ((metroWindow == null) || !metroWindow.WindowTransitionsEnabled)
            {
                SetOpacityTo(1.0);
            }
            else
            {
                StartOpacityStoryboard();
                AssociatedObject.IsVisibleChanged += AssociatedObjectIsVisibleChanged;
                AssociatedObject.Closing += (CancelEventHandler) ((o, args) =>
                {
                    if (args.Cancel)
                        return;
                    AssociatedObject.IsVisibleChanged -= AssociatedObjectIsVisibleChanged;
                });
            }
        }

        private void AssociatedObjectIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!AssociatedObject.IsVisible)
                SetOpacityTo(0.0);
            else
                StartOpacityStoryboard();
        }

        /// <summary>
        ///     Updates all glow windows (visible, hidden, collapsed)
        /// </summary>
        private void Update()
        {
            if ((_left == null) || (_right == null) || (_top == null) || (_bottom == null))
                return;
            _left.Update();
            _right.Update();
            _top.Update();
            _bottom.Update();
        }

        /// <summary>
        ///     Sets the opacity to all glow windows
        /// </summary>
        private void SetOpacityTo(double newOpacity)
        {
            if ((_left == null) || (_right == null) || (_top == null) || (_bottom == null))
                return;
            _left.Opacity = newOpacity;
            _right.Opacity = newOpacity;
            _top.Opacity = newOpacity;
            _bottom.Opacity = newOpacity;
        }

        /// <summary>
        ///     Starts the opacity storyboard 0 -&gt; 1
        /// </summary>
        private void StartOpacityStoryboard()
        {
            if ((_left?.OpacityStoryboard == null) || (_right?.OpacityStoryboard == null) ||
                (_top?.OpacityStoryboard == null) || (_bottom?.OpacityStoryboard == null))
                return;
            _left.BeginStoryboard(_left.OpacityStoryboard);
            _right.BeginStoryboard(_right.OpacityStoryboard);
            _top.BeginStoryboard(_top.OpacityStoryboard);
            _bottom.BeginStoryboard(_bottom.OpacityStoryboard);
        }

        /// <summary>
        ///     Shows all glow windows
        /// </summary>
        private void Show()
        {
            _left.Show();
            _right.Show();
            _top.Show();
            _bottom.Show();
        }
    }
}
#region

using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

#endregion

namespace Gemini.Framework.Behaviors
{

    /// <summary>
    ///     Represents a <see cref="Behavior{T}"/> which automatically focuses an associated <see cref="FrameworkElement"/>
    ///     using <see cref="Keyboard.Focus"/> when attached.
    /// </summary>
    public class KeyboardFocusBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            if (!AssociatedObject.IsLoaded)
                AssociatedObject.Loaded += (sender, e) => { Keyboard.Focus(AssociatedObject); };
            else
                Keyboard.Focus(AssociatedObject);

            base.OnAttached();
        }
    }
}

#region

using System.Windows;
using System.Windows.Input;

#endregion

namespace Gemini.Framework.Commands
{

    /// <summary>
    ///     Represents a service for binding and retrieving <see cref="KeyGesture"/>s.
    /// </summary>
    public interface ICommandKeyGestureService
    {
        /// <summary>
        ///     Binds the available key gestures to the specified <see cref="UIElement"/>.
        /// </summary>
        /// <param name="uiElement">
        ///     The <see cref="UIElement"/> to bind the key gestures to.
        /// </param>
        void BindKeyGestures(UIElement uiElement);

        /// <summary>
        ///     Returns the primary <see cref="KeyGesture"/> associated with the specified <see cref="CommandDefinitionBase"/>.
        /// </summary>
        /// <param name="commandDefinition">The <see cref="CommandDefinitionBase"/> to lookup.</param>
        /// <returns>The primary <see cref="KeyGesture"/> if available, otherwise null.</returns>
        KeyGesture GetPrimaryKeyGesture(CommandDefinitionBase commandDefinition);
    }
}

#region

using System;
using System.Windows.Input;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Framework.Menus
{

    /// <summary>
    ///     Represents the menu item definition of a standard text menu item.
    /// </summary>
    public class TextMenuItemDefinition : MenuItemDefinition
    {
        /// <summary>
        ///     Returns the display text of the menu definition.
        /// </summary>
        public override string Text { get; }

        /// <summary>
        ///     Returns the <see cref="Uri"/> of an icon associated with the menu definition.
        /// </summary>
        public override Uri IconSource { get; }

        /// <summary>
        ///     Returns a default <see cref="System.Windows.Input.KeyGesture"/> associated with the menu definition.
        /// </summary>
        public override KeyGesture KeyGesture => null;

        /// <summary>
        ///     Returns the <see cref="CommandDefinitionBase"/> associated with the menu definition.
        /// </summary>
        public override CommandDefinitionBase CommandDefinition => null;

        /// <summary>
        ///     Creates a new <see cref="TextMenuItemDefinition"/>.
        /// </summary>
        /// <param name="group">The <see cref="MenuItemGroupDefinition"/> this menu item belongs to.</param>
        /// <param name="sortOrder">The sort order of the menu item definition.</param>
        /// <param name="text">The display text of the menu item definition.</param>
        /// <param name="iconSource">The <see cref="Uri"/> of an icon associated with the menu definition.</param>
        public TextMenuItemDefinition(MenuItemGroupDefinition group, int sortOrder, string text, Uri iconSource = null)
            : base(group, sortOrder)
        {
            Text = text;
            IconSource = iconSource;
        }
    }
}

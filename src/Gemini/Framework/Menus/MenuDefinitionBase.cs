#region

using System;
using System.Windows.Input;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Framework.Menus
{

    /// <summary>
    ///     Represents a base type for menu definitions. Menu definitions are used to
    ///     declare menu items within the composition model of the framework.
    /// </summary>
    public abstract class MenuDefinitionBase
    {

        /// <summary>
        ///     Returns the sort order of the menu definition.
        /// </summary>
        public abstract int SortOrder { get; }

        /// <summary>
        ///     Returns the display text of the menu definition.
        /// </summary>
        public abstract string Text { get; }

        /// <summary>
        ///     Returns the <see cref="Uri"/> of an icon associated with the menu definition.
        /// </summary>
        public abstract Uri IconSource { get; }

        /// <summary>
        ///     Returns a default <see cref="System.Windows.Input.KeyGesture"/> associated with the menu definition.
        /// </summary>
        public abstract KeyGesture KeyGesture { get; }

        /// <summary>
        ///     Returns the <see cref="CommandDefinitionBase"/> associated with the menu definition.
        /// </summary>
        public abstract CommandDefinitionBase CommandDefinition { get; }
    }
}

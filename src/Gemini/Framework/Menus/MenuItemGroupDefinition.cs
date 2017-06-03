namespace Gemini.Framework.Menus
{
    /// <summary>
    ///     Represents a group of menu items within a menu. Menu item groups are being used
    ///     for hierarchically build up menu structures.
    /// </summary>
    public class MenuItemGroupDefinition
    {
        /// <summary>
        ///     Returns the parent <see cref="MenuDefinitionBase" /> of the group definition.
        /// </summary>
        public MenuDefinitionBase Parent { get; }

        /// <summary>
        ///     Returns the sort order of the group definition.
        /// </summary>
        public int SortOrder { get; }

        /// <summary>
        ///     Creates a new <see cref="MenuItemGroupDefinition" />.
        /// </summary>
        /// <param name="parent">The parent <see cref="MenuDefinitionBase" /> of the group definition.</param>
        /// <param name="sortOrder">The sort order of the group definition.</param>
        public MenuItemGroupDefinition(MenuDefinitionBase parent, int sortOrder)
        {
            Parent = parent;
            SortOrder = sortOrder;
        }
    }
}

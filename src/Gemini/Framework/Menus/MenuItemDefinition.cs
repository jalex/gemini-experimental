namespace Gemini.Framework.Menus
{
    /// <summary>
    ///     Represents a base type for menu item definitions. Menu items are entries within
    ///     a menu.
    /// </summary>
    public abstract class MenuItemDefinition : MenuDefinitionBase
    {
        /// <summary>
        ///     Returns the <see cref="MenuItemGroupDefinition" /> this menu item belongs to.
        /// </summary>
        public MenuItemGroupDefinition Group { get; }

        /// <summary>
        ///     Returns the sort order of the menu definition.
        /// </summary>
        public override int SortOrder { get; }

        /// <summary>
        ///     Creates a new <see cref="MenuItemDefinition" />.
        /// </summary>
        /// <param name="group">The <see cref="MenuItemGroupDefinition" /> this menu item belongs to.</param>
        /// <param name="sortOrder">The sort order of the menu definition.</param>
        protected MenuItemDefinition(MenuItemGroupDefinition group, int sortOrder)
        {
            Group = group;
            SortOrder = sortOrder;
        }
    }
}

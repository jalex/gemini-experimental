namespace Gemini.Framework.Menus
{
    public abstract class MenuItemDefinition : MenuDefinitionBase
    {
        private readonly MenuItemGroupDefinition _group;
        private readonly int _sortOrder;

        public MenuItemGroupDefinition Group => _group;

        public override int SortOrder => _sortOrder;

        protected MenuItemDefinition(MenuItemGroupDefinition group, int sortOrder)
        {
            _group = group;
            _sortOrder = sortOrder;
        }
    }
}
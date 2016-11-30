namespace Gemini.Framework.Menus
{
    public class MenuItemGroupDefinition
    {
        private readonly MenuDefinitionBase _parent;
        private readonly int _sortOrder;

        public MenuDefinitionBase Parent => _parent;

        public int SortOrder => _sortOrder;

        public MenuItemGroupDefinition(MenuDefinitionBase parent, int sortOrder)
        {
            _parent = parent;
            _sortOrder = sortOrder;
        }
    }
}
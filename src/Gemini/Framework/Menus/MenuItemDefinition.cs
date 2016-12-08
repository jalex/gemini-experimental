namespace Gemini.Framework.Menus
{
    public abstract class MenuItemDefinition : MenuDefinitionBase
    {
        public MenuItemGroupDefinition Group { get; }

        public override int SortOrder { get; }

        protected MenuItemDefinition(MenuItemGroupDefinition group, int sortOrder)
        {
            Group = group;
            SortOrder = sortOrder;
        }
    }
}
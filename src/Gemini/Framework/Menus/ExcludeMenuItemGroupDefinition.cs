namespace Gemini.Framework.Menus
{
    public class ExcludeMenuItemGroupDefinition
    {
        private readonly MenuItemGroupDefinition _menuItemGroupDefinitionToExclude;
        public MenuItemGroupDefinition MenuItemGroupDefinitionToExclude => _menuItemGroupDefinitionToExclude;

        public ExcludeMenuItemGroupDefinition(MenuItemGroupDefinition menuItemGroupDefinition)
        {
            _menuItemGroupDefinitionToExclude = menuItemGroupDefinition;
        }
    }
}

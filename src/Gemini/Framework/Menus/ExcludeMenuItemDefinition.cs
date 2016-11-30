namespace Gemini.Framework.Menus
{
    public class ExcludeMenuItemDefinition
    {
        private readonly MenuItemDefinition _menuItemDefinitionToExclude;
        public MenuItemDefinition MenuItemDefinitionToExclude => _menuItemDefinitionToExclude;

        public ExcludeMenuItemDefinition(MenuItemDefinition menuItemDefinition)
        {
            _menuItemDefinitionToExclude = menuItemDefinition;
        }
    }
}

namespace Gemini.Framework.Menus
{
    public class ExcludeMenuDefinition
    {
        private readonly MenuDefinition _menuDefinitionToExclude;
        public MenuDefinition MenuDefinitionToExclude => _menuDefinitionToExclude;

        public ExcludeMenuDefinition(MenuDefinition menuDefinition)
        {
            _menuDefinitionToExclude = menuDefinition;
        }
    }
}

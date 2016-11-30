namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarItemDefinition
    {
        private readonly ToolBarItemDefinition _toolBarItemDefinitionToExclude;
        public ToolBarItemDefinition ToolBarItemDefinitionToExclude => _toolBarItemDefinitionToExclude;

        public ExcludeToolBarItemDefinition(ToolBarItemDefinition toolBarItemDefinition)
        {
            _toolBarItemDefinitionToExclude = toolBarItemDefinition;
        }
    }
}
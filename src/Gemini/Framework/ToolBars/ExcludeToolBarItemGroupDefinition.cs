namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarItemGroupDefinition
    {
        private readonly ToolBarItemGroupDefinition _toolBarItemGroupDefinitionToExclude;
        public ToolBarItemGroupDefinition ToolBarItemGroupDefinitionToExclude => _toolBarItemGroupDefinitionToExclude;

        public ExcludeToolBarItemGroupDefinition(ToolBarItemGroupDefinition toolBarItemGroupDefinition)
        {
            _toolBarItemGroupDefinitionToExclude = toolBarItemGroupDefinition;
        }
    }
}
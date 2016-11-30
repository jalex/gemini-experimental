namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarDefinition
    {
        private readonly ToolBarDefinition _toolBarDefinitionToExclude;
        public ToolBarDefinition ToolBarDefinitionToExclude => _toolBarDefinitionToExclude;

        public ExcludeToolBarDefinition(ToolBarDefinition toolBarDefinition)
        {
            _toolBarDefinitionToExclude = toolBarDefinition;
        }
    }
}
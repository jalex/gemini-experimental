namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarItemGroupDefinition
    {
        public ExcludeToolBarItemGroupDefinition(ToolBarItemGroupDefinition toolBarItemGroupDefinition)
        {
            ToolBarItemGroupDefinitionToExclude = toolBarItemGroupDefinition;
        }

        public ToolBarItemGroupDefinition ToolBarItemGroupDefinitionToExclude { get; }
    }
}
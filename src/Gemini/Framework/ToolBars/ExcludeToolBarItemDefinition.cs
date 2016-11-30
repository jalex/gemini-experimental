namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarItemDefinition
    {
        public ExcludeToolBarItemDefinition(ToolBarItemDefinition toolBarItemDefinition)
        {
            ToolBarItemDefinitionToExclude = toolBarItemDefinition;
        }

        public ToolBarItemDefinition ToolBarItemDefinitionToExclude { get; }
    }
}
namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarDefinition
    {
        public ExcludeToolBarDefinition(ToolBarDefinition toolBarDefinition)
        {
            ToolBarDefinitionToExclude = toolBarDefinition;
        }

        public ToolBarDefinition ToolBarDefinitionToExclude { get; }
    }
}
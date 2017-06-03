﻿namespace Gemini.Framework.ToolBars
{
    public class ExcludeToolBarDefinition
    {
        public ToolBarDefinition ToolBarDefinitionToExclude { get; }

        public ExcludeToolBarDefinition(ToolBarDefinition toolBarDefinition)
        {
            ToolBarDefinitionToExclude = toolBarDefinition;
        }
    }
}

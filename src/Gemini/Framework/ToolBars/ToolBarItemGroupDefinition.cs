﻿namespace Gemini.Framework.ToolBars
{
    public class ToolBarItemGroupDefinition
    {
        public ToolBarItemGroupDefinition(ToolBarDefinition toolBar, int sortOrder)
        {
            ToolBar = toolBar;
            SortOrder = sortOrder;
        }

        public ToolBarDefinition ToolBar { get; }

        public int SortOrder { get; }
    }
}
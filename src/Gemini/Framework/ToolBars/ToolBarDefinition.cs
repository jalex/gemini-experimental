﻿namespace Gemini.Framework.ToolBars
{
    public class ToolBarDefinition
    {
        public ToolBarDefinition(int sortOrder, string name)
        {
            SortOrder = sortOrder;
            Name = name;
        }

        public int SortOrder { get; }

        public string Name { get; }
    }
}
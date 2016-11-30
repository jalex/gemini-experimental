namespace Gemini.Framework.ToolBars
{
    public class ToolBarItemGroupDefinition
    {
        private readonly ToolBarDefinition _toolBar;
        private readonly int _sortOrder;

        public ToolBarDefinition ToolBar => _toolBar;

        public int SortOrder => _sortOrder;

        public ToolBarItemGroupDefinition(ToolBarDefinition toolBar, int sortOrder)
        {
            _toolBar = toolBar;
            _sortOrder = sortOrder;
        }
    }
}
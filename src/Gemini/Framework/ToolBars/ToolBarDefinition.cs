namespace Gemini.Framework.ToolBars
{
    public class ToolBarDefinition
    {
        private readonly int _sortOrder;
        private readonly string _name;

        public int SortOrder => _sortOrder;

        public string Name => _name;

        public ToolBarDefinition(int sortOrder, string name)
        {
            _sortOrder = sortOrder;
            _name = name;
        }
    }
}
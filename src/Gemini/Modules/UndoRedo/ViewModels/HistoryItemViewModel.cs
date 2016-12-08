#region

using Caliburn.Micro;

#endregion

namespace Gemini.Modules.UndoRedo.ViewModels
{
    public class HistoryItemViewModel : PropertyChangedBase
    {
        private readonly string _name;

        public IUndoableAction Action { get; }

        public string Name => _name ?? Action.Name;
        public HistoryItemType ItemType { get; }

        public HistoryItemViewModel(IUndoableAction action, HistoryItemType itemType)
        {
            Action = action;
            ItemType = itemType;
        }

        public HistoryItemViewModel(string name, HistoryItemType itemType)
        {
            _name = name;
            ItemType = itemType;
        }
    }
}
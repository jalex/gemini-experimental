#region

using Caliburn.Micro;
using Gemini.Framework;
using Action = System.Action;

#endregion

namespace Gemini.Modules.ErrorList
{
    public interface IErrorList : ITool
    {
        bool ShowErrors { get; set; }
        bool ShowWarnings { get; set; }
        bool ShowMessages { get; set; }

        IObservableCollection<ErrorListItem> Items { get; }

        void AddItem(ErrorListItemType itemType, string description,
            string path = null, int? line = null, int? column = null,
            Action onClick = null);
    }
}

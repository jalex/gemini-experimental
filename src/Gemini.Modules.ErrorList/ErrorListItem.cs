#region

using Caliburn.Micro;
using Action = System.Action;

#endregion

namespace Gemini.Modules.ErrorList
{
    public class ErrorListItem : PropertyChangedBase
    {
        private int? _column;

        private string _description;
        private ErrorListItemType _itemType;

        private int? _line;

        private int _number;

        private string _path;

        public ErrorListItemType ItemType
        {
            get { return _itemType; }
            set
            {
                _itemType = value;
                NotifyOfPropertyChange(() => ItemType);
            }
        }

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyOfPropertyChange(() => Number);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                NotifyOfPropertyChange(() => _path);
                NotifyOfPropertyChange(nameof(File));
            }
        }

        public string File => System.IO.Path.GetFileName(Path);

        public int? Line
        {
            get { return _line; }
            set
            {
                _line = value;
                NotifyOfPropertyChange(() => Line);
            }
        }

        public int? Column
        {
            get { return _column; }
            set
            {
                _column = value;
                NotifyOfPropertyChange(() => Column);
            }
        }

        public Action OnClick { get; set; }

        public ErrorListItem(ErrorListItemType itemType, int number, string description,
            string path = null, int? line = null, int? column = null)
        {
            _itemType = itemType;
            _number = number;
            _description = description;
            _path = path;
            _line = line;
            _column = column;
        }

        public ErrorListItem()
        {
        }
    }
}

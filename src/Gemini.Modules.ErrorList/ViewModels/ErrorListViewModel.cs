#region

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.ErrorList.Properties;
using Action = System.Action;

#endregion

namespace Gemini.Modules.ErrorList.ViewModels
{
    [Export(typeof(IErrorList))]
    public class ErrorListViewModel : Tool, IErrorList
    {
        private readonly BindableCollection<ErrorListItem> _items;

        private bool _showErrors = true;

        private bool _showMessages = true;

        private bool _showWarnings = true;

        public IEnumerable<ErrorListItem> FilteredItems
        {
            get
            {
                var items = _items.AsEnumerable();
                if (!ShowErrors)
                    items = items.Where(x => x.ItemType != ErrorListItemType.Error);
                if (!ShowWarnings)
                    items = items.Where(x => x.ItemType != ErrorListItemType.Warning);
                if (!ShowMessages)
                    items = items.Where(x => x.ItemType != ErrorListItemType.Message);
                return items;
            }
        }

        public override PaneLocation PreferredLocation => PaneLocation.Bottom;

        public IObservableCollection<ErrorListItem> Items => _items;

        public bool ShowErrors
        {
            get { return _showErrors; }
            set
            {
                _showErrors = value;
                NotifyOfPropertyChange(() => ShowErrors);
                NotifyOfPropertyChange(nameof(FilteredItems));
            }
        }

        public bool ShowWarnings
        {
            get { return _showWarnings; }
            set
            {
                _showWarnings = value;
                NotifyOfPropertyChange(() => ShowWarnings);
                NotifyOfPropertyChange(nameof(FilteredItems));
            }
        }

        public bool ShowMessages
        {
            get { return _showMessages; }
            set
            {
                _showMessages = value;
                NotifyOfPropertyChange(() => ShowMessages);
                NotifyOfPropertyChange(nameof(FilteredItems));
            }
        }

        public ErrorListViewModel()
        {
            DisplayName = Resources.ViewErrorListCommandToolTip;

            ToolBarDefinition = ToolBarDefinitions.ErrorListToolBar;

            _items = new BindableCollection<ErrorListItem>();
            _items.CollectionChanged += (sender, e) =>
            {
                NotifyOfPropertyChange(nameof(FilteredItems));
                NotifyOfPropertyChange("ErrorItemCount");
                NotifyOfPropertyChange("WarningItemCount");
                NotifyOfPropertyChange("MessageItemCount");
            };
        }

        public void AddItem(ErrorListItemType itemType, string description,
            string path = null, int? line = null, int? column = null,
            Action onClick = null)
        {
            Items.Add(new ErrorListItem(itemType, Items.Count + 1, description, path, line, column)
            {
                OnClick = onClick
            });
        }
    }
}

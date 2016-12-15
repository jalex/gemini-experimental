#region

using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Properties;
using Action = System.Action;

#endregion

namespace Gemini.Modules.UndoRedo.ViewModels
{
    [Export(typeof(IHistoryTool))]
    public class HistoryViewModel : Tool, IHistoryTool
    {
        private bool _batchRunning;
        private BindableCollection<HistoryItemViewModel> _historyItems;

        private bool _internallyTriggeredChange;
        private int _selectedIndex;

        private IUndoRedoManager _undoRedoManager;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
                TriggerInternalHistoryChange(() => UndoOrRedoToInternal(HistoryItems[value - 1]));
            }
        }

        public IObservableCollection<HistoryItemViewModel> HistoryItems => _historyItems;

        public override PaneLocation PreferredLocation => PaneLocation.Right;

        public IUndoRedoManager UndoRedoManager
        {
            get { return _undoRedoManager; }
            set
            {
                if (_undoRedoManager != null)
                {
                    _undoRedoManager.UndoStack.CollectionChanged -= OnUndoRedoStackChanged;
                    _undoRedoManager.RedoStack.CollectionChanged -= OnUndoRedoStackChanged;
                    _undoRedoManager.BatchBegin -= OnUndoRedoBatchBegin;
                    _undoRedoManager.BatchEnd -= OnUndoRedoBatchEnd;
                }

                _undoRedoManager = value;
                if (_undoRedoManager != null)
                {
                    _undoRedoManager.UndoStack.CollectionChanged += OnUndoRedoStackChanged;
                    _undoRedoManager.RedoStack.CollectionChanged += OnUndoRedoStackChanged;
                    _undoRedoManager.BatchBegin += OnUndoRedoBatchBegin;
                    _undoRedoManager.BatchEnd += OnUndoRedoBatchEnd;
                }
                RefreshHistory();
            }
        }

        [ImportingConstructor]
        public HistoryViewModel(IShell shell)
        {
            DisplayName = Resources.HistoryDisplayName;

            _historyItems = new BindableCollection<HistoryItemViewModel>();

            if (shell == null)
                return;

            shell.ActiveDocumentChanged +=
                (sender, e) => { UndoRedoManager = shell.SelectedDocument?.UndoRedoManager; };
            if (shell.SelectedDocument != null)
                UndoRedoManager = shell.SelectedDocument.UndoRedoManager;
        }

        private void OnUndoRedoStackChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshHistory();
        }

        private void OnUndoRedoBatchBegin(object sender, EventArgs e)
        {
            _batchRunning = true;
        }

        private void OnUndoRedoBatchEnd(object sender, EventArgs e)
        {
            _batchRunning = false;
            RefreshHistory();
        }

        private void RefreshHistory()
        {
            if (_batchRunning)
                return;

            var historyItems = new BindableCollection<HistoryItemViewModel>();
            if (_undoRedoManager != null)
            {
                historyItems.Add(new HistoryItemViewModel(Resources.HistoryInitialState,
                    _undoRedoManager.UndoStack.Any() ? HistoryItemType.InitialState : HistoryItemType.Current));
                for (var i = 0; i < _undoRedoManager.UndoStack.Count; i++)
                    historyItems.Add(new HistoryItemViewModel(_undoRedoManager.UndoStack[i],
                        i == _undoRedoManager.UndoStack.Count - 1 ? HistoryItemType.Current : HistoryItemType.Undo));
                for (var i = _undoRedoManager.RedoStack.Count - 1; i >= 0; i--)
                    historyItems.Add(new HistoryItemViewModel(
                        _undoRedoManager.RedoStack[i],
                        HistoryItemType.Redo));
            }
            _historyItems = historyItems;
            NotifyOfPropertyChange(() => HistoryItems);

            if (!_internallyTriggeredChange)
                UpdateSelectedIndexOnly(
                    _historyItems.IndexOf(_historyItems.FirstOrDefault(x => x.ItemType == HistoryItemType.Current)) + 1);
        }

        public void UndoOrRedoTo(HistoryItemViewModel item)
        {
            TriggerInternalHistoryChange(() => UndoOrRedoToInternal(item));
            UpdateSelectedIndexOnly(_historyItems.IndexOf(_historyItems.First(x => x.Action == item.Action)) + 1);
        }

        private void TriggerInternalHistoryChange(Action callback)
        {
            _internallyTriggeredChange = true;
            callback();
            _internallyTriggeredChange = false;
        }

        private void UpdateSelectedIndexOnly(int selectedIndex)
        {
            _selectedIndex = selectedIndex;
            NotifyOfPropertyChange(() => SelectedIndex);
        }

        private void UndoOrRedoToInternal(HistoryItemViewModel item)
        {
            switch (item.ItemType)
            {
                case HistoryItemType.InitialState:
                    _undoRedoManager.UndoAll();
                    break;
                case HistoryItemType.Undo:
                    _undoRedoManager.UndoTo(item.Action);
                    break;
                case HistoryItemType.Current:
                    break;
                case HistoryItemType.Redo:
                    _undoRedoManager.RedoTo(item.Action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
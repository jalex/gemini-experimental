#region

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.ToolBars;
using Gemini.Modules.Shell.Commands;
using Gemini.Modules.ToolBars;
using Gemini.Modules.ToolBars.Models;
using Gemini.Modules.UndoRedo;
using Gemini.Modules.UndoRedo.Commands;
using Gemini.Modules.UndoRedo.Services;
using Gemini.Properties;
using Microsoft.Win32;

#endregion

namespace Gemini.Framework
{
    public abstract class Document : LayoutItemBase, IDocument,
        ICommandHandler<UndoCommandDefinition>,
        ICommandHandler<RedoCommandDefinition>,
        ICommandHandler<SaveFileCommandDefinition>,
        ICommandHandler<SaveFileAsCommandDefinition>
    {
        private ICommand _closeCommand;

        private IToolBar _toolBar;

        private ToolBarDefinition _toolBarDefinition;
        private IUndoRedoManager _undoRedoManager;

        public ToolBarDefinition ToolBarDefinition
        {
            get { return _toolBarDefinition; }
            protected set
            {
                _toolBarDefinition = value;
                NotifyOfPropertyChange(() => ToolBar);
                NotifyOfPropertyChange();
            }
        }

        public IToolBar ToolBar
        {
            get
            {
                if (_toolBar != null)
                    return _toolBar;

                if (ToolBarDefinition == null)
                    return null;

                var toolBarBuilder = IoC.Get<IToolBarBuilder>();
                _toolBar = new ToolBarModel();
                toolBarBuilder.BuildToolBar(ToolBarDefinition, _toolBar);
                return _toolBar;
            }
        }

        protected virtual IUndoRedoManager CreateUndoRedoManager() {
            return new UndoRedoManager();
        }

        public IUndoRedoManager UndoRedoManager => _undoRedoManager ?? (_undoRedoManager = CreateUndoRedoManager());

        public override ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(p => TryClose(null), p => true)); }
        }

        void ICommandHandler<RedoCommandDefinition>.Update(Command command)
        {
            command.Enabled = UndoRedoManager.RedoStack.Any();
        }

        Task ICommandHandler<RedoCommandDefinition>.Run(Command command)
        {
            UndoRedoManager.Redo(1);
            return Task.CompletedTask;
        }

        void ICommandHandler<SaveFileAsCommandDefinition>.Update(Command command)
        {
            command.Enabled = this is IPersistedDocument;
        }

        async Task ICommandHandler<SaveFileAsCommandDefinition>.Run(Command command)
        {
            var persistedDocument = this as IPersistedDocument;
            if (persistedDocument == null)
                return;

            await DoSaveAs(persistedDocument);
        }

        void ICommandHandler<SaveFileCommandDefinition>.Update(Command command)
        {
            var persistedDocument = this as IPersistedDocument;
            command.Enabled = (persistedDocument != null) && persistedDocument.IsDirty;
        }

        async Task ICommandHandler<SaveFileCommandDefinition>.Run(Command command)
        {
            var persistedDocument = this as IPersistedDocument;
            if (persistedDocument == null)
                return;

            // If file has never been saved, show Save As dialog.
            if (persistedDocument.IsNew)
            {
                await DoSaveAs(persistedDocument);
                return;
            }

            // Save file.
            var filePath = persistedDocument.FilePath;
            await persistedDocument.Save(filePath);
        }

        void ICommandHandler<UndoCommandDefinition>.Update(Command command)
        {
            command.Enabled = UndoRedoManager.UndoStack.Any();
        }

        Task ICommandHandler<UndoCommandDefinition>.Run(Command command)
        {
            UndoRedoManager.Undo(1);
            return Task.CompletedTask;
        }

        private static async Task DoSaveAs(IPersistedDocument persistedDocument) {
            // Show user dialog to choose filename.
            var dialog = new SaveFileDialog { FileName = persistedDocument.FileName };
            var filter = string.Empty;

            var fileExtension = Path.GetExtension(persistedDocument.FileName);
            var fileTypes = IoC.GetAll<IEditorProvider>()
                .Where(ep => ep.FileTypes.Any(ft => ft.FileExtension.Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase)))
                .SelectMany(x => x.FileTypes)
                .ToList();
            if(fileTypes.Count > 0) filter = string.Join("|", fileTypes.Select(ft => $"{ft.Name}|*{ft.FileExtension}")) + "|";

            filter += Resources.AllFiles + "|*.*";
            dialog.Filter = filter;

            var filterIndex = fileTypes.FindIndex(ft => ft.FileExtension.Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase));
            dialog.FilterIndex = filterIndex < 0 ? 1 : (filterIndex + 1);

            if(dialog.ShowDialog() != true) return;

            var filePath = dialog.FileName;

            // Save file.
            await persistedDocument.Save(filePath);

            // Add to recent files
            var shell = IoC.Get<IShell>();
            shell.RecentFiles.Update(filePath);
        }

    }
}
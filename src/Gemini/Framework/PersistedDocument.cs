#region

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Properties;
using Microsoft.Win32;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Represents a base implementation of a <see cref="IPersistedDocument" />.
    /// </summary>
    public abstract class PersistedDocument : Document, IPersistedDocument
    {
        private string _fileName;
        private string _filePath;
        private bool _isDirty;

        /// <summary>
        ///     Returns whether the document is currently in a transient state.
        /// </summary>
        public bool IsNew { get; private set; }

        /// <summary>
        ///     Returns the file name of the document.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            private set
            {
                if (value == _fileName) return;
                _fileName = value;
                NotifyOfPropertyChange(nameof(ToolTip));
            }
        }

        /// <summary>
        ///     Returns the full file path of the document.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            private set
            {
                if (value == _filePath) return;
                _filePath = value;
                NotifyOfPropertyChange(nameof(ToolTip));
            }
        }

        /// <summary>
        ///     Returns a tooltip associated with the panel.
        /// </summary>
        /// <remarks>Defaults to <see cref="IHaveDisplayName.DisplayName" /> if not specified.</remarks>
        public override string ToolTip => FilePath ?? FileName;

        /// <summary>
        ///     Returns whether the document has unpersisted changes.
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (value == _isDirty)
                    return;

                _isDirty = value;
                NotifyOfPropertyChange(() => IsDirty);
                UpdateDisplayName();
            }
        }

        // ShouldReopenOnStart, SaveState and LoadState are default methods of PersistedDocument.
        /// <summary>
        ///     Returns whether the panel should be re-opened when the application starts.
        /// </summary>
        public override bool ShouldReopenOnStart => FilePath != null;

        /// <summary>
        ///     Asynchronously saves the state of the panel using the specified <see cref="BinaryWriter" />.
        /// </summary>
        /// <param name="writer">A <see cref="BinaryWriter" /> for persisting the state.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        /// <exception cref="IOException">An I/O error occurs. </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer" /> is null.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The stream is closed. </exception>
        public override Task SaveState(BinaryWriter writer)
        {
            writer.Write(FilePath);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Asynchronously loads the state of the panel using the specified <see cref="BinaryReader" />.
        /// </summary>
        /// <param name="reader">A <see cref="BinaryReader" /> for reading the state.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached. </exception>
        /// <exception cref="IOException">An I/O error occurs. </exception>
        /// <exception cref="ObjectDisposedException">The stream is closed. </exception>
        public override async Task LoadState(BinaryReader reader) => await Load(reader.ReadString());

        /// <summary>
        ///     Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="callback">The implementor calls this action with the result of the close check.</param>
        public override async void CanClose(Action<bool> callback)
        {
            try { 
                if (IsDirty)
                {
                    // Show save prompt.
                    // Note that CanClose method of Demo ShellViewModel blocks this.
                    var title = IoC.Get<IMainWindow>().Title;
                    var fileName = Path.GetFileNameWithoutExtension(FileName);
                    var fileExtension = Path.GetExtension(FileName);
                    var fileType = IoC.GetAll<IEditorProvider>()
                        .SelectMany(x => x.FileTypes)
                        .SingleOrDefault(x => x.FileExtension == fileExtension);
                    if (fileType==null) {
                            callback(true);
                            return;
                        }

                    var message = string.Format(Resources.SaveChangesBeforeClosingMessage, fileType.Name, fileName);
                    var result = MessageBox.Show(message, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        if (IsNew)
                        {
                            // Ask new file path.
                            var filter = string.Empty;
                            filter = fileType.Name + "|*" + fileType.FileExtension + "|";
                            filter += Resources.AllFiles + "|*.*";

                            var dialog = new SaveFileDialog {FileName = FileName, Filter = filter};
                            if (dialog.ShowDialog() == true)
                            {
                                // Save file.
                                await Save(dialog.FileName);

                                // Add to recent files. Temporally, commented out.
                                //IShell _shell = IoC.Get<IShell>();
                                //_shell.RecentFiles.Update(dialog.FileName);
                            }
                            else
                            {
                                callback(false);
                                return;
                            }
                        }
                        else
                        {
                            // Save file.
                            await Save(FilePath);
                        }
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        callback(false);
                        return;
                    }
                }
            } catch {
            }
            callback(true);
        }

        /// <summary>
        ///     Asynchronously a new document content using the specified file name.
        /// </summary>
        /// <param name="fileName">The file name of the document.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        public async Task New(string fileName)
        {
            FileName = fileName;
            UpdateDisplayName();

            IsNew = true;
            IsDirty = false;

            await DoNew();
        }

        /// <summary>
        ///     Loads the contents of a file into the document using the specified file path.
        /// </summary>
        /// <param name="filePath">The path of a file to load.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        public async Task Load(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            UpdateDisplayName();

            IsNew = false;
            IsDirty = false;

            await DoLoad(filePath);
        }

        /// <summary>
        ///     Saves the contents of the the document into a file using the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to save.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        public async Task Save(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            UpdateDisplayName();

            await DoSave(filePath);

            IsDirty = false;
            IsNew = false;
        }

        private void UpdateDisplayName()
        {
            DisplayName = IsDirty ? FileName + "*" : FileName;
        }

        /// <summary>
        ///     Invoked when creating a new document.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        protected abstract Task DoNew();

        /// <summary>
        ///     Invoked when loading the contents of a file into the document.
        /// </summary>
        /// <param name="filePath">The path of a file to load.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        protected abstract Task DoLoad(string filePath);

        /// <summary>
        ///     Invoked when persisting the contents of the document into a file.
        /// </summary>
        /// <param name="filePath">The path of the file to save.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        protected abstract Task DoSave(string filePath);
    }
}

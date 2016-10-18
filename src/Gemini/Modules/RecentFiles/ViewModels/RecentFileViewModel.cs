using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Gemini.Modules.RecentFiles.ViewModels
{
    [Export(typeof(IRecentFiles))]
    public class RecentFileViewModel : PropertyChangedBase, IRecentFiles
    {
        public RecentFileViewModel()
        {
            InitializeList();
        }

        public IObservableCollection<RecentFileItemViewModel> Items { get { return Properties.Settings.Default.RecentDocuments; } }

        public void Update(string filePath)
        {
            Properties.Settings.Default.RecentDocuments.Update(filePath);
            SaveList();
        }

        public void Remove(string filePath)
        {
            var index = Properties.Settings.Default.RecentDocuments.FindIndex(filePath);
            if (index >= 0)
            {
                Properties.Settings.Default.RecentDocuments.RemoveAt(index);
                SaveList();
            }
        }

        private void InitializeList()
        {
            // Create a new collection if it was not serialized before.
            if (Properties.Settings.Default.RecentDocuments == null)
                Properties.Settings.Default.RecentDocuments = new RecentFileItemCollection();
            NotifyOfPropertyChange(() => Items);
        }

        private void SaveList()
        {
            Properties.Settings.Default.Save();
            NotifyOfPropertyChange(() => Items);
        }

        [Serializable]
        public sealed class RecentFileItemCollection : BindableCollection<RecentFileItemViewModel>
        {
            public int FindIndex(string filePath)
            {
                for (var i = 0; i < Count; ++i)
                    if (string.Equals(this[i].FilePath, filePath, StringComparison.OrdinalIgnoreCase))
                        return i;
                return -1;
            }

            /// <summary>
            ///     Adds or moves the file to the top of the list.
            /// </summary>
            /// <param name="filePath"></param>
            public void Update(string filePath)
            {
                var i = FindIndex(filePath);
                if (i >= 0)
                {
                    if (this[i].Pinned)
                        return;
                    RemoveAt(i);
                }
                var recentFileItem = new RecentFileItemViewModel
                {
                    FilePath = filePath
                };
                InsertItem(0, recentFileItem);
            }
        }
    }
}
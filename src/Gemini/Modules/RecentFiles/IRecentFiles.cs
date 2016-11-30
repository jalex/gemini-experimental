#region

using Caliburn.Micro;
using Gemini.Modules.RecentFiles.ViewModels;

#endregion

namespace Gemini.Modules.RecentFiles
{
    public interface IRecentFiles
    {
        IObservableCollection<RecentFileItemViewModel> Items { get; }
        void Update(string filePath);
    }
}
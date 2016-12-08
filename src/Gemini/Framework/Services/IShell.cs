#region

using System;
using Caliburn.Micro;
using Gemini.Modules.MainMenu;
using Gemini.Modules.RecentFiles;
using Gemini.Modules.StatusBar;
using Gemini.Modules.ToolBars;

#endregion

namespace Gemini.Framework.Services
{
    public interface IShell : IGuardClose, IDeactivate
    {
        bool ShowFloatingWindowsInTaskbar { get; set; }

        IMenu MainMenu { get; }
        IToolBars ToolBars { get; }
        IStatusBar StatusBar { get; }
        IRecentFiles RecentFiles { get; }

        ILayoutPanel ActivePanel { get; set; }

        IDocument SelectedDocument { get; }

        IObservableCollection<IDocument> Documents { get; }
        IObservableCollection<ITool> Tools { get; }
        event EventHandler ActiveDocumentChanging;
        event EventHandler ActiveDocumentChanged;

        void ShowTool<TTool>() where TTool : ITool;
        void ShowTool(ITool model);

        bool TryActivateDocumentByPath(string path);
        void OpenDocument(IDocument model);
        void CloseDocument(IDocument document);

        void Close();
    }
}

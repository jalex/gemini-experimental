#region

using System;
using System.Collections.Generic;
using System.IO;
using Gemini.Framework;

#endregion

namespace Gemini.Modules.Shell.Views
{
    public interface IShellView
    {
        Xceed.Wpf.AvalonDock.DockingManager Manager { get; }

        void LoadLayout(Stream stream, Action<ITool> addToolCallback, Action<IDocument> addDocumentCallback,
            Dictionary<string, ILayoutPanel> itemsState);

        void SaveLayout(Stream stream);

        void UpdateFloatingWindows();
    }
}

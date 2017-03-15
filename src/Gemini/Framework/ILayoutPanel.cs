#region

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework
{
    public interface ILayoutPanel : IScreen
    {
        Guid Id { get; }
        string ToolTip { get; }
        string ContentId { get; }
        ICommand CloseCommand { get; }
        Uri IconSource { get; }
        bool IsSelected { get; set; }
        bool ShouldReopenOnStart { get; }
        Task LoadState(BinaryReader reader);
        Task SaveState(BinaryWriter writer);
    }
}
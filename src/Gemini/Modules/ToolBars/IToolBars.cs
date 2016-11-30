#region

using Caliburn.Micro;

#endregion

namespace Gemini.Modules.ToolBars
{
    public interface IToolBars
    {
        IObservableCollection<IToolBar> Items { get; }
        bool Visible { get; set; }
        bool Locked { get; set; }
    }
}
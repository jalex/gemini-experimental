#region

using Caliburn.Micro;
using Gemini.Modules.ToolBars.Models;

#endregion

namespace Gemini.Modules.ToolBars
{
    public interface IToolBar : IObservableCollection<ToolBarItemBase>
    {
    }
}
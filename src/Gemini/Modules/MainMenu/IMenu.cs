#region

using Caliburn.Micro;
using Gemini.Modules.MainMenu.Models;

#endregion

namespace Gemini.Modules.MainMenu
{
    public interface IMenu : IObservableCollection<MenuItemBase>
    {
    }
}
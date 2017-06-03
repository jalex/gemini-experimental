#region

using Gemini.Framework.Menus;
using Gemini.Modules.MainMenu.Models;

#endregion

namespace Gemini.Modules.MainMenu
{
    public interface IMenuBuilder
    {
        void BuildMenuBar(MenuBarDefinition menuBarDefinition, MenuModel result);
    }
}

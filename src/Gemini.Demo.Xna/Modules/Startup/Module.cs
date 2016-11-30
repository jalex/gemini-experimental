#region

using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Menus;
using Gemini.Modules.MainMenu;

#endregion

namespace Gemini.Demo.Xna.Modules.Startup
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        [Export] public static MenuDefinition DemosMenu = new MenuDefinition(
            MenuDefinitions.MainMenuBar, 5, "De_mos");

        [Export] public static MenuItemGroupDefinition DemosMenuGroup = new MenuItemGroupDefinition(
            DemosMenu, 0);

        public override void Initialize()
        {
            MainWindow.Title = "Gemini XNA Demo";
        }
    }
}
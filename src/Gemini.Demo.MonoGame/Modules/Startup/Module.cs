#region

using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Menus;
using Gemini.Modules.MainMenu;

#endregion

namespace Gemini.Demo.MonoGame.Modules.Startup
{
    /// <summary>
    ///     Represents the startup <see cref="IModule" /> of the demo.
    /// </summary>
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        /// <summary>
        ///     Specifies the <see cref="MenuDefinition" /> for the demo menu.
        /// </summary>
        [Export] public static MenuDefinition DemosMenu = new MenuDefinition(
            MenuDefinitions.MainMenuBar, 5, "De_mos");

        /// <summary>
        ///     Specifies the <see cref="MenuItemGroupDefinition" /> for the demo menu.
        /// </summary>
        [Export] public static MenuItemGroupDefinition DemosMenuGroup = new MenuItemGroupDefinition(
            DemosMenu, 0);

        /// <summary>
        ///     Invoked during the initialization stage of the module.
        /// </summary>
        public override void Initialize()
        {
            MainWindow.Title = "Gemini MonoGame Demo";
        }
    }
}

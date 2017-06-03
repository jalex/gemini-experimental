#region

using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using Gemini.Modules.Output.Commands;

#endregion

namespace Gemini.Modules.Output
{
    public static class MenuDefinitions
    {
        [Export] public static MenuItemDefinition ViewOutputMenuItem = new CommandMenuItemDefinition
            <ViewOutputCommandDefinition>(
                MainMenu.MenuDefinitions.ViewToolsMenuGroup, 1);
    }
}

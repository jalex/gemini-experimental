#region

using System.ComponentModel.Composition;
using Gemini.Demo.Xna.Modules.PrimitiveList.Commands;
using Gemini.Demo.Xna.Modules.Startup;
using Gemini.Framework.Menus;

#endregion

namespace Gemini.Demo.Xna.Modules.PrimitiveList
{
    public static class MenuDefinitions
    {
        [Export] public static MenuItemDefinition ViewPrimitiveListMenuItem = new CommandMenuItemDefinition
            <ViewPrimitiveListCommandDefinition>(
                Module.DemosMenuGroup, 0);
    }
}
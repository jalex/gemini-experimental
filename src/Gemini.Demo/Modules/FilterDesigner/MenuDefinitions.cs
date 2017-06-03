#region

using System.ComponentModel.Composition;
using Gemini.Demo.Modules.FilterDesigner.Commands;
using Gemini.Framework.Menus;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner
{
    /// <summary>
    ///     Represents the menu definitions for the filter designer.
    /// </summary>
    public static class MenuDefinitions
    {
        /// <summary>
        ///     Specifies the <see cref="CommandMenuItemDefinition{TCommandDefinition}" /> for the
        ///     <see cref="OpenGraphCommandDefinition" />.
        /// </summary>
        [Export] public static MenuItemDefinition OpenGraphMenuItem = new CommandMenuItemDefinition
            <OpenGraphCommandDefinition>(
                Gemini.Modules.MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 2);
    }
}

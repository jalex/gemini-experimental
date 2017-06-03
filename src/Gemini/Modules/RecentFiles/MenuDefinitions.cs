#region

using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using Gemini.Modules.RecentFiles.Commands;

#endregion

namespace Gemini.Modules.RecentFiles
{
    public static class MenuDefinitions
    {
        [Export] public static MenuItemDefinition FileRecentFilesMenuItem = new CommandMenuItemDefinition
            <RecentFilesCommandDefinition>(
                MainMenu.MenuDefinitions.FileOpenRecentMenuGroup, 0);

        [Export] public static MenuItemGroupDefinition FileRecentFilesCascadeGroup = new MenuItemGroupDefinition(
            FileRecentFilesMenuItem, 0);

        [Export] public static MenuItemDefinition FileOpenRecentMenuItemList = new CommandMenuItemDefinition
            <OpenRecentFileCommandListDefinition>(
                FileRecentFilesCascadeGroup, 0);
    }
}

#region

using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.MainMenu
{
#if false
    public static class MenuDefinitions
    {
        [Export] public static MenuBarDefinition MainMenuBar = new MenuBarDefinition();

        [Export] public static MenuDefinition FileMenu = new MenuDefinition(MainMenuBar, 0, Resources.FileMenuText);

        [Export] public static MenuItemGroupDefinition FileNewOpenMenuGroup = new MenuItemGroupDefinition(FileMenu, 0);

        [Export] public static MenuItemGroupDefinition FileCloseMenuGroup = new MenuItemGroupDefinition(FileMenu, 3);

        [Export] public static MenuItemGroupDefinition FileSaveMenuGroup = new MenuItemGroupDefinition(FileMenu, 6);

        [Export] public static MenuItemGroupDefinition FileOpenRecentMenuGroup =
            new MenuItemGroupDefinition(FileMenu, 9);

        [Export]
        public static MenuItemGroupDefinition FileExitOpenMenuGroup = new MenuItemGroupDefinition(FileMenu, 10);

        [Export] public static MenuDefinition EditMenu = new MenuDefinition(MainMenuBar, 1, Resources.EditMenuText);

        [Export] public static MenuItemGroupDefinition EditUndoRedoMenuGroup = new MenuItemGroupDefinition(EditMenu, 0);

        [Export] public static MenuDefinition ViewMenu = new MenuDefinition(MainMenuBar, 2, Resources.ViewMenuText);

        [Export] public static MenuItemGroupDefinition ViewToolsMenuGroup = new MenuItemGroupDefinition(ViewMenu, 0);

        [Export] public static MenuItemGroupDefinition ViewPropertiesMenuGroup = new MenuItemGroupDefinition(ViewMenu,
            100);

        [Export] public static MenuDefinition ToolsMenu = new MenuDefinition(MainMenuBar, 10, Resources.ToolsMenuText);

        [Export] public static MenuItemGroupDefinition ToolsOptionsMenuGroup = new MenuItemGroupDefinition(ToolsMenu,
            100);

        [Export]
        public static MenuDefinition WindowMenu = new MenuDefinition(MainMenuBar, 20, Resources.WindowMenuText);

        [Export] public static MenuItemGroupDefinition WindowDocumentListMenuGroup =
            new MenuItemGroupDefinition(WindowMenu, 10);

        [Export] public static MenuDefinition HelpMenu = new MenuDefinition(MainMenuBar, 30, Resources.HelpMenuText);
    }
#endif

    public static class MenuDefinitions
    {
        [Export] public static MenuBarDefinition MainMenuBar = new MenuBarDefinition();

        [Export] public static MenuDefinition ServiceMenu = new MenuDefinition(MainMenuBar, 0, "Сервисное Меню");

        //[Export] public static MenuDefinition FileMenu = new MenuDefinition(MainMenuBar, 0, Resources.FileMenuText);

        [Export] public static MenuItemGroupDefinition FileNewOpenMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 0);

        [Export] public static MenuItemGroupDefinition FileCloseMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 3);

        [Export] public static MenuItemGroupDefinition FileSaveMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 6);

        [Export] public static MenuItemGroupDefinition FileOpenRecentMenuGroup =new MenuItemGroupDefinition(ServiceMenu, 9);

        [Export] public static MenuItemGroupDefinition FileExitOpenMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 10);

        //[Export] public static MenuDefinition EditMenu = new MenuDefinition(ServiceMenu, 1, Resources.EditMenuText);

        [Export] public static MenuItemGroupDefinition EditUndoRedoMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 0);

       // [Export] public static MenuDefinition ViewMenu = new MenuDefinition(ServiceMenu, 2, Resources.ViewMenuText);

        [Export] public static MenuItemGroupDefinition ViewToolsMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 0);

        [Export] public static MenuItemGroupDefinition ViewPropertiesMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 100);

        //[Export] public static MenuDefinition ToolsMenu = new MenuDefinition(ServiceMenu, 10, Resources.ToolsMenuText);

        [Export] public static MenuItemGroupDefinition ToolsOptionsMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 100);

        //[Export] public static MenuDefinition WindowMenu = new MenuDefinition(ServiceMenu, 20, Resources.WindowMenuText);

        [Export] public static MenuItemGroupDefinition WindowDocumentListMenuGroup = new MenuItemGroupDefinition(ServiceMenu, 10);

        //[Export] public static MenuDefinition HelpMenu = new MenuDefinition(ServiceMenu, 30, Resources.HelpMenuText);
    }
}

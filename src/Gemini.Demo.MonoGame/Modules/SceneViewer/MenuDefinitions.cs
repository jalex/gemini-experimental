#region

using System.ComponentModel.Composition;
using Gemini.Demo.MonoGame.Modules.SceneViewer.Commands;
using Gemini.Framework.Menus;

#endregion

namespace Gemini.Demo.MonoGame.Modules.SceneViewer
{
    /// <summary>
    ///     Represents the menu definitions of the Scene Viewer.
    /// </summary>
    public static class MenuDefinitions
    {
        /// <summary>
        ///     Specifies the <see cref="CommandMenuItemDefinition{TCommandDefinition}"/> for the
        ///     <see cref="ViewSceneViewerCommandDefinition"/>.
        /// </summary>
        [Export] public static readonly MenuItemDefinition ViewSceneViewerMenuItem = new CommandMenuItemDefinition
            <ViewSceneViewerCommandDefinition>(
                Startup.Module.DemosMenuGroup, 1);
    }
}

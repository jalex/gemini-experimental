#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Demo.MonoGame.Modules.SceneViewer.Commands
{
    /// <summary>
    ///     Represents a <see cref="CommandDefinition"/> for opening the SceneViewer.
    /// </summary>
    [CommandDefinition]
    public class ViewSceneViewerCommandDefinition : CommandDefinition
    {
        /// <summary>
        ///     Specifies the name of the command.
        /// </summary>
        public const string CommandName = "Demos.SceneViewer";

        /// <summary>
        ///     Returns the name of the command.
        /// </summary>
        public override string Name => CommandName;

        /// <summary>
        ///     Returns the display text of the command.
        /// </summary>
        public override string Text => "_3D Scene";

        /// <summary>
        ///     Returns a tooltip associated with the command.
        /// </summary>
        public override string ToolTip => "3D Scene";
    }
}

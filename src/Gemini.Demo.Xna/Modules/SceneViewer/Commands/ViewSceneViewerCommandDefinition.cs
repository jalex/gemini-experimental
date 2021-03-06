﻿#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Demo.Xna.Modules.SceneViewer.Commands
{
    [CommandDefinition]
    public class ViewSceneViewerCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Demos.SceneViewer";

        public override string Name => CommandName;

        public override string Text => "_3D Scene";

        public override string ToolTip => "3D Scene";
    }
}

using Gemini.Framework.Commands;
using Gemini.Properties;

namespace Gemini.Modules.Settings.Commands
{
    [CommandDefinition]
    public class OpenSettingsCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Tools.Options";

        public override string Name => CommandName;

        public override string Text => Resources.ToolsOptionsCommandText;

        public override string ToolTip => Resources.ToolsOptionsCommandToolTip;
    }
}
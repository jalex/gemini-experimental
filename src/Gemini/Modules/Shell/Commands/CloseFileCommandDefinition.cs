#region

using Gemini.Framework.Commands;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandDefinition]
    public class CloseFileCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.CloseFile";

        public override string Name => CommandName;

        public override string Text => Resources.FileCloseCommandText;

        public override string ToolTip => Resources.FileCloseCommandToolTip;
    }
}

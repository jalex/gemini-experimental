#region

using Gemini.Framework.Commands;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandDefinition]
    public class SaveFileAsCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.SaveFileAs";

        public override string Name => CommandName;

        public override string Text => Resources.FileSaveAsCommandText;

        public override string ToolTip => Resources.FileSaveAsCommandToolTip;
    }
}

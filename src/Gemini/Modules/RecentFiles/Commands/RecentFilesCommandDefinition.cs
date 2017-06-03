#region

using Gemini.Framework.Commands;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.RecentFiles.Commands
{
    [CommandDefinition]
    public class RecentFilesCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.RecentFiles";

        public override string Name => CommandName;

        public override string Text => Resources.FileRecentFilesCommandText;

        public override string ToolTip => Resources.FileRecentFilesCommandToolTip;
    }
}

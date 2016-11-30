using Gemini.Framework.Commands;
using Gemini.Properties;

namespace Gemini.Modules.UndoRedo.Commands
{
    [CommandDefinition]
    public class ViewHistoryCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.History";

        public override string Name => CommandName;

        public override string Text => Resources.ViewHistoryCommandText;

        public override string ToolTip => Resources.ViewHistoryCommandToolTip;
    }
}
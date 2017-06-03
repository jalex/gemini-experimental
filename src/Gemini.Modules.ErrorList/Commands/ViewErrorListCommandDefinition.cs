#region

using Gemini.Framework.Commands;
using Gemini.Modules.ErrorList.Properties;

#endregion

namespace Gemini.Modules.ErrorList.Commands
{
    [CommandDefinition]
    public class ViewErrorListCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.ErrorList";

        public override string Name => CommandName;

        public override string Text => Resources.ViewErrorListCommandText;

        public override string ToolTip => Resources.ViewErrorListCommandToolTip;
    }
}

using Gemini.Framework.Commands;
using Gemini.Properties;

namespace Gemini.Modules.Toolbox.Commands
{
    [CommandDefinition]
    public class ViewToolboxCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.Toolbox";

        public override string Name => CommandName;

        public override string Text => Resources.ViewToolboxCommandText;

        public override string ToolTip => Resources.ViewToolboxCommandToolTip;
    }
}
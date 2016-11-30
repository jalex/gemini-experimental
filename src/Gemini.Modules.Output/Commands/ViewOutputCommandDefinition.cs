using Gemini.Framework.Commands;
using Gemini.Modules.Output.Properties;

namespace Gemini.Modules.Output.Commands
{
    [CommandDefinition]
    public class ViewOutputCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.Output";

        public override string Name => CommandName;

        public override string Text => Resources.ViewOutputCommandText;

        public override string ToolTip => Resources.ViewOutputCommandToolTip;
    }
}
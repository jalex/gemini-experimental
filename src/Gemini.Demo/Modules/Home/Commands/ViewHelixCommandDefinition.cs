using Gemini.Framework.Commands;

namespace Gemini.Demo.Modules.Home.Commands
{
    [CommandDefinition]
    public class ViewHelixCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.Helix";

        public override string Name => CommandName;

        public override string Text => "Helix";

        public override string ToolTip => "Helix";
    }
}
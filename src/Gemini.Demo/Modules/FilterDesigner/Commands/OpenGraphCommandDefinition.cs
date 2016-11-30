#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.Commands
{
    [CommandDefinition]
    public class OpenGraphCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.OpenGraph";

        public override string Name => CommandName;

        public override string Text => "Open Graph";

        public override string ToolTip => "Open Graph";
    }
}
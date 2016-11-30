#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Demo.Modules.Home.Commands
{
    [CommandDefinition]
    public class ViewHomeCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.Home";

        public override string Name => CommandName;

        public override string Text => "Home";

        public override string ToolTip => "Home";
    }
}
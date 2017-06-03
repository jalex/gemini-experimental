#region

using Gemini.Framework.Commands;
using Gemini.Modules.Inspector.Properties;

#endregion

namespace Gemini.Modules.Inspector.Commands
{
    [CommandDefinition]
    public class ViewInspectorCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.Inspector";

        public override string Name => CommandName;

        public override string Text => Resources.ViewInspectorCommandText;

        public override string ToolTip => Resources.ViewInspectorCommandToolTip;
    }
}

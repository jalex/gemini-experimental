#region

using Gemini.Framework.Commands;
using Gemini.Modules.PropertyGrid.Properties;

#endregion

namespace Gemini.Modules.PropertyGrid.Commands
{
    [CommandDefinition]
    public class ViewPropertyGridCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.PropertiesWindow";

        public override string Name => CommandName;

        public override string Text => Resources.ViewPropertyGridCommandText;

        public override string ToolTip => Resources.ViewPropertyGridCommandToolTip;
    }
}

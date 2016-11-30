#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Demo.Xna.Modules.PrimitiveList.Commands
{
    [CommandDefinition]
    public class ViewPrimitiveListCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Demos.PrimitiveList";

        public override string Name => CommandName;

        public override string Text => "_Primitive List";

        public override string ToolTip => "Primitive List";
    }
}
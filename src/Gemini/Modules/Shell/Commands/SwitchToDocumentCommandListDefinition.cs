#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandDefinition]
    public class SwitchToDocumentCommandListDefinition : CommandListDefinition
    {
        public const string CommandName = "Window.SwitchToDocument";

        public override string Name => CommandName;
    }
}

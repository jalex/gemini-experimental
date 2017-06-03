#region

using Gemini.Framework.Commands;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandDefinition]
    public class NewFileCommandListDefinition : CommandListDefinition
    {
        public const string CommandName = "File.NewFile";

        public override string Name => CommandName;
    }
}

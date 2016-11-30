#region

using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandDefinition]
    public class ExitCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.Exit";

        [Export] public static CommandKeyboardShortcut KeyGesture =
            new CommandKeyboardShortcut<ExitCommandDefinition>(new KeyGesture(Key.F4, ModifierKeys.Alt));

        public override string Name => CommandName;

        public override string Text => Resources.FileExitCommandText;

        public override string ToolTip => Resources.FileExitCommandToolTip;
    }
}
#region

using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandDefinition]
    public class OpenFileCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.OpenFile";

        [Export] public static CommandKeyboardShortcut KeyGesture =
            new CommandKeyboardShortcut<OpenFileCommandDefinition>(new KeyGesture(Key.O, ModifierKeys.Control));

        public override string Name => CommandName;

        public override string Text => Resources.FileOpenCommandText;

        public override string ToolTip => Resources.FileOpenCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/Gemini;component/Resources/Icons/Open.png");
    }
}

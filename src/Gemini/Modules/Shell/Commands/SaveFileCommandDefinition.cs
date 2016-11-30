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
    public class SaveFileCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.SaveFile";

        [Export] public static CommandKeyboardShortcut KeyGesture =
            new CommandKeyboardShortcut<SaveFileCommandDefinition>(new KeyGesture(Key.S, ModifierKeys.Control));

        public override string Name => CommandName;

        public override string Text => Resources.FileSaveCommandText;

        public override string ToolTip => Resources.FileSaveCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/Gemini;component/Resources/Icons/Save.png");
    }
}
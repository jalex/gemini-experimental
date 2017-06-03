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
    public class SaveAllFilesCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.SaveAllFiles";

        [Export] public static CommandKeyboardShortcut KeyGesture =
            new CommandKeyboardShortcut<SaveAllFilesCommandDefinition>(new KeyGesture(Key.S,
                ModifierKeys.Control | ModifierKeys.Shift));

        public override string Name => CommandName;

        public override string Text => Resources.FileSaveAllCommandText;

        public override string ToolTip => Resources.FileSaveAllCommandToolTip;

        public override Uri IconSource =>
            new Uri("pack://application:,,,/Gemini;component/Resources/Icons/SaveAll.png");
    }
}

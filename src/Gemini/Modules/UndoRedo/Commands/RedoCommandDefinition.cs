#region

using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.UndoRedo.Commands
{
    [CommandDefinition]
    public class RedoCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Edit.Redo";

        [Export] public static CommandKeyboardShortcut KeyGesture =
            new CommandKeyboardShortcut<RedoCommandDefinition>(new KeyGesture(Key.Y, ModifierKeys.Control));

        public override string Name => CommandName;

        public override string Text => Resources.EditRedoCommandText;

        public override string ToolTip => Resources.EditRedoCommandToolTip;

        public override Uri IconSource => new Uri("pack://application:,,,/Gemini;component/Resources/Icons/Redo.png");
    }
}

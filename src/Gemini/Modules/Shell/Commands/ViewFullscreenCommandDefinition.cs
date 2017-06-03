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
    public class ViewFullScreenCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.FullScreen";

        [Export] public static CommandKeyboardShortcut KeyGesture =
            new CommandKeyboardShortcut<ViewFullScreenCommandDefinition>(new KeyGesture(Key.Enter,
                ModifierKeys.Shift | ModifierKeys.Alt));

        public override string Name => CommandName;

        public override string Text => Resources.ViewFullScreenCommandText;

        public override string ToolTip => Resources.ViewFullScreenCommandToolTip;

        public override Uri IconSource
            => new Uri("pack://application:,,,/Gemini;component/Resources/Icons/FullScreen.png");
    }
}

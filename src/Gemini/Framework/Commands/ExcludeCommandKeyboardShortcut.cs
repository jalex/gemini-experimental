namespace Gemini.Framework.Commands
{
    public class ExcludeCommandKeyboardShortcut
    {
        public ExcludeCommandKeyboardShortcut(CommandKeyboardShortcut keyboardShortcut)
        {
            KeyboardShortcut = keyboardShortcut;
        }

        public CommandKeyboardShortcut KeyboardShortcut { get; }
    }
}
namespace Gemini.Framework.Commands
{
    public class ExcludeCommandKeyboardShortcut
    {
        private readonly CommandKeyboardShortcut _keyboardShortcut;

        public CommandKeyboardShortcut KeyboardShortcut => _keyboardShortcut;

        public ExcludeCommandKeyboardShortcut(CommandKeyboardShortcut keyboardShortcut)
        {
            _keyboardShortcut = keyboardShortcut;
        }
    }
}
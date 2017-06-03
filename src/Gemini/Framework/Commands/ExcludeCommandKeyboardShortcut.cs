namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a type for declaring that a previously declared <see cref="CommandKeyboardShortcut" /> should
    ///     be excluded.
    /// </summary>
    public class ExcludeCommandKeyboardShortcut
    {
        /// <summary>
        ///     Returns the <see cref="CommandKeyboardShortcut" /> to exclude.
        /// </summary>
        public CommandKeyboardShortcut KeyboardShortcut { get; }

        /// <summary>
        ///     Creates a new <see cref="ExcludeCommandKeyboardShortcut" />.
        /// </summary>
        /// <param name="keyboardShortcut">The <see cref="CommandKeyboardShortcut" /> to exclude.</param>
        public ExcludeCommandKeyboardShortcut(CommandKeyboardShortcut keyboardShortcut)
        {
            KeyboardShortcut = keyboardShortcut;
        }
    }
}

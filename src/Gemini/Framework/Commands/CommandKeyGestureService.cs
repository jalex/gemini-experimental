#region

using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents the default implementation of the <see cref="ICommandKeyGestureService" />.
    /// </summary>
    [Export(typeof(ICommandKeyGestureService))]
    public class CommandKeyGestureService : ICommandKeyGestureService
    {
        private readonly ICommandService _commandService;
        private readonly CommandKeyboardShortcut[] _keyboardShortcuts;

        /// <summary>
        ///     Creates a new <see cref="CommandKeyGestureService" />.
        /// </summary>
        /// <param name="keyboardShortcuts">The available <see cref="CommandKeyboardShortcut" />s.</param>
        /// <param name="excludeKeyboardShortcuts">
        ///     The <see cref="ExcludeCommandKeyboardShortcut" /> for excluding declared
        ///     <see cref="CommandKeyboardShortcut" />s.
        /// </param>
        /// <param name="commandService">The <see cref="ICommandService" />.</param>
        [ImportingConstructor]
        public CommandKeyGestureService(
            [ImportMany] CommandKeyboardShortcut[] keyboardShortcuts,
            [ImportMany] ExcludeCommandKeyboardShortcut[] excludeKeyboardShortcuts,
            ICommandService commandService)
        {
            _keyboardShortcuts = keyboardShortcuts
                .Except(excludeKeyboardShortcuts.Select(x => x.KeyboardShortcut))
                .OrderBy(x => x.SortOrder)
                .ToArray();
            _commandService = commandService;
        }

        /// <summary>
        ///     Binds the available key gestures to the specified <see cref="UIElement" />.
        /// </summary>
        /// <param name="uiElement">
        ///     The <see cref="UIElement" /> to bind the key gestures to.
        /// </param>
        public void BindKeyGestures(UIElement uiElement)
        {
            foreach (var keyboardShortcut in _keyboardShortcuts)
                if (keyboardShortcut.KeyGesture != null)
                    uiElement.InputBindings.Add(new InputBinding(
                        _commandService.GetTargetableCommand(
                            _commandService.GetCommand(keyboardShortcut.CommandDefinition)),
                        keyboardShortcut.KeyGesture));
        }

        /// <summary>
        ///     Returns the primary <see cref="KeyGesture" /> associated with the specified <see cref="CommandDefinitionBase" />.
        /// </summary>
        /// <param name="commandDefinition">The <see cref="CommandDefinitionBase" /> to lookup.</param>
        /// <returns>The primary <see cref="KeyGesture" /> if available, otherwise null.</returns>
        public KeyGesture GetPrimaryKeyGesture(CommandDefinitionBase commandDefinition)
        {
            var keyboardShortcut = _keyboardShortcuts.FirstOrDefault(x => x.CommandDefinition == commandDefinition);
            return keyboardShortcut?.KeyGesture;
        }
    }
}

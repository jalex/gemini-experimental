#region

using System;
using System.Windows.Input;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a base type for declaring a global keyboard shortcut associated with a command.
    /// </summary>
    public abstract class CommandKeyboardShortcut
    {
        private readonly Func<CommandDefinitionBase> _commandDefinition;

        /// <summary>
        ///     Returns the <see cref="CommandDefinitionBase" /> associated with the shortcut.
        /// </summary>
        public CommandDefinitionBase CommandDefinition => _commandDefinition();

        /// <summary>
        ///     Returns the <see cref="System.Windows.Input.KeyGesture" /> associated with the shortcut.
        /// </summary>
        public KeyGesture KeyGesture { get; }

        /// <summary>
        ///     Returns the priority of the shortcut.
        /// </summary>
        public int SortOrder { get; }

        /// <summary>
        ///     Creates a new <see cref="CommandKeyboardShortcut" />.
        /// </summary>
        /// <param name="keyGesture">The <see cref="System.Windows.Input.KeyGesture" /> associated with the shortcut.</param>
        /// <param name="sortOrder">The priority of the shortcut.</param>
        /// <param name="commandDefinition">A delegate for resolving a <see cref="CommandDefinitionBase" />.</param>
        protected CommandKeyboardShortcut(KeyGesture keyGesture, int sortOrder,
            Func<CommandDefinitionBase> commandDefinition)
        {
            _commandDefinition = commandDefinition;
            KeyGesture = keyGesture;
            SortOrder = sortOrder;
        }
    }

    /// <summary>
    ///     Represents a generic <see cref="CommandKeyboardShortcut" /> with strongly-typed command definition.
    /// </summary>
    /// <typeparam name="TCommandDefinition">
    ///     The type contract of the command definition, implementing
    ///     <see cref="CommandDefinition" />.
    /// </typeparam>
    public class CommandKeyboardShortcut<TCommandDefinition> : CommandKeyboardShortcut
        where TCommandDefinition : CommandDefinition
    {
        /// <summary>
        ///     Creates a new <see cref="CommandKeyboardShortcut{TCommandDefinition}" />.
        /// </summary>
        /// <param name="keyGesture">The <see cref="System.Windows.Input.KeyGesture" /> associated with the shortcut.</param>
        /// <param name="sortOrder">The priority of the shortcut.</param>
        public CommandKeyboardShortcut(KeyGesture keyGesture, int sortOrder = 5)
            : base(
                keyGesture, sortOrder, () => IoC.Get<ICommandService>().GetCommandDefinition(typeof(TCommandDefinition))
            )
        {
        }
    }
}

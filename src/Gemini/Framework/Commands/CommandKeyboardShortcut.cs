#region

using System;
using System.Windows.Input;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Commands
{
    public abstract class CommandKeyboardShortcut
    {
        private readonly Func<CommandDefinitionBase> _commandDefinition;

        protected CommandKeyboardShortcut(KeyGesture keyGesture, int sortOrder,
            Func<CommandDefinitionBase> commandDefinition)
        {
            _commandDefinition = commandDefinition;
            KeyGesture = keyGesture;
            SortOrder = sortOrder;
        }

        public CommandDefinitionBase CommandDefinition => _commandDefinition();

        public KeyGesture KeyGesture { get; }

        public int SortOrder { get; }
    }

    public class CommandKeyboardShortcut<TCommandDefinition> : CommandKeyboardShortcut
        where TCommandDefinition : CommandDefinition
    {
        public CommandKeyboardShortcut(KeyGesture keyGesture, int sortOrder = 5)
            : base(
                keyGesture, sortOrder, () => IoC.Get<ICommandService>().GetCommandDefinition(typeof(TCommandDefinition))
            )
        {
        }
    }
}
#region

using System;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Framework.ToolBars
{
    public class CommandToolBarItemDefinition<TCommandDefinition> : ToolBarItemDefinition
        where TCommandDefinition : CommandDefinitionBase
    {
        private readonly CommandDefinitionBase _commandDefinition;

        public CommandToolBarItemDefinition(ToolBarItemGroupDefinition group, int sortOrder,
            ToolBarItemDisplay display = ToolBarItemDisplay.IconOnly)
            : base(group, sortOrder, display)
        {
            _commandDefinition = IoC.Get<ICommandService>().GetCommandDefinition(typeof(TCommandDefinition));
            KeyGesture = IoC.Get<ICommandKeyGestureService>().GetPrimaryKeyGesture(_commandDefinition);
        }

        public override string Text => _commandDefinition.ToolTip;

        public override Uri IconSource => _commandDefinition.IconSource;

        public override KeyGesture KeyGesture { get; }

        public override CommandDefinitionBase CommandDefinition => _commandDefinition;
    }
}
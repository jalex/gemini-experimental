#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Modules.MainMenu.Models
{
    public class CommandMenuItem : StandardMenuItem, ICommandUiItem
    {
        private readonly Command _command;
        private readonly KeyGesture _keyGesture;
        private readonly List<StandardMenuItem> _listItems;
        private readonly StandardMenuItem _parent;

        public override string Text => _command.Text;

        //public override Uri IconSource => _command.IconSource;
        public override object IconSource => _command.IconSource;

        public override string InputGestureText => _keyGesture == null
            ? string.Empty
            : _keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);

        public override ICommand Command => IoC.Get<ICommandService>().GetTargetableCommand(_command);

        public override bool IsChecked => _command.Checked;

        public override bool IsVisible => _command.Visible;

        private bool IsListItem { get; set; }

        CommandDefinitionBase ICommandUiItem.CommandDefinition => _command.CommandDefinition;

        public CommandMenuItem(Command command, StandardMenuItem parent)
        {
            _command = command;
            _keyGesture = IoC.Get<ICommandKeyGestureService>().GetPrimaryKeyGesture(_command.CommandDefinition);
            _parent = parent;

            _listItems = new List<StandardMenuItem>();

            _command.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Visible" || e.PropertyName == "Checked")
                    NotifyOfPropertyChange("Is" + e.PropertyName);
                else if (e.PropertyName == "Text" || e.PropertyName == "IconSource")
                    NotifyOfPropertyChange(e.PropertyName);
            };
        }

        void ICommandUiItem.Update(CommandHandlerWrapper commandHandler)
        {
            if (_command != null && _command.CommandDefinition.IsList && !IsListItem)
            {
                foreach (var listItem in _listItems)
                    _parent.Children.Remove(listItem);

                _listItems.Clear();

                var listCommands = new List<Command>();
                commandHandler.Populate(_command, listCommands);

                _command.Visible = false;

                var startIndex = _parent.Children.IndexOf(this) + 1;

                foreach (var command in listCommands)
                {
                    var newMenuItem = new CommandMenuItem(command, _parent)
                    {
                        IsListItem = true
                    };
                    _parent.Children.Insert(startIndex++, newMenuItem);
                    _listItems.Add(newMenuItem);
                }
            }
        }
    }
}

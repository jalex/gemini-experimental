﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Commands;
using Gemini.Framework.ToolBars;

namespace Gemini.Modules.ToolBars.Models
{
	public class CommandToolBarItem : ToolBarItemBase, ICommandUiItem
    {
	    private readonly ToolBarItemDefinition _toolBarItem;
	    private readonly Command _command;
        private readonly KeyGesture _keyGesture;
        private readonly IToolBar _parent;

		public string Text => _command.Text;

        public ToolBarItemDisplay Display => _toolBarItem.Display;

        public Uri IconSource => _command.IconSource;

        public string ToolTip
	    {
	        get
	        {
                var inputGestureText = (_keyGesture != null)
                    ? $" ({_keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture)})"
                    : string.Empty;

                return $"{_command.ToolTip}{inputGestureText}".Trim();
	        }
	    }

	    public bool HasToolTip => !string.IsNullOrWhiteSpace(ToolTip);

        public ICommand Command => IoC.Get<ICommandService>().GetTargetableCommand(_command);

        public bool IsChecked => _command.Checked;

        public CommandToolBarItem(ToolBarItemDefinition toolBarItem, Command command, IToolBar parent)
		{
		    _toolBarItem = toolBarItem;
		    _command = command;
            _keyGesture = IoC.Get<ICommandKeyGestureService>().GetPrimaryKeyGesture(_command.CommandDefinition);
            _parent = parent;

            command.PropertyChanged += OnCommandPropertyChanged;
		}

        private void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => Text);
            NotifyOfPropertyChange(() => IconSource);
            NotifyOfPropertyChange(() => ToolTip);
            NotifyOfPropertyChange(() => HasToolTip);
            NotifyOfPropertyChange(() => IsChecked);
        }

	    CommandDefinitionBase ICommandUiItem.CommandDefinition => _command.CommandDefinition;

        void ICommandUiItem.Update(CommandHandlerWrapper commandHandler)
	    {
	        // TODO
	    }
    }
}
#region

using System;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Commands
{
    public class Command : PropertyChangedBase
    {
        private bool _checked;
        private bool _enabled = true;
        private Uri _iconSource;
        private string _text;
        private string _toolTip;
        private bool _visible = true;

        public CommandDefinitionBase CommandDefinition { get; }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                NotifyOfPropertyChange(() => Visible);
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                NotifyOfPropertyChange(() => Enabled);
            }
        }

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                NotifyOfPropertyChange(() => Checked);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                NotifyOfPropertyChange(() => ToolTip);
            }
        }

        public Uri IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                NotifyOfPropertyChange(() => IconSource);
            }
        }

        public object Tag { get; set; }

        public Command(CommandDefinitionBase commandDefinition)
        {
            CommandDefinition = commandDefinition;
            Text = commandDefinition.Text;
            ToolTip = commandDefinition.ToolTip;
            IconSource = commandDefinition.IconSource;
        }
    }
}
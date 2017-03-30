#region

using System;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Commands
{

    /// <summary>
    ///     Represents the state of an executable operation which can be persisted across UI elements.
    /// </summary>
    public class Command : PropertyChangedBase
    {
        private bool _checked;
        private bool _enabled = true;
        private Uri _iconSource;
        private string _text;
        private string _toolTip;
        private bool _visible = true;

        /// <summary>
        ///     Returns the <see cref="CommandDefinitionBase"/> associated with the command.
        /// </summary>
        public CommandDefinitionBase CommandDefinition { get; }

        /// <summary>
        ///     Gets or sets whether the command and associated UI elements are visible on the UI.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                NotifyOfPropertyChange(() => Visible);
            }
        }

        /// <summary>
        ///     Gets or sets whether the command and associated UI elements are enabled.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                NotifyOfPropertyChange(() => Enabled);
            }
        }

        /// <summary>
        ///     Gets or sets whether the command and associated UI elements are in a checked state.
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                NotifyOfPropertyChange(() => Checked);
            }
        }

        /// <summary>
        ///     Gets or sets the display text of the command and associated UI elements.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        /// <summary>
        ///     Gets or sets a tooltip associated with the command and associated UI elements.
        /// </summary>
        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                NotifyOfPropertyChange(() => ToolTip);
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="Uri"/> of an icon of the command and associated UI elements.
        /// </summary>
        public Uri IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                NotifyOfPropertyChange(() => IconSource);
            }
        }

        /// <summary>
        ///     Gets or sets a custom tag which can be used to associated payload with the command.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        ///     Creates a new <see cref="Command"/>.
        /// </summary>
        /// <param name="commandDefinition">The <see cref="CommandDefinitionBase"/> associated with the command.</param>
        public Command(CommandDefinitionBase commandDefinition)
        {
            CommandDefinition = commandDefinition;
            Text = commandDefinition.Text;
            ToolTip = commandDefinition.ToolTip;
            IconSource = commandDefinition.IconSource;
        }
    }
}

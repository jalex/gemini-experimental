#region

using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework
{

    /// <summary>
    ///     Represents a base implementation of a <see cref="ILayoutPanel"/>.
    /// </summary>
    public abstract class LayoutItemBase : Screen, ILayoutPanel
    {
        private bool _isSelected;

        /// <summary>
        ///     Returns an <see cref="ICommand"/> which is being triggered for closing the panel.
        /// </summary>
        public abstract ICommand CloseCommand { get; }

        /// <summary>
        ///     Returns the unique identifier of the panel.
        /// </summary>
        /// <remarks>Defaults to <see cref="Guid.NewGuid"/>.</remarks>
        [Browsable(false)]
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        ///     Returns a tooltip associated with the panel.
        /// </summary>
        /// <remarks>Defaults to <see cref="IHaveDisplayName.DisplayName"/> if not specified.</remarks>
        [Browsable(false)]
        public virtual string ToolTip => DisplayName;

        /// <summary>
        ///     Returns the content id of the panel.
        /// </summary>
        /// <remarks>Defaults to <see cref="Id"/>.</remarks>
        [Browsable(false)]
        public string ContentId => Id.ToString();

        /// <summary>
        ///     Returns the <see cref="Uri"/> of an icon associated with the panel.
        /// </summary>
        [Browsable(false)]
        public virtual Uri IconSource => null;

        /// <summary>
        ///     Gets or sets whether the panel is currently selected.
        /// </summary>
        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        /// <summary>
        ///     Returns whether the panel should be re-opened when the application starts.
        /// </summary>
        [Browsable(false)]
        public virtual bool ShouldReopenOnStart => false;

        /// <summary>
        ///     Asynchronously loads the state of the panel using the specified <see cref="BinaryReader"/>.
        /// </summary>
        /// <param name="reader">A <see cref="BinaryReader"/> for reading the state.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public virtual Task LoadState(BinaryReader reader) => Task.CompletedTask;

        /// <summary>
        ///     Asynchronously saves the state of the panel using the specified <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="writer">A <see cref="BinaryWriter"/> for persisting the state.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public virtual Task SaveState(BinaryWriter writer) => Task.CompletedTask;
    }
}

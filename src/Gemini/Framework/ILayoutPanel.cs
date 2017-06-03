#region

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework.Services;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Represents a common contract for layout panels which can be loaded into the
    ///     workspace of the <see cref="IShell" />.
    /// </summary>
    public interface ILayoutPanel : IScreen
    {
        /// <summary>
        ///     Returns the unique identifier of the panel.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        ///     Returns a tooltip associated with the panel.
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        ///     Returns the content id of the panel.
        /// </summary>
        string ContentId { get; }

        /// <summary>
        ///     Returns an <see cref="ICommand" /> which is being triggered for closing the panel.
        /// </summary>
        ICommand CloseCommand { get; }

        /// <summary>
        ///     Returns the <see cref="Uri" /> of an icon associated with the panel.
        /// </summary>
        Uri IconSource { get; }

        /// <summary>
        ///     Gets or sets whether the panel is currently selected.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        ///     Returns whether the panel should be re-opened when the application starts.
        /// </summary>
        bool ShouldReopenOnStart { get; }

        /// <summary>
        ///     Asynchronously loads the state of the panel using the specified <see cref="BinaryReader" />.
        /// </summary>
        /// <param name="reader">A <see cref="BinaryReader" /> for reading the state.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task LoadState(BinaryReader reader);

        /// <summary>
        ///     Asynchronously saves the state of the panel using the specified <see cref="BinaryWriter" />.
        /// </summary>
        /// <param name="writer">A <see cref="BinaryWriter" /> for persisting the state.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task SaveState(BinaryWriter writer);
    }
}

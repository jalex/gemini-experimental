#region

using Gemini.Framework.Services;
using Gemini.Modules.UndoRedo;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Represents a document. A document is a panel which is being used to represent data
    ///     or application contents in the main area of the <see cref="IShell" />.
    /// </summary>
    public interface IDocument : ILayoutPanel
    {
        /// <summary>
        ///     Returns an <see cref="IUndoRedoManager" /> associated with the document.
        /// </summary>
        IUndoRedoManager UndoRedoManager { get; }
    }
}

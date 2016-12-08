#region

using Gemini.Modules.UndoRedo;

#endregion

namespace Gemini.Framework
{
    public interface IDocument : ILayoutPanel
    {
        IUndoRedoManager UndoRedoManager { get; }
    }
}

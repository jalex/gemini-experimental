#region

using Gemini.Modules.UndoRedo;

#endregion

namespace Gemini.Framework
{
    public interface IDocument : ILayoutItem
    {
        IUndoRedoManager UndoRedoManager { get; }
    }
}
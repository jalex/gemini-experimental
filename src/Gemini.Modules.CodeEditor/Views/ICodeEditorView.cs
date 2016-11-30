#region

using ICSharpCode.AvalonEdit;

#endregion

namespace Gemini.Modules.CodeEditor.Views
{
    public interface ICodeEditorView
    {
        TextEditor TextEditor { get; }
    }
}
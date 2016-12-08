#region

using System.Windows.Input;
using ICSharpCode.AvalonEdit;

#endregion

namespace Gemini.Modules.CodeEditor.Views
{
    public partial class CodeEditorView : ICodeEditorView
    {
        public TextEditor TextEditor => CodeEditor;

        public CodeEditorView()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
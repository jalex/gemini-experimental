#region

using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;

#endregion

namespace Gemini.Modules.CodeEditor.Views
{
    public partial class CodeEditorView : UserControl, ICodeEditorView
    {
        public CodeEditorView()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        public TextEditor TextEditor => CodeEditor;
    }
}
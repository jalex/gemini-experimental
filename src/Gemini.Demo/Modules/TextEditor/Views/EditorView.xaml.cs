#region

using System.Windows.Input;

#endregion

namespace Gemini.Demo.Modules.TextEditor.Views
{
    public partial class EditorView
    {
        public EditorView()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}

namespace Gemini.Demo.Modules.Home.Views
{
    /// <summary>
    ///     Interaction logic for HelixView.xaml
    /// </summary>
    public partial class HelixView : IHelixView
    {
        public ICSharpCode.AvalonEdit.TextEditor TextEditor => CodeEditor;

        public HelixView()
        {
            InitializeComponent();
        }
    }
}

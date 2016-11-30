#region

using System.Windows.Controls;

#endregion

namespace Gemini.Demo.Modules.Home.Views
{
    /// <summary>
    ///     Interaction logic for HelixView.xaml
    /// </summary>
    public partial class HelixView : UserControl, IHelixView
    {
        public HelixView()
        {
            InitializeComponent();
        }

        public ICSharpCode.AvalonEdit.TextEditor TextEditor => CodeEditor;
    }
}
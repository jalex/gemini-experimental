namespace Gemini.Modules.Output.Views
{
    /// <summary>
    ///     Interaction logic for OutputView.xaml
    /// </summary>
    public partial class OutputView : IOutputView
    {
        public OutputView()
        {
            InitializeComponent();
        }

        public void ScrollToEnd()
        {
            OutputText.ScrollToEnd();
        }

        public void Clear()
        {
            OutputText.Clear();
        }

        public void AppendText(string text)
        {
            OutputText.AppendText(text);
            ScrollToEnd();
        }

        public void SetText(string text)
        {
            OutputText.Text = text;
            ScrollToEnd();
        }
    }
}
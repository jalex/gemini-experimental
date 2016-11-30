#region

using System.Windows.Controls;

#endregion

namespace Gemini.Modules.ToolBars.Views
{
    /// <summary>
    ///     Interaction logic for ToolBarsView.xaml
    /// </summary>
    public partial class ToolBarsView : UserControl, IToolBarsView
    {
        public ToolBarsView()
        {
            InitializeComponent();
        }

        ToolBarTray IToolBarsView.ToolBarTray => ToolBarTray;
    }
}
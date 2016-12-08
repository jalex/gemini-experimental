#region

using System.Windows.Controls;

#endregion

namespace Gemini.Modules.ToolBars.Views
{
    /// <summary>
    ///     Interaction logic for ToolBarsView.xaml
    /// </summary>
    public partial class ToolBarsView : IToolBarsView
    {
        ToolBarTray IToolBarsView.ToolBarTray => ToolBarTray;

        public ToolBarsView()
        {
            InitializeComponent();
        }
    }
}
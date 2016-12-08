#region

using System.Windows.Controls;
using System.Windows.Input;

#endregion

namespace Gemini.Modules.ErrorList.Views
{
    /// <summary>
    ///     Interaction logic for ErrorListView.xaml
    /// </summary>
    public partial class ErrorListView
    {
        public ErrorListView()
        {
            InitializeComponent();
        }

        private void OnDataGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = (DataGrid) sender;
            if ((dataGrid.SelectedItems == null) || (dataGrid.SelectedItems.Count != 1))
                return;

            var errorListItem = (ErrorListItem) dataGrid.SelectedItem;
            errorListItem.OnClick?.Invoke();
        }
    }
}
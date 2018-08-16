using EventLogQueryTool.Model;
using EventLogQueryTool.ViewModel;
using System.Linq;
using System.Windows;
using Xceed.Wpf.DataGrid;

namespace EventLogQueryTool.Views
{
    /// <summary>
    /// Logique d'interaction pour EventLogWindow.xaml
    /// </summary>
    public partial class EventLogWindow : Window
    {

        #region Public Constructors

        public EventLogWindow()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var context = (EventLogViewModel)DataContext;
            context.SelectedCategories = categoryList.SelectedItems.Cast<ServerCategory>().ToList();
        }

        #endregion Private Methods

        private void DataGridControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var g = (DataGridControl)sender;
            if (g.SelectedItem!=null)
            {
                var w = new EventDetailWindow();
                w.DataContext = g.SelectedItem;
                w.Show();
            }
            
        }
    }
}
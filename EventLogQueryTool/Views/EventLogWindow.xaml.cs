using EventLogQueryTool.Model;
using EventLogQueryTool.ViewModel;
using System.Linq;
using System.Windows;

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
    }
}
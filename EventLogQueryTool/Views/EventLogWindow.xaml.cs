using EventLogQueryTool.ViewModel;
using System.Windows;
using System.Linq;
using EventLogQueryTool.Model;

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

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var context = (EventLogViewModel)DataContext;
            context.SelectedCategories = categoryList.SelectedItems.Cast<ServerCategory>().ToList();
        }
    }
}
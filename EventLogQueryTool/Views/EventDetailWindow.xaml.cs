using System.Windows;

namespace EventLogQueryTool.Views
{
    /// <summary>
    /// Logique d'interaction pour ServerConfigurationEditor.xaml
    /// </summary>
    public partial class EventDetailWindow : Window
    {
        public EventDetailWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
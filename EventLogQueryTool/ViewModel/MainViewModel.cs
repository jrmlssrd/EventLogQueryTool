using EventLogQueryTool.Views;
using EventLogQueryToolCore.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace EventLogQueryTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IEventLogReaderManager _eventLogReaderManager;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IEventLogReaderManager eventLogReaderManager)
        {
            _eventLogReaderManager = eventLogReaderManager;
            OpenEventLogCommand = new RelayCommand(ExecuteOpenEventLogWindow);
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand OpenEventLogCommand { get; set; }

        #endregion Public Properties

        #region Private Methods

        private void ExecuteOpenEventLogWindow()
        {
            EventLogWindow f = new EventLogWindow();
            f.Show();
        }

        #endregion Private Methods
    }
}
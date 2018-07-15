using EventLogQueryTool.Services;
using GalaSoft.MvvmLight;

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
        }

        #endregion Public Constructors
    }
}
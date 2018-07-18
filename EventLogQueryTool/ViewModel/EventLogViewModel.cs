using EventLogQueryTool.Model;
using EventLogQueryTool.Services;
using EventLogQueryToolCore.Model;
using EventLogQueryToolCore.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EventLogQueryTool.ViewModel
{
    public class EventLogViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IEventLogReaderManager _eventLogReaderManager;
        private readonly IServerConfigurationManager _serverConfigurationManager;

        private DateTime? _dateFrom;

        private DateTime? _dateTo;

        private ICommand _searchCommand;

        //private ObservableCollection<string> _entryTypeList = new ObservableCollection<string>();
        private ObservableCollection<EventLogEntryLevel> _selectedEntryTypeList = new ObservableCollection<EventLogEntryLevel>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public EventLogViewModel(IEventLogReaderManager eventLogReaderManager, IServerConfigurationManager serverConfigurationManager)
        {
            InitViewModelData();

            _searchCommand = new RelayCommand(ExecuteSearch);

            _eventLogReaderManager = eventLogReaderManager;
            _serverConfigurationManager = serverConfigurationManager;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime? DateFrom
        {
            get { return _dateFrom; }

            set
            {
                _dateFrom = value;
                RaisePropertyChanged("DateFrom");
            }
        }

        public DateTime? DateTo
        {
            get { return _dateTo; }

            set
            {
                _dateTo = value;
                RaisePropertyChanged("DateTo");
            }
        }

        public ICommand OpenEventLogCommand { get; set; }

        public ICommand SearchCommand
        {
            get { return _searchCommand; }
            set { _searchCommand = value; }
        }

        public ObservableCollection<EventLogEntryLevel> SelectedEntryTypeList
        {
            get { return _selectedEntryTypeList; }

            set
            {
                _selectedEntryTypeList = value;
                RaisePropertyChanged("SelectedEntryTypeList");
            }
        }

        public ServerConfiguration ServerConfiguration { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void ExecuteSearch()
        {
            var crit = new EventLogQueryCriteria()
            {
                DateFrom = DateFrom,
                DateTo = DateTo,
                EventLogEntryTypeList = SelectedEntryTypeList
            };
            var logs = _eventLogReaderManager.ReadLogs("localhost", crit);
        }

        #endregion Public Methods

        #region Private Methods

        private void InitViewModelData()
        {
            // Inititiaze default selected entry level
            _selectedEntryTypeList = new ObservableCollection<EventLogEntryLevel>();
            _selectedEntryTypeList.Add(EventLogEntryLevel.Error);
            _selectedEntryTypeList.Add(EventLogEntryLevel.Warning);
            _selectedEntryTypeList.Add(EventLogEntryLevel.Information);

            // Initialize current server configuration
            ServerConfiguration = _serverConfigurationManager.LoadConfiguration();

            // If there is no current server configuration then set the default. (localhost only)
            if (ServerConfiguration == null)
            {
                _serverConfigurationManager.InitializeConfiguration();
                ServerConfiguration = _serverConfigurationManager.LoadConfiguration();
            }
        }

        #endregion Private Methods
    }
}
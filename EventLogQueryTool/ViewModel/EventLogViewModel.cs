using EventLogQueryTool.Model;
using EventLogQueryTool.Services;
using EventLogQueryTool.Views;
using EventLogQueryToolCore.Model;
using EventLogQueryToolCore.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

        private ICommand _editConfigCommand;
        private string _providerName;
        private ICommand _searchCommand;

        private ObservableCollection<EventLogEntryLevel> _selectedEntryTypeList = new ObservableCollection<EventLogEntryLevel>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public EventLogViewModel(IEventLogReaderManager eventLogReaderManager, IServerConfigurationManager serverConfigurationManager)
        {
            _eventLogReaderManager = eventLogReaderManager;
            _serverConfigurationManager = serverConfigurationManager;

            InitViewModelData();

            _searchCommand = new RelayCommand(ExecuteSearch);
            _editConfigCommand = new RelayCommand(ExecuteEditConfig);
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime? DateFrom
        {
            get
            {
                return _dateFrom;
            }

            set
            {
                _dateFrom = value;
                RaisePropertyChanged("DateFrom");
            }
        }

        public DateTime? DateTo
        {
            get
            {
                return _dateTo;
            }

            set
            {
                _dateTo = value;
                RaisePropertyChanged("DateTo");
            }
        }

        public ICommand EditConfigCommand
        {
            get { return _editConfigCommand; }
            set { _editConfigCommand = value; }
        }

        public ObservableCollection<Event> EventResultList { get; set; } = new ObservableCollection<Event>();

        public ICommand OpenEventLogCommand { get; set; }

        public string ProviderName
        {
            get
            {
                return _providerName;
            }
            set
            {
                _providerName = value;
                RaisePropertyChanged("ProviderName");
            }
        }

        public ICommand SearchCommand
        {
            get { return _searchCommand; }
            set { _searchCommand = value; }
        }

        public List<ServerCategory> SelectedCategories { get; set; }

        public ObservableCollection<EventLogEntryLevel> SelectedEntryTypeList
        {
            get
            {
                return _selectedEntryTypeList;
            }

            set
            {
                _selectedEntryTypeList = value;
                RaisePropertyChanged("SelectedEntryTypeList");
            }
        }

        public ServerConfiguration ServerConfiguration { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void ExecuteEditConfig()
        {
            ServerConfigurationEditor editor = new Views.ServerConfigurationEditor();
            editor.ShowDialog();
            InitViewModelData();
        }

        public void ExecuteSearch()
        {
            var crit = new EventLogQueryCriteria()
            {
                ProviderName = ProviderName,
                DateFrom = DateFrom,
                DateTo = DateTo,
                EventLogEntryTypeList = SelectedEntryTypeList
            };

            if (SelectedCategories != null && SelectedCategories.Any())
            {
                var logs = _eventLogReaderManager.ReadLogs(SelectedCategories.SelectMany(x => x.ServerList).Select(x => x.Name).ToArray(), crit);

                if (logs != null && logs.Any())
                {
                    EventResultList = new ObservableCollection<Event>(logs.Select(x => new Event(x)).ToList());
                }
                else
                {
                    EventResultList = new ObservableCollection<Event>();
                    MessageBox.Show("No events found with the current criteria");
                }
                RaisePropertyChanged("EventResultList");
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void InitViewModelData()
        {
            // Inititiaze default selected entry level
            SelectedEntryTypeList = new ObservableCollection<EventLogEntryLevel>();
            SelectedEntryTypeList.Add(EventLogEntryLevel.Error);
            SelectedEntryTypeList.Add(EventLogEntryLevel.Warning);
            SelectedEntryTypeList.Add(EventLogEntryLevel.Information);
            RaisePropertyChanged("SelectedEntryTypeList");

            // Initialize current server configuration
            ServerConfiguration = _serverConfigurationManager.LoadConfiguration();

            // If there is no current server configuration then set the default. (localhost only)
            if (ServerConfiguration == null)
            {
                _serverConfigurationManager.InitializeConfiguration();
                ServerConfiguration = _serverConfigurationManager.LoadConfiguration();
            }

            DateFrom = DateTime.Now.AddHours(-24);
        }

        #endregion Private Methods

    }
}
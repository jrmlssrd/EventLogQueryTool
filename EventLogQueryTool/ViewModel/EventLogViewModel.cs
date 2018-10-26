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
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Input;

namespace EventLogQueryTool.ViewModel
{
    public class EventLogViewModel : ViewModelBase
    {

        #region Private Fields

        private readonly IEventLogReaderManager _eventLogReaderManager;
        private readonly IServerConfigurationManager _serverConfigurationManager;

        private string _contains = string.Empty;
        private DateTime? _dateFrom;

        private DateTime? _dateTo;

        private ICommand _editConfigCommand;
        private string _manualServers = string.Empty;
        private string _providerName;
        private ICommand _searchCommand;
        private ObservableCollection<EventLogEntryLevel> _selectedEntryTypeList = new ObservableCollection<EventLogEntryLevel>();

        #endregion Private Fields

        #region Public Constructeurs

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

        #endregion Public Constructeurs

        #region Public Properties

        public string Contains
        {
            get
            {
                return _contains;
            }
            set
            {
                _contains = value;
                RaisePropertyChanged("Contains");
            }
        }

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

        public string ManualServers
        {
            get
            {
                return _manualServers;
            }
            set
            {
                _manualServers = value;
                RaisePropertyChanged("ManualServers");
            }
        }

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

            ServerConfiguration = _serverConfigurationManager.LoadConfiguration();
            if (ServerConfiguration == null)
            {
                _serverConfigurationManager.InitializeConfiguration();
                ServerConfiguration = _serverConfigurationManager.LoadConfiguration();
            }
            RaisePropertyChanged("ServerConfiguration");
        }

        public void ExecuteSearch()
        {
            var crit = new EventLogQueryCriteria()
            {
                ProvidersName = string.IsNullOrWhiteSpace(ProviderName) ? new List<string>() : ProviderName.Split(';').ToList(),
                DateFrom = DateFrom,
                DateTo = DateTo,
                EventLogEntryTypeList = SelectedEntryTypeList,
                DescriptionContains = string.IsNullOrWhiteSpace(Contains) ? new List<string>() : Contains.Split(';').ToList()
            };

            List<string> serversList = new List<string>();
            if (SelectedCategories != null && SelectedCategories.Any())
            {
                serversList.AddRange(SelectedCategories.SelectMany(x => x.ServerList).Select(x => x.Name));
            }
            if (!string.IsNullOrEmpty(ManualServers))
            {
                serversList.AddRange(ManageManualServerList(ManualServers));
            }

            if (serversList.Any())
            {
                var logs = _eventLogReaderManager.ReadLogs(serversList, crit);

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
            else
            {
                MessageBox.Show("No servers has been selected!");
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

        /// <summary>
        /// Manager manual server list to check if server are actually reachable.
        /// </summary>
        /// <param name="manualServers">Server list as a string with semi-colon to split them</param>
        /// <returns>List of reachable servers</returns>
        private List<string> ManageManualServerList(string manualServers)
        {
            var initialManualServerList = manualServers.Split(';').ToList();
            var manualServerList = manualServers.Split(';').ToList();
            string msg = string.Empty;
            foreach (var server in initialManualServerList)
            {
                if (!ReachServer(server))
                {
                    msg += string.Format("The server {0} cannot be reach!", server.ToUpper()) + Environment.NewLine;
                    manualServerList.Remove(server);
                }
            }
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
            }
            return manualServerList;
        }

        private bool ReachServer(string server)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(server);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        #endregion Private Methods

    }
}
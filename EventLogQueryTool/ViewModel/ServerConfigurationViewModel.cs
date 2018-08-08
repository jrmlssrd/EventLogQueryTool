using EventLogQueryTool.Model;
using EventLogQueryTool.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace EventLogQueryTool.ViewModel
{
    public class ServerConfigurationViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IServerConfigurationManager _serverConfigurationManager;
        private readonly IServerConfigurationConverter _serverConfigurationConverter;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ServerConfigurationViewModel(IServerConfigurationManager serverConfigurationManager, IServerConfigurationConverter serverConfigurationConverter)
        {
            _serverConfigurationConverter = serverConfigurationConverter;
            _serverConfigurationManager = serverConfigurationManager;

            InitViewModelData();

            SaveConfigCommand = new RelayCommand(ExecuteSaveConfig);
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand SaveConfigCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ImportCommand { get; set; }

        public ServerConfiguration ServerConfiguration { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void ExecuteSaveConfig()
        {
            _serverConfigurationManager.WriteConfiguration(ServerConfiguration);
        }

        public void ExecuteExport()
        {
            var configString = _serverConfigurationConverter.FromObject(ServerConfiguration);
        }

        public void ExecuteImport()
        {
        }

        #endregion Public Methods

        #region Private Methods

        private void InitViewModelData()
        {
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
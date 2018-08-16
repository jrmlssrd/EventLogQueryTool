using EventLogQueryTool.Model;
using EventLogQueryTool.Services;
using EventLogQueryToolCore.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

namespace EventLogQueryTool.ViewModel
{
    public class ServerConfigurationViewModel : ViewModelBase
    {

        #region Private Fields

        private readonly IExceptionManager _exceptionManager;
        private readonly IServerConfigurationConverter _serverConfigurationConverter;
        private readonly IServerConfigurationManager _serverConfigurationManager;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ServerConfigurationViewModel(IServerConfigurationManager serverConfigurationManager, IServerConfigurationConverter serverConfigurationConverter, IExceptionManager exceptionManager)
        {
            _serverConfigurationConverter = serverConfigurationConverter;
            _serverConfigurationManager = serverConfigurationManager;
            _exceptionManager = exceptionManager;
            InitViewModelData();

            SaveConfigCommand = new RelayCommand(ExecuteSaveConfig);
            ExportCommand = new RelayCommand(ExecuteExport);
            ImportCommand = new RelayCommand(ExecuteImport);
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand ExportCommand { get; set; }

        public ICommand ImportCommand { get; set; }

        public ICommand SaveConfigCommand { get; set; }
        public ServerConfiguration ServerConfiguration { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void ExecuteExport()
        {
            var configString = _serverConfigurationConverter.FromObject(ServerConfiguration);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xml";
            sfd.FileName = "EventLogConfig.xml";
            sfd.OverwritePrompt = true;
            var r = sfd.ShowDialog();
            if (r.HasValue && r == true)
            {
                try
                {
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }
                    File.WriteAllText(sfd.FileName, configString);
                }
                catch (System.Exception ex)
                {
                    _exceptionManager.Raise(ex);
                }
            }
        }

        public void ExecuteImport()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".xml";
            var r = ofd.ShowDialog();
            if (r.HasValue && r == true)
            {
                try
                {
                    var configString = File.ReadAllText(ofd.FileName);
                    ServerConfiguration = _serverConfigurationConverter.ToObject(configString);
                    RaisePropertyChanged("ServerConfiguration");
                }
                catch (System.Exception ex)
                {
                    _exceptionManager.Raise(ex);
                }
            }
        }

        public void ExecuteSaveConfig()
        {
            _serverConfigurationManager.WriteConfiguration(ServerConfiguration);
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
using EventLogQueryTool.Model;
using System.Collections.Generic;

namespace EventLogQueryTool.Services
{
    public class ServerConfigurationManager : IServerConfigurationManager
    {
        #region Private Fields

        private const string APP_SETTING_KEY = "ServerConfiguration";
        private IServerConfigurationConverter _serverConfigurationConverter;

        #endregion Private Fields

        #region Public Constructors

        public ServerConfigurationManager(IServerConfigurationConverter serverConfigurationConverter)
        {
            _serverConfigurationConverter = serverConfigurationConverter;
        }

        #endregion Public Constructors

        #region Public Methods

        public void DeleteConfiguration()
        {
            Properties.Settings.Default[APP_SETTING_KEY] = string.Empty;
            Properties.Settings.Default.Save();
        }

        public void InitializeConfiguration()
        {
            var defaultConfig = new ServerConfiguration()
            {
                CategoryList = new List<ServerCategory>()
                {
                    new ServerCategory()
                    {
                        ServerList = new List<Server>()
                        {
                            new Server()
                            {
                                Name = "localhost"
                            }
                        }
                    }
                }
            };
            WriteConfiguration(defaultConfig);
        }

        public ServerConfiguration LoadConfiguration()
        {
            ServerConfiguration config = null;
            var configXML = Properties.Settings.Default[APP_SETTING_KEY].ToString();

            if (!string.IsNullOrEmpty(configXML))
            {
                config = _serverConfigurationConverter.ToObject(configXML);
            }

            return config;
        }

        public void WriteConfiguration(ServerConfiguration serverConfiguration)
        {
            Properties.Settings.Default[APP_SETTING_KEY] = _serverConfigurationConverter.FromObject(serverConfiguration);
            Properties.Settings.Default.Save();
        }

        #endregion Public Methods
    }
}
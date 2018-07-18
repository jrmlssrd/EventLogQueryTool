using EventLogQueryTool.Model;

namespace EventLogQueryTool.Services
{
    public interface IServerConfigurationManager
    {
        #region Public Methods

        void DeleteConfiguration();

        void InitializeConfiguration();

        ServerConfiguration LoadConfiguration();

        void WriteConfiguration(ServerConfiguration serverConfiguration);

        #endregion Public Methods
    }
}
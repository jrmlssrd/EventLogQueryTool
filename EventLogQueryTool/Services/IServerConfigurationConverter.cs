using EventLogQueryTool.Model;

namespace EventLogQueryTool.Services
{
    public interface IServerConfigurationConverter
    {
        #region Public Methods

        string FromObject(ServerConfiguration serverConfiguration);

        ServerConfiguration ToObject(string serverConfiguration);

        #endregion Public Methods
    }
}
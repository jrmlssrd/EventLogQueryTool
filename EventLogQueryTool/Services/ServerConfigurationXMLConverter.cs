using EventLogQueryTool.Model;
using System.IO;
using System.Xml.Serialization;

namespace EventLogQueryTool.Services
{
    public class ServerConfigurationXMLConverter : IServerConfigurationConverter
    {

        #region Public Methods

        public string FromObject(ServerConfiguration serverConfiguration)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ServerConfiguration));
            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, serverConfiguration);
            return writer.ToString();
        }

        public ServerConfiguration ToObject(string serverConfiguration)
        {
            ServerConfiguration config;
            var xmlSerializer = new XmlSerializer(typeof(ServerConfiguration));
            var stringReader = new StringReader(serverConfiguration);
            config = (ServerConfiguration)xmlSerializer.Deserialize(stringReader);
            return config;
        }

        #endregion Public Methods

    }
}
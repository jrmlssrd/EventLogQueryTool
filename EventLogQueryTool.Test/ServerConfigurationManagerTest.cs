using EventLogQueryTool.Model;
using EventLogQueryTool.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace EventLogQueryTool.Test
{
    [TestClass]
    public class ServerConfigurationManagerTest
    {
        #region Private Fields

        private ServerConfigurationManager _manager;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void DeleteConfiguration_SetDefaultConfigThenDelete_ConfigIsNullWhenLoaded()
        {
            // Load initial test config (should be null)
            var config = _manager.LoadConfiguration();
            Assert.IsNull(config);

            // Set the default configuration (localhost only)
            _manager.InitializeConfiguration();

            // Load the configuration (should be the default config with only localhost)
            var loadedConfig = _manager.LoadConfiguration();
            Assert.IsNotNull(loadedConfig);
            Assert.IsTrue(loadedConfig.CategoryList.First().ServerList.First().Name.Equals("localhost"));

            // Delete the configuration
            _manager.DeleteConfiguration();

            // Load the configuration (should be null)
            var loadedConfig2 = _manager.LoadConfiguration();
            Assert.IsNull(loadedConfig2);
        }

        [TestInitialize]
        public void Init()
        {
            _manager = new ServerConfigurationManager(new ServerConfigurationXMLConverter());
            _manager.DeleteConfiguration();
        }

        [TestMethod]
        public void InitializeConfiguration_SetDefaultConfig_LoadedConfigIsDefault()
        {
            // Load initial test config (should be null)
            var config = _manager.LoadConfiguration();
            Assert.IsNull(config);

            // Set the default configuration (localhost only)
            _manager.InitializeConfiguration();

            // Load the configuration (should be the default config with only localhost)
            var loadedConfig = _manager.LoadConfiguration();
            Assert.IsNotNull(loadedConfig);
            Assert.IsTrue(loadedConfig.CategoryList.First().ServerList.First().Name.Equals("localhost"));
        }

        [TestMethod]
        public void LoadConfiguration_NoSavedConfiguration_ConfigIsNull()
        {
            // Load the inital test configuration, should be null
            var config = _manager.LoadConfiguration();
            Assert.IsNull(config);
        }

        [TestMethod]
        public void WriteConfiguration_CustomConfiguration_ReloadIsSuccess()
        {
            // Define the configuration we want to write.
            var config = new ServerConfiguration()
            {
                CategoryList = new List<ServerCategory>()
                {
                    new ServerCategory()
                    {
                        ServerList = new List<Server>()
                        {
                            new Server()
                            {
                                Name = "testserver1"
                            }
                        }
                    }
                }
            };
            _manager.WriteConfiguration(config);

            // Load the last writen configuration
            var loadedConfig = _manager.LoadConfiguration();
            Assert.IsNotNull(loadedConfig);
            Assert.IsTrue(loadedConfig.CategoryList.First().ServerList.First().Name.Equals("testserver1"));
        }

        #endregion Public Methods
    }
}
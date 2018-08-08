using EventLogQueryToolCore.Common;
using EventLogQueryToolCore.Model;
using EventLogQueryToolCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventLogQueryToolCore.Test
{
    [TestClass]
    public class EventLogReaderManagerTest
    {
        #region Private Fields

        private EventLogReaderManager eventLogReaderManager;
        private EventLogReaderService eventLogReaderService;
        private IExceptionManager exceptionManager;
        private Exception thrownException;

        #endregion Private Fields

        #region Public Methods

        [TestInitialize]
        public void TestInit()
        {
            var exceptionManagerMock = new Mock<IExceptionManager>();
            exceptionManagerMock.Setup(x => x.Raise(thrownException));
            exceptionManager = exceptionManagerMock.Object;
            eventLogReaderService = new EventLogReaderService(exceptionManager);
            eventLogReaderManager = new EventLogReaderManager(eventLogReaderService, new EventLogCriteriaConverter());
            thrownException = null;
        }

        [TestMethod]
        public void CheckChargeOnLocalhost()
        {
            var serversList = new List<string>();
            serversList.Add("localhost");
            serversList.Add("localhost");
            serversList.Add("localhost");
            serversList.Add("localhost");
            serversList.Add("localhost");

            var crit = new EventLogQueryCriteria();

            var r = eventLogReaderManager.ReadLogs(serversList, crit);
            r = eventLogReaderManager.ReadLogs(serversList, crit);
            r = eventLogReaderManager.ReadLogs(serversList, crit);
            r = eventLogReaderManager.ReadLogs(serversList, crit);
            r = eventLogReaderManager.ReadLogs(serversList, crit);
            r = r.OrderBy(x => x.TimeCreated).ToList();
            Assert.IsTrue(r.Any());
        }

        #endregion Public Methods
    }
}
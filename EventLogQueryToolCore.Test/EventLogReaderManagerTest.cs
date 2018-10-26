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

        private EventLogReaderManager _eventLogReaderManager;
        private EventLogReaderService _eventLogReaderService;
        private IExceptionManager _exceptionManager;
        private Exception _thrownException;

        #endregion Private Fields

        #region Public Methods

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

            var r = _eventLogReaderManager.ReadLogs(serversList, crit);
            r = _eventLogReaderManager.ReadLogs(serversList, crit);
            r = _eventLogReaderManager.ReadLogs(serversList, crit);
            r = _eventLogReaderManager.ReadLogs(serversList, crit);
            r = _eventLogReaderManager.ReadLogs(serversList, crit);
            r = r.OrderBy(x => x.TimeCreated).ToList();
            Assert.IsTrue(r.Any());
        }

        [TestInitialize]
        public void TestInit()
        {
            var exceptionManagerMock = new Mock<IExceptionManager>();
            exceptionManagerMock.Setup(x => x.Raise(_thrownException));
            _exceptionManager = exceptionManagerMock.Object;
            _eventLogReaderService = new EventLogReaderService(_exceptionManager);
            _eventLogReaderManager = new EventLogReaderManager(_eventLogReaderService, new EventLogCriteriaConverter());
            _thrownException = null;
        }

        #endregion Public Methods
    }
}
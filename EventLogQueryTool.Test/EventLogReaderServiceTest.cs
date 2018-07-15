using EventLogQueryTool.Model;
using EventLogQueryTool.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.Eventing.Reader;

namespace EventLogQueryTool.Test
{
    [TestClass]
    public class EventLogReaderServiceTest
    {
        #region Private Fields

        private EventLogReaderService eventLogReaderService;
        private IExceptionManager exceptionManager;
        private Exception thrownException;

        #endregion Private Fields

        #region Public Methods

        [TestInitialize]
        public void TestInit()
        {
            var exceptionManagerMock = new Mock<IExceptionManager>();
            exceptionManagerMock.Setup(x => x.Notify(thrownException));
            exceptionManager = exceptionManagerMock.Object;
            eventLogReaderService = new EventLogReaderService(exceptionManager);
            thrownException = null;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var query = "<QueryList>" +
                        "  <Query Id=\"0\">" +
                        "    <Select>" +
                        "        *[System[((Level = 4) or (Level = 3)) " +
                        "          and TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]" +
                        "    </Select>" +
                        "  </Query>" +
                        "</QueryList>";
            var crit = new EventLogQueryCriteria();
            crit.EventLogEntryTypeList.Add(EventLogEntryLevel.Error);
            var r = eventLogReaderService.ReadLogs("localhost", new EventLogQuery("Application", PathType.LogName, query));
            Assert.IsNotNull(thrownException);
        }

        #endregion Public Methods
    }
}
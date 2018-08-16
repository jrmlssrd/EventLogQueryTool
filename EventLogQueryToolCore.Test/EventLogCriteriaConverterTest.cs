using EventLogQueryToolCore.Model;
using EventLogQueryToolCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace EventLogQueryToolCore.Test
{
    [TestClass]
    public class EventLogCriteriaConverterTest
    {

        #region Private Fields

        private IEventLogCriteriaConverter _eventLogCriteriaConverter;
        private IEventLogReaderService _eventLogReaderService;

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void InformationCriteriaAndLast10Minutes()
        {
            var criteria = new EventLogQueryCriteria();
            criteria.EventLogEntryTypeList.Add(EventLogEntryLevel.Information);
            criteria.DateFrom = DateTime.Now.AddMinutes(-10);
            var queryString = _eventLogCriteriaConverter.Convert(criteria);
            var r = _eventLogReaderService.ReadLogs("localhost", new EventLogQuery("Application", PathType.LogName, queryString));
            Assert.IsTrue(r.All(x => x.TimeCreated >= criteria.DateFrom));
        }

        [TestMethod]
        public void InformationCriteriaOnly()
        {
            var criteria = new EventLogQueryCriteria();
            criteria.EventLogEntryTypeList.Add(EventLogEntryLevel.Information);
            var queryString = _eventLogCriteriaConverter.Convert(criteria);
            var r = _eventLogReaderService.ReadLogs("localhost", new EventLogQuery("Application", PathType.LogName, queryString));
        }

        [TestMethod]
        public void NoCriteria()
        {
            var criteria = new EventLogQueryCriteria();
            var queryString = _eventLogCriteriaConverter.Convert(criteria);
            var r = _eventLogReaderService.ReadLogs("localhost", new EventLogQuery("Application", PathType.LogName, queryString));
        }

        [TestMethod]
        public void OneProviderOnly()
        {
            var criteria = new EventLogQueryCriteria();
            criteria.ProvidersName.Add("TTT");
            var queryString = _eventLogCriteriaConverter.Convert(criteria);
            var r = _eventLogReaderService.ReadLogs("localhost", new EventLogQuery("Application", PathType.LogName, queryString));
        }
        [TestMethod]
        public void MultipleProviders()
        {
            var criteria = new EventLogQueryCriteria();
            criteria.ProvidersName.Add("TTT");
            criteria.ProvidersName.Add("PPP");
            criteria.ProvidersName.Add("RRR");
            var queryString = _eventLogCriteriaConverter.Convert(criteria);
            var r = _eventLogReaderService.ReadLogs("localhost", new EventLogQuery("Application", PathType.LogName, queryString));
        }



        [TestInitialize]
        public void TestInit()
        {
            _eventLogCriteriaConverter = new EventLogCriteriaConverter();
            _eventLogReaderService = new EventLogReaderService(new ExceptionManagerMock());
        }

        #endregion Public Methods

    }
}
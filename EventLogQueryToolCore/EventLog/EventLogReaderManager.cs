﻿using EventLogQueryToolCore.Model;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Threading;

namespace EventLogQueryToolCore.Services
{
    public class EventLogReaderManager : IEventLogReaderManager
    {
        #region Private Fields

        private IEventLogCriteriaConverter _eventLogCriteriaConverter;
        private IEventLogReaderService _eventLogReaderService;

        #endregion Private Fields

        #region Public Constructors

        public EventLogReaderManager(IEventLogReaderService eventLogReaderService, IEventLogCriteriaConverter eventLogCriteriaConverter)
        {
            _eventLogReaderService = eventLogReaderService;
            _eventLogCriteriaConverter = eventLogCriteriaConverter;
        }

        #endregion Public Constructors

        #region Public Methods

        public IList<EventRecord> ReadLogs(IList<string> serversList, EventLogQueryCriteria criteria)
        {
            EventLogQuery query = new EventLogQuery("Application", PathType.LogName, _eventLogCriteriaConverter.Convert(criteria));
            List<EventRecord> returnList = new List<EventRecord>();
            List<Thread> threadList = new List<Thread>();
            foreach (var server in serversList)
            {
                var t = new Thread(() => returnList.AddRange(_eventLogReaderService.ReadLogs(server, query)));
                t.Start();
                threadList.Add(t);
            }

            threadList.ForEach(x => x.Join());

            return returnList;
        }

        public IList<EventRecord> ReadLogs(string server, EventLogQueryCriteria criteria)
        {
            var serversList = new List<string>() { server };
            return ReadLogs(serversList, criteria);
        }

        #endregion Public Methods
    }
}
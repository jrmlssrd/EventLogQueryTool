using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace EventLogQueryTool.Services
{
    public class EventLogReaderService : IEventLogReaderService
    {
        #region Private Fields

        private IExceptionManager _exceptionManager;

        #endregion Private Fields

        #region Public Constructors

        public EventLogReaderService(IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public IList<EventRecord> ReadLogs(string server, EventLogQuery criteria)
        {
            IList<EventRecord> eventRecords = new List<EventRecord>();
            EventLogReader logReader;
            criteria.Session = new EventLogSession(server);
            try
            {
                logReader = new EventLogReader(criteria);
                for (EventRecord current = logReader.ReadEvent(); current != null; current = logReader.ReadEvent())
                {
                    eventRecords.Add(current);
                }
            }
            catch (Exception ex)
            {
                _exceptionManager.Notify(ex);
            }

            return eventRecords;
        }

        #endregion Public Methods
    }
}
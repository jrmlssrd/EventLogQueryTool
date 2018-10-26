using EventLogQueryToolCore.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace EventLogQueryToolCore.Services
{
    public class EventLogReaderService : IEventLogReaderService
    {

        #region Private Fields

        private IExceptionManager _exceptionManager;

        #endregion Private Fields

        #region Public Constructeurs

        public EventLogReaderService(IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        #endregion Public Constructeurs

        #region Public Methods

        public IList<EventRecord> ReadLogs(string server, EventLogQuery criteria, IList<string> contains)
        {
            IList<EventRecord> eventRecords = new List<EventRecord>();

            criteria.Session = new EventLogSession(server);
            try
            {
                eventRecords = LaunchCriteria(criteria, contains);
            }
            catch (Exception ex)
            {
                // If we have error, try again, some time it fail for no reason.
                try
                {
                    eventRecords = LaunchCriteria(criteria, contains);
                }
                catch (Exception)
                {
                    _exceptionManager.Raise(ex);
                }
            }

            return eventRecords;
        }

        #endregion Public Methods

        #region Private Methods

        private static bool EventContains(EventRecord eventRecord, IList<string> contains)
        {
            string msg = eventRecord.FormatDescription();
            foreach (var ev in contains)
            {
                if (msg.IndexOf(ev, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        private static IList<EventRecord> LaunchCriteria(EventLogQuery criteria, IList<string> contains)
        {
            IList<EventRecord> eventRecords = new List<EventRecord>();
            EventLogReader logReader = new EventLogReader(criteria);
            for (EventRecord current = logReader.ReadEvent(); current != null; current = logReader.ReadEvent())
            {
                eventRecords.Add(current);
            }

            if (contains != null && contains.Any() && eventRecords.Any())
            {
                eventRecords = eventRecords.Where(x => EventContains(x, contains)).ToList();
            }

            return eventRecords;
        }

        #endregion Private Methods
    }
}
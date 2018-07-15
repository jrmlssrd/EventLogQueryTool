using EventLogQueryTool.Model;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace EventLogQueryTool.Services
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
            IList<EventRecord> returnList = new List<EventRecord>();

            foreach (var server in serversList)
            {
                _eventLogReaderService.ReadLogs(server, query);
            }

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
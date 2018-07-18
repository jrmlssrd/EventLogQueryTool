using EventLogQueryToolCore.Model;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace EventLogQueryToolCore.Services
{
    public interface IEventLogReaderManager
    {
        #region Public Methods

        IList<EventRecord> ReadLogs(IList<string> machine, EventLogQueryCriteria criteria);

        IList<EventRecord> ReadLogs(string machine, EventLogQueryCriteria criteria);

        #endregion Public Methods
    }
}
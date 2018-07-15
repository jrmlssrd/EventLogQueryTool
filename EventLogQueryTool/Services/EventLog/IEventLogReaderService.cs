using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace EventLogQueryTool.Services
{
    public interface IEventLogReaderService
    {
        #region Public Methods

        IList<EventRecord> ReadLogs(string machine, EventLogQuery criteria);

        #endregion Public Methods
    }
}
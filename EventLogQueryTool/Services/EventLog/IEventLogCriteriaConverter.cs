using EventLogQueryTool.Model;

namespace EventLogQueryTool.Services
{
    public interface IEventLogCriteriaConverter
    {
        #region Public Methods

        string Convert(EventLogQueryCriteria eventLogQueryCriteria);

        #endregion Public Methods
    }
}
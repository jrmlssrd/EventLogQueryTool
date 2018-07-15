using EventLogQueryToolCore.Model;

namespace EventLogQueryToolCore.Services
{
    public interface IEventLogCriteriaConverter
    {
        #region Public Methods

        string Convert(EventLogQueryCriteria eventLogQueryCriteria);

        #endregion Public Methods
    }
}
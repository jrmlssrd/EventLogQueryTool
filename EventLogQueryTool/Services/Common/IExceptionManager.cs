using System;

namespace EventLogQueryTool.Services
{
    public interface IExceptionManager
    {
        #region Public Methods

        void Notify(Exception exception);

        #endregion Public Methods
    }
}
using System;

namespace EventLogQueryToolCore.Common
{
    public interface IExceptionManager
    {
        #region Public Methods

        void Raise(Exception exception);

        #endregion Public Methods
    }
}
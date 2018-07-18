using System;

namespace EventLogQueryToolCore.Common
{
    public class ExceptionManager : IExceptionManager
    {
        #region Public Methods

        public void Raise(Exception exception)
        {
            throw exception;
        }

        #endregion Public Methods
    }
}
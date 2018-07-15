using EventLogQueryToolCore.Common;
using System;

namespace EventLogQueryToolCore.Test
{
    internal class ExceptionManagerMock : IExceptionManager
    {
        #region Public Methods

        public void Raise(Exception exception)
        {
            throw exception;
        }

        #endregion Public Methods
    }
}
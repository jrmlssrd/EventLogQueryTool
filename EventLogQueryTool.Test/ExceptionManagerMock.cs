using EventLogQueryTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogQueryTool.Test
{
    internal class ExceptionManagerMock : IExceptionManager
    {
        #region Public Methods

        public void Notify(Exception exception)
        {
            throw exception;
        }

        #endregion Public Methods
    }
}
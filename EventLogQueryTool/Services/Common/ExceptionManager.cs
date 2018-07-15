using System;

namespace EventLogQueryTool.Services
{
    public class ExceptionManager : IExceptionManager
    {
        #region Public Methods

        public void Notify(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        #endregion Public Methods
    }
}
using EventLogQueryToolCore.Common;
using System;
using System.Windows;

namespace EventLogQueryTool.Services
{
    internal class UIExceptionManager : IExceptionManager
    {
        #region Public Constructors

        public UIExceptionManager()
        {
            // Popup manager
        }

        #endregion Public Constructors

        #region Public Methods

        public void Raise(Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }

        #endregion Public Methods
    }
}
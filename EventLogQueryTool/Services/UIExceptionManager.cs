using System;
using System.Windows;
using EventLogQueryToolCore.Common;

namespace EventLogQueryTool.Services
{
    class UIExceptionManager : IExceptionManager
    {

        public UIExceptionManager()
        { 
            // Popup manager
        }

        public void Raise(Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }
    }
}

using System;
using System.Collections.Generic;

namespace EventLogQueryTool.Model
{
    [Serializable]
    public class ServerConfiguration
    {
        #region Public Properties

        public List<ServerCategory> CategoryList { get; set; }

        #endregion Public Properties
    }
}
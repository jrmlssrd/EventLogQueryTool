using System;
using System.Collections.Generic;

namespace EventLogQueryTool.Model
{
    [Serializable]
    public class ServerCategory
    {
        #region Public Properties

        public string CategoryName { get; set; }

        public List<Server> ServerList { get; set; }

        #endregion Public Properties
    }
}
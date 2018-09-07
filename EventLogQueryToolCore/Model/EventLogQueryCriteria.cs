using System;
using System.Collections.Generic;

namespace EventLogQueryToolCore.Model
{
    /// <summary>
    /// Criteria object
    /// </summary>
    public class EventLogQueryCriteria
    {
        #region Public Constructors

        public EventLogQueryCriteria()
        {
            ServersList = new List<string>();
            EventLogEntryTypeList = new List<EventLogEntryLevel>();
            ProvidersName = new List<string>();
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public IList<EventLogEntryLevel> EventLogEntryTypeList { get; set; }

        public IList<string> ProvidersName { get; set; }

        public string DescriptionContains { get; set; }

        public IList<string> ServersList { get; set; }

        #endregion Public Properties
    }
}
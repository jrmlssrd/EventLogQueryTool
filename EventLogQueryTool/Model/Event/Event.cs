using System;
using System.Diagnostics.Eventing.Reader;

namespace EventLogQueryTool.Model
{
    [Serializable]
    public class Event
    {

        #region Private Fields

        private EventRecord _eventRecord;

        #endregion Private Fields

        #region Public Constructors

        public Event(EventRecord eventRecord)
        {
            _eventRecord = eventRecord;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Description { get { return _eventRecord.FormatDescription(); } }

        public EventRecord EventRecord
        {
            get { return _eventRecord; }
            set { _eventRecord = value; }
        }

        #endregion Public Properties

    }
}
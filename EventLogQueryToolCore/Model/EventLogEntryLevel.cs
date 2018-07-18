using EventLogQueryTool.Common;
using EventLogQueryToolCore.Common;
using EventLogQueryToolCore.Properties;
using System.ComponentModel;

namespace EventLogQueryToolCore.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EventLogEntryLevel
    {
        [LocalizedDescription("LBL_INFORMATION", typeof(Resources))]
        Information = 4,

        [LocalizedDescription("LBL_WARNING", typeof(Resources))]
        Warning = 3,

        [LocalizedDescription("LBL_ERROR", typeof(Resources))]
        Error = 2
    }
}
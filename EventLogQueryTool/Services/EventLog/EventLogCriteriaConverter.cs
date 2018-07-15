using EventLogQueryTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLogQueryTool.Services
{
    public class EventLogCriteriaConverter : IEventLogCriteriaConverter
    {
        #region Private Fields

        private const string AND = " and ";
        private const string OR = " or ";
        private const string QUERY_BEGIN = "<QueryList><Query Id=\"0\"><Select>";
        private const string QUERY_ENDING = "</Select></Query></QueryList>";

        #endregion Private Fields

        #region Public Methods

        public string Convert(EventLogQueryCriteria eventLogQueryCriteria)
        {
            var query = "<QueryList>" +
                       "  <Query Id=\"0\">" +
                       "    <Select>" +
                       "        *[System[((Level = 4) or (Level = 3)) " +
                       "          and TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]" +
                       "    </Select>" +
                       "  </Query>" +
                       "</QueryList>";

            StringBuilder queryString = new StringBuilder();
            queryString.Append("*[System");
            var criteriaList = new List<string>();
            AddEventLogLevelCriteria(eventLogQueryCriteria, criteriaList);
            AddEventLogDateCriteria(eventLogQueryCriteria, criteriaList);

            if (criteriaList.Any())
            {
                queryString.Append("[");
                queryString.Append(String.Join(AND, criteriaList));
                queryString.Append("]");
            }
            queryString.Append("]");

            return string.Concat(QUERY_BEGIN, queryString.ToString(), QUERY_ENDING);
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddEventLogDateCriteria(EventLogQueryCriteria eventLogQueryCriteria, List<string> criteriaList)
        {
            if (eventLogQueryCriteria.DateFrom.HasValue)
            {
                var crit = "(";
                crit += string.Format("TimeCreated[@SystemTime &gt;= '{0}']", ConvertDate(eventLogQueryCriteria.DateFrom.Value));
                crit += ")";
                criteriaList.Add(crit);
            }

            if (eventLogQueryCriteria.DateTo.HasValue)
            {
                var crit = "(";
                crit += string.Format("TimeCreated[@SystemTime &lt;= '{0}']", ConvertDate(eventLogQueryCriteria.DateTo.Value));
                crit += ")";
                criteriaList.Add(crit);
            }
        }

        private static void AddEventLogLevelCriteria(EventLogQueryCriteria eventLogQueryCriteria, List<string> criteriaList)
        {
            if (eventLogQueryCriteria.EventLogEntryTypeList.Any())
            {
                var crit = "(";
                var levelList = new List<string>();
                foreach (var level in eventLogQueryCriteria.EventLogEntryTypeList)
                {
                    levelList.Add(string.Format("(Level = {0})", (int)level));
                }
                crit += String.Join(OR, levelList);
                crit += ")";
                criteriaList.Add(crit);
            }
        }

        private static string ConvertDate(DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffff0000Z");
        }

        #endregion Private Methods
    }
}
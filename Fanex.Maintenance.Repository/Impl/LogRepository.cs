// --------------------------------------------------------------------------
// <copyright file="LogRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Log repository.
    /// </summary>
    public class LogRepository : ReadonlyRepository<Log>
    {
        /// <summary>
        /// Override return Logs.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of log.</returns>
        public IEnumerable<Log> GetLog(ref GetLogCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();

                IEnumerable<Log> list = db.Query<Log, LogCategory, Machine, Log>(
                    (l, c, m) =>
                    {
                        l.Category = c;
                        l.Machine = m;
                        return l;
                    }, 
                    "CategoryID,MachineId", 
                    param);

                criteria.TotalRows = Convert.ToInt32(param["totalrows"]);

                return list;
            }
        }

        /// <summary>
        /// Get details message of log.
        /// </summary>
        /// <param name="logId">Log object ID.</param>
        /// <returns>Log message.</returns>
        public string GetLogMessage(long logId)
        {
            using (var db = new ObjectDb("Logging_SelMessage"))
            {
                return db.ExecuteScalar(new { logid = logId }).ToString();
            }
        }

        /// <summary>
        /// Remove log.
        /// </summary>
        /// <param name="from">From DateTime.</param>
        /// <returns>Number of deleted rows.</returns>
        public int DeleteLogs(DateTime from)
        {
            int row = 0;

            using (var db = new ObjectDb("Logging_Del"))
            {
                row = db.ExecuteNonQuery(new { from = from });
            }

            return row;
        }

        /// <summary>
        /// Update log's monitor status.
        /// </summary>
        /// <param name="logIds">Log object IDs.</param>
        /// <param name="monitoringUserId">Monitoring user ID.</param>
        /// <param name="isMonitored">Is monitored or not.</param>
        /// <returns>Number of rows.</returns>
        public int MonitorLog(string logIds, int? monitoringUserId, bool isMonitored)
        {
            int row = 0;

            using (var db = new ObjectDb("Logging_Monitor"))
            {
                row = db.ExecuteNonQuery(new { LogIds = logIds, @MonitoringUserId = monitoringUserId, IsMonitored = isMonitored });
            }

            return row;
        }
    }
}
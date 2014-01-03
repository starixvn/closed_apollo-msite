// --------------------------------------------------------------------------
// <copyright file="LogService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Repository.Impl;

    /// <summary>
    /// Log service.
    /// </summary>
    public class LogService
    {
        private readonly LogRepository _logRepository = new LogRepository();

        private int? _categoryId;

        private int? _machineId;

        private bool? _isProduction;

        private bool? _isMonitored;

        private string _severity;

        private DateTime _dateFrom;

        private DateTime _dateTo;

        private int _page;

        private int _size;

        private LogService()
        {
            _categoryId = 0;
            _severity = "All";
            _machineId = 0;
            _dateFrom = DateTime.Now;
            _dateTo = DateTime.Now.AddDays(-1);
            _page = 0;
            _size = 100;
        }

        /// <summary>
        /// Create log service.
        /// </summary>
        /// <returns>Log service object.</returns>
        public static LogService Create()
        {
            return new LogService();
        }

        /// <summary>
        /// By categoryId.
        /// </summary>
        /// <param name="categoryId">Category ID.</param>
        /// <returns>Log service object.</returns>
        public LogService With(int categoryId)
        {
            this._categoryId = categoryId;

            return this;
        }

        /// <summary>
        /// By categoryId and Severity.
        /// </summary>
        /// <param name="categoryId">Category ID.</param>
        /// <param name="severity">Severity level.</param>
        /// <returns>Log service object.</returns>
        public LogService With(int? categoryId, string severity)
        {
            this._categoryId = categoryId;
            this._severity = severity;

            return this;
        }

        /// <summary>
        /// By CategoryId, MachineId and Severity.
        /// </summary>
        /// <param name="categoryId">Category ID.</param>
        /// <param name="machineId">Machine ID.</param>
        /// <param name="isProduction">Production or not.</param>
        /// <param name="isMonitored">Monitored or not.</param>
        /// <param name="severity">Severity level.</param>
        /// <returns>Log service object.</returns>
        public LogService With(int? categoryId, int? machineId, bool? isProduction, bool? isMonitored, string severity)
        {
            this._categoryId = categoryId;
            this._machineId = machineId;
            this._severity = severity;
            this._isProduction = isProduction;
            this._isMonitored = isMonitored;

            return this;
        }

        /// <summary>
        /// Update from date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns>Log service object.</returns>
        public LogService From(DateTime fromDate)
        {
            this._dateFrom = fromDate;

            return this;
        }

        /// <summary>
        /// Update to date.
        /// </summary>
        /// <param name="toDate">To date value.</param>
        /// <returns>Log service object.</returns>
        public LogService To(DateTime toDate)
        {
            this._dateTo = toDate;

            return this;
        }

        /// <summary>
        /// Pagination items.
        /// </summary>
        /// <param name="page">Page index.</param>
        /// <param name="size">Page size.</param>
        /// <returns>Log service object.</returns>
        public LogService WithPagination(int page, int size)
        {
            this._page = page;
            this._size = size;

            return this;
        }
        
        /// <summary>
        /// Get message detail.
        /// </summary>
        /// <param name="logId">Log object ID.</param>
        /// <returns>Message string.</returns>
        public string GetMessage(long logId)
        {
            string message = _logRepository.GetLogMessage(logId);

            return message;
        }

        /// <summary>
        /// Get list logs.
        /// </summary>
        /// <param name="totalRows">Number of rows.</param>
        /// <returns>List of log.</returns>
        public IEnumerable<Log> GetLogs(ref int totalRows)
        {
            GetLogCriteria criteria = new GetLogCriteria
            {
                CategoryId = this._categoryId != null ? this._categoryId : 0,
                From = this._dateFrom,
                To = this._dateTo,
                Page = this._page,
                Size = this._size,
                Severity = this._severity,
                MachineId = this._machineId,
                IsProduction = this._isProduction,
                IsMonitored = this._isMonitored
            };

            var list = _logRepository.GetLog(ref criteria);

            totalRows = criteria.TotalRows;

            return list;
        }

        /// <summary>
        /// Remove log before date.
        /// </summary>
        /// <returns>Command result.</returns>
        public int RemoveLogs()
        {
            return _logRepository.DeleteLogs(this._dateFrom);
        }

        /// <summary>
        /// Update log's monitored status.
        /// </summary>
        /// <param name="logIds">Log object IDs.</param>
        /// <param name="monitoringUserId">Monitoring user ID.</param>
        /// <param name="isMonitored">Is monitored or not.</param>
        /// <returns>Command result.</returns>
        public int MonitorLog(string logIds, int? monitoringUserId, bool isMonitored)
        {
            return _logRepository.MonitorLog(logIds, monitoringUserId, isMonitored);
        }
    }
}
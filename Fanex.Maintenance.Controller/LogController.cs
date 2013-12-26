// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
//   <change who="Jason.Ly" date="2013.8.7">Modify Get Logs</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl;

    /// <summary>
    /// Log controller.
    /// </summary>
    [Authorize]
    public class LogController : BaseController
    {
        #region Member
        /// <summary>
        /// Log Service.
        /// </summary>
        private readonly LogService _logService;

        /// <summary>
        /// Log Category Service.
        /// </summary>
        private readonly LogCategoryService _categoryService;

        #endregion Member

        #region Ctor

        public LogController()
            : this(null, null)
        {
        }

        public LogController(LogService logService, LogCategoryService categoryService)
        {
            _logService = logService ?? LogService.Create();
            _categoryService = categoryService ?? LogCategoryService.Create();
        }

        #endregion Ctor

        #region Action

        /// <summary>
        /// Log list page.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            InitDataView();

            ViewBag.ListLog = null;

            var criteria = new GetLogCriteria
            {
                CategoryId = 0,
                MachineId = 0,
                IsProduction = null,
                IsMonitored = null,
                From = DateTime.Now,
                To = DateTime.Now,
                Page = 0,
                Size = 10,
                Severity = "All"
            };

            return View(criteria);
        }

        /// <summary>
        /// Get log page.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult Index(GetLogCriteria criteria)
        {
            InitDataView();

            var totalRows = 0;

            ////Get logs by date
            IEnumerable<Log> listLogs = _logService.With(
                criteria.CategoryId, 
                criteria.MachineId,
                criteria.IsProduction, 
                criteria.IsMonitored, 
                criteria.Severity)
                .From(criteria.From)
                .To(criteria.To)
                .WithPagination(criteria.Page, criteria.Size)
                .GetLogs(ref totalRows);

            return Json(new
            {
                TotalRows = totalRows,
                ListLog = listLogs
            });
        }

        /// <summary>
        /// Get details message.
        /// </summary>
        /// <param name="id">Log message ID.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult GetLogMessage(int id)
        {
            if (Request.IsAjaxRequest())
            {
                return Json(_logService.GetMessage(id));
            }

            return Json(null);
        }

        #endregion Action

        #region Ajax

        /// <summary>
        /// Remove Log.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult RemoveLog()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(_logService.From(DateTime.Now.AddDays(-5)).RemoveLogs());
            }

            return Json(0);
        }

        /// <summary>
        /// Update log's monitored status.
        /// </summary>
        /// <param name="logId">Log object ID.</param>
        /// <param name="isMonitored">Is monitored or not.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult MonitorLog(string logId, bool isMonitored)
        {
            string updatedUsername = string.Empty;

            if (Request.IsAjaxRequest())
            {
                int? userId = this.CurrentSession.UserId;
                userId = isMonitored ? userId : null;
                int affectedRows = _logService.MonitorLog(logId, userId, isMonitored);
                updatedUsername = affectedRows != 0 ? this.CurrentSession.Username : string.Empty;
            }

            return Json(updatedUsername);
        }

        #endregion Ajax

        #region Private

        /// <summary>
        /// Initialize data for View.
        /// </summary>
        private void InitDataView()
        {
            // Default selection for drop-down
            var defaultSelection = new SelectListItem
            {
                Text = ViewResource.All,
                Value = "0",
                Selected = true
            };

            // Get Category from DB
            List<SelectListItem> listCategory = _categoryService.GetAll().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryID.ToString()
            }).ToList();
            listCategory.Insert(0, defaultSelection);
            ViewBag.ListCategory = listCategory;
        }

        #endregion Private
    }
}
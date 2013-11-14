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

        /// <summary>
        /// Machine Service.
        /// </summary>
        private readonly MachineService _machineService;

        #endregion Member

        #region Ctor

        public LogController()
            : this(null, null, null)
        {
        }

        public LogController(LogService logService, LogCategoryService categoryService, MachineService machineService)
        {
            _logService = logService ?? LogService.Create();
            _categoryService = categoryService ?? LogCategoryService.Create();
            _machineService = machineService ?? MachineService.Create();
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
            InitPageSize();

            ViewBag.ListLog = null;

            GetLogCriteria criteria = new GetLogCriteria
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
            InitPageSize();

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
        /// Initialize Page Size.
        /// </summary>
        private void InitPageSize()
        {
            List<SelectListItem> listPageSize = new List<SelectListItem>();
            listPageSize.Add(new SelectListItem
            {
                Text = "10",
                Value = "10"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "30",
                Value = "30"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "50",
                Value = "50"
            });

            listPageSize.Add(new SelectListItem
            {
                Text = "100",
                Value = "100"
            });

            ViewBag.PageSize = listPageSize;
        }

        /// <summary>
        /// Initialize Machine Types data.
        /// </summary>
        private void InitMachineTypes()
        {
            List<SelectListItem> machineTypes = new List<SelectListItem>();
            machineTypes.Add(new SelectListItem
            {
                Text = ViewResource.All,
                Value = "null"
            });
            machineTypes.Add(new SelectListItem
            {
                Text = ViewResource.MachineType_Production,
                Value = "true"
            });
            machineTypes.Add(new SelectListItem
            {
                Text = ViewResource.MachineType_UAT,
                Value = "false"
            });

            ViewBag.MachineTypes = machineTypes;
        }

        /// <summary>
        /// Initialize Log Status data.
        /// </summary>
        private void InitLogStatus()
        {
            List<SelectListItem> logStatus = new List<SelectListItem>();
            logStatus.Add(new SelectListItem
            {
                Text = ViewResource.All,
                Value = "null"
            });
            logStatus.Add(new SelectListItem
            {
                Text = ViewResource.LogStatus_Monitored,
                Value = "true"
            });
            logStatus.Add(new SelectListItem
            {
                Text = ViewResource.LogStatus_Unmonitored,
                Value = "false"
            });

            ViewBag.LogStatus = logStatus;
        }

        /// <summary>
        /// Initialize Severities data.
        /// </summary>
        private void InitSeverities()
        {
            ////Init Serverity
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = ViewResource.All,
                Value = "All"
            });
            list.Add(new SelectListItem
            {
                Text = ViewResource.Severity_Critical,
                Value = "Critical"
            });
            list.Add(new SelectListItem
            {
                Text = ViewResource.Severity_Error,
                Value = "Error"
            });
            list.Add(new SelectListItem
            {
                Text = ViewResource.Severity_Information,
                Value = "Information"
            });
            list.Add(new SelectListItem
            {
                Text = ViewResource.Severity_Verbose,
                Value = "Verbose"
            });
            list.Add(new SelectListItem
            {
                Text = ViewResource.Severity_Warning,
                Value = "Warning"
            });
            ViewBag.ListSeverity = list;
        }

        /// <summary>
        /// Initialize data for View.
        /// </summary>
        private void InitDataView()
        {
            InitSeverities();
            InitLogStatus();

            // Default selection for drop-down
            SelectListItem defaultSelection = new SelectListItem
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
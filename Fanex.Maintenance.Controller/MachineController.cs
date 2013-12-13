// --------------------------------------------------------------------------
// <copyright file="MachineController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.8">Created</change>
//   <change who="Edward.Nguyen" date="2013.8.12">Edit GetMachineByType</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Service.Impl;

    /// <summary>
    /// Machine Controller.
    /// </summary>
    [Authorize]
    public class MachineController : BaseController
    {
        #region Member
        /// <summary>
        /// Machine Service.
        /// </summary>
        private readonly MachineService _machineService;

        #endregion Member

        #region Constructor

        public MachineController()
            : this(null)
        {
        }

        public MachineController(MachineService machineService)
        {
            _machineService = machineService ?? MachineService.Create();
        }

        #endregion Constructor

        #region Action

        /// <summary>
        /// Get machines.
        /// </summary>
        /// <param name="machineType">Machine type.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Index(bool? machineType = null)
        {
            var machines = _machineService.GetByType(machineType);

            ViewBag.Machines = machines;
            ViewBag.MachineType = machineType;
            InitMachineTypes();

            return View(machines);
        }

        /// <summary>
        /// Get machines.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult Index(GetMachinesByTypeCriteria criteria)
        {
            return Index(criteria.MachineType);
        }

        /// <summary>
        /// Edit machine.
        /// </summary>
        /// <param name="id">Machine ID.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult EditMachine(int id)
        {
            var machine = _machineService.GetMachine(id);
            return View(machine);
        }

        /// <summary>
        /// Edit machine.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult EditMachine(Machine machine)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = _machineService.UpdateMachine(machine.MachineId, machine.IsProduction, machine.TimeZone);
            }

            return View(machine);
        }

        /// <summary>
        /// Insert machine.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult InsertMachine()
        {
            var machine = new Machine
            {
                IsProduction = false,
                TimeZone = 7
            };
            return View(machine);
        }

        /// <summary>
        /// Insert machine.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult InsertMachine(Machine machine)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = _machineService.InsertMachine(machine.MachineName, machine.IsProduction, machine.TimeZone);
            }

            return View(machine);
        }

        #endregion Action

        #region Ajax

        /// <summary>
        /// Get machines.
        /// </summary>
        /// <param name="machineType">Machine type.</param>
        /// <returns>JSON result.</returns>
        [HttpGet]
        public JsonResult GetMachinesByType(bool? machineType)
        {
            var machines = new List<Machine>();

            if (Request.IsAjaxRequest())
            {
                machines = _machineService.GetByType(machineType).ToList();
            }

            return Json(machines, JsonRequestBehavior.AllowGet);
        }

        #endregion Ajax

        #region Private

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

        #endregion Private
    }
}
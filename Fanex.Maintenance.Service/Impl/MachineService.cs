// --------------------------------------------------------------------------
// <copyright file="MachineService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Repository.Impl;
    using Fanex.NAP.Framework.Command;

    /// <summary>
    /// Machine Service.
    /// </summary>
    public class MachineService
    {
        private readonly MachineRepository _machineRepository = new MachineRepository();

        private MachineService()
        {
        }

        /// <summary>
        /// Create machine service.
        /// </summary>
        /// <returns>Machine service.</returns>
        public static MachineService Create()
        {
            return new MachineService();
        }

        /// <summary>
        /// Get all machines.
        /// </summary>
        /// <returns>List of machine.</returns>
        public IEnumerable<Machine> GetAll()
        {
            var criteria = new GetAllMachinesCriteria();

            return _machineRepository.Fetch(criteria);
        }

        /// <summary>
        /// Get machines by machine type.
        /// </summary>
        /// <param name="machineType">Machine type.</param>
        /// <returns>List of machine.</returns>
        public IEnumerable<Machine> GetByType(bool? machineType)
        {
            var criteria = new GetMachinesByTypeCriteria
            {
                MachineType = machineType
            };

            return _machineRepository.Fetch(criteria);
        }

        /// <summary>
        /// Get machine by machine id.
        /// </summary>
        /// <param name="id">Machine ID.</param>
        /// <returns>Machine object.</returns>
        public Machine GetMachine(int id)
        {
            var criteria = new GetMachineByIdCriteria
            {
                MachineId = id
            };

            return _machineRepository.Get<Machine>(criteria);
        }

        /// <summary>
        /// Insert machine.
        /// </summary>
        /// <param name="machineName">Machine name.</param>
        /// <param name="machineType">Machine type.</param>
        /// <param name="timeZone">Machine time zone.</param>
        /// <returns>Command result.</returns>
        public CommandResult InsertMachine(string machineName, bool machineType, short timeZone)
        {
            var criteria = new InsertMachineCriteria
            {
                MachineName = machineName,
                IsProduction = machineType,
                Timezone = timeZone
            };

            return _machineRepository.InsertMachine(criteria);
        }

        /// <summary>
        /// Update machine.
        /// </summary>
        /// <param name="machineId">Machine ID.</param>
        /// <param name="machineType">Machine type.</param>
        /// <param name="timeZone">Machine time zone.</param>
        /// <returns>Command result.</returns>
        public CommandResult UpdateMachine(int machineId, bool machineType, short timeZone)
        {
            var criteria = new UpdateMachineCriteria
            {
                MachineId = machineId,
                IsProduction = machineType,
                Timezone = timeZone
            };

            return _machineRepository.UpdateMachine(criteria);
        }
    }
}
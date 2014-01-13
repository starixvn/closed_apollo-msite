// --------------------------------------------------------------------------
// <copyright file="MachineRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.NAP.Core.Criteria.Misc;
    using Fanex.NAP.Core.Repository.Impl;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Machine Repository.
    /// </summary>
    public class MachineRepository : BaseRepository<Machine>
    {
        /// <summary>
        /// Return Machine list.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of machine.</returns>
        public override IEnumerable<Machine> Fetch(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                return db.Query<Machine>(param);
            }
        }

        /// <summary>
        /// Insert machine.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public CommandResult InsertMachine(InsertMachineCriteria criteria)
        {
            Hashtable hash = criteria.GetSpParams();

            hash = (new ResultCriteria()).AddSpParams(hash);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(hash);
                return GetCommandResult(hash);
            }
        }

        /// <summary>
        /// Update machine.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public CommandResult UpdateMachine(UpdateMachineCriteria criteria)
        {
            Hashtable hash = criteria.GetSpParams();

            hash = (new ResultCriteria()).AddSpParams(hash);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(hash);
                return GetCommandResult(hash);
            }
        }
    }
}
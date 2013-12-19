// --------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.8">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.9">Add Fetchm, Get Datatable Function</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Base repository.
    /// </summary>
    /// <typeparam name="T">The element type of the class.</typeparam>
    public class BaseRepository<T> : ReadonlyRepository<T>
    {
        /// <summary>
        /// Get Data table from Criteria.
        /// </summary>
        /// <param name="criteria">A Criteria.</param>
        /// <returns>A table contains information.</returns>
        public virtual DataTable GetDataTable(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                DataSet ds = db.ExecuteDataSet(criteria.GetSpParams());
                if (!ReferenceEquals(ds, null) && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }

                return null;
            }
        }

        /// <summary>
        /// Fetch item.
        /// </summary>
        /// <typeparam name="S">Object class.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="mapObjectFunction">Map object function.</param>
        /// <returns>List of object.</returns>
        public virtual IEnumerable<S> Fetch<S>(
            ICriteria criteria,
            Func<IDataReader, S> mapObjectFunction) where S : class
        {
            IList<S> listObject = new List<S>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        listObject.Add(mapObjectFunction(reader));
                    }
                }
            }

            return listObject;
        }

        /// <summary>
        /// Get command result.
        /// </summary>
        /// <param name="hash">Hash command result.</param>
        /// <returns>Command result.</returns>
        public CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }
    }
}
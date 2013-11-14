// --------------------------------------------------------------------------
// <copyright file="LogCategoryRepository.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Maintenance.Entity.Model;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Log category.
    /// </summary>
    public class LogCategoryRepository : ReadonlyRepository<LogCategory>
    {
        /// <summary>
        /// Override return Category.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of log category.</returns>
        public override IEnumerable<LogCategory> Fetch(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<LogCategory>();
            }
        }
    }
}
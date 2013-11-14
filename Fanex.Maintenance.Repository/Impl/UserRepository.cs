// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl
{
    using System;
    using System.Collections;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserRepository : ReadonlyRepository<dynamic>, IUserRepository
    {
        /// <summary>
        /// Check login
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public ICommandResult Login(ICriteria criteria)
        {
            try
            {
                Hashtable hash = criteria.GetSpParams();
                hash = (new ResultCriteria()).AddSpParams(hash);
                
                using (ObjectDb objectDb = new ObjectDb(criteria.GetSettingKey()))
                {                    
                    objectDb.ExecuteNonQuery(hash);
                    
                    return this.GetCommandResult(hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
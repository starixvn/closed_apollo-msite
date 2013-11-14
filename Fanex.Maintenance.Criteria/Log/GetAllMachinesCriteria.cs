// --------------------------------------------------------------------------
// <copyright file="GetAllMachinesCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Log
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get all machines criteria.
    /// </summary>
    public class GetAllMachinesCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Logging_Machine_SelectAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
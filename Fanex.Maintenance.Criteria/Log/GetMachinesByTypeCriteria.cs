// --------------------------------------------------------------------------
// <copyright file="GetMachinesByTypeCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.8">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Log
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get machine by type criteria.
    /// </summary>
    public class GetMachinesByTypeCriteria : CriteriaBase
    {
        public bool? MachineType { get; set; }

        public override string GetSettingKey()
        {
            return "Logging_Machine_ByType";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
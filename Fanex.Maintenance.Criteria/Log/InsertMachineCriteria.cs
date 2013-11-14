// --------------------------------------------------------------------------
// <copyright file="InsertMachineCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Insert criteria.
    /// </summary>
    public class InsertMachineCriteria : CriteriaBase
    {
        public string MachineName { get; set; }

        public bool IsProduction { get; set; }

        public short Timezone { get; set; }

        public override string GetSettingKey()
        {
            return "Logging_InsertMachine";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
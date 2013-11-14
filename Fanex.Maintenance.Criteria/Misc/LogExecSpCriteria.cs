// --------------------------------------------------------------------------
// <copyright file="LogExecSpCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.Misc
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Execute SP log criteria.
    /// </summary>
    public class LogExecSpCriteria : CriteriaBase
    {
        public int CurrentUserId { get; set; }

        public DateTime LogDate { get; set; }

        public string LoginIp { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}

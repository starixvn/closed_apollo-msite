// --------------------------------------------------------------------------
// <copyright file="Machine.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Machine model.
    /// </summary>
    public class Machine
    {
        public int MachineId { get; set; }

        [Required]
        public string MachineName { get; set; }

        public bool IsProduction { get; set; }

        [Range(-12, 12)]
        public short TimeZone { get; set; }
    }
}
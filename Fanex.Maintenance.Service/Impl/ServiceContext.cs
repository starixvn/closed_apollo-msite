// --------------------------------------------------------------------------
// <copyright file="ServiceContext.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using Fanex.Maintenance.Service.Interface;

    /// <summary>
    /// Service context.
    /// </summary>
    public class ServiceContext : IServiceContext
    {
        public int UserId { get; set; }

        public string Ip { get; set; }
    }
}

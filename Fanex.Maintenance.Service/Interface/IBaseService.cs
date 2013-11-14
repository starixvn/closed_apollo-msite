// --------------------------------------------------------------------------
// <copyright file="IBaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Interface
{
    using System;
    using Fanex.Maintenance.Criteria.Misc;

    /// <summary>
    /// Base service interface.
    /// </summary>
    public interface IBaseService
    {
        IServiceContext ServiceContext { get; set; }

        void HandleException(Exception ex);

        LogExecSpCriteria GetLogExecSpCriteria();
    }
}

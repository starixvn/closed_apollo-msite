// --------------------------------------------------------------------------
// <copyright file="LogController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Repository.Impl;
    using Fanex.Maintenance.Repository.Interface;
    using Fanex.Maintenance.Service.Interface;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public ICommandResult Login(ICriteria criteria)
        {
            if (criteria.IsValid())
            {
                return _userRepository.Login(criteria);
            }

            return null;
        }
    }
}
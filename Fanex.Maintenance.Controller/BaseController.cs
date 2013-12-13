// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.4">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System.Web.Mvc;
    using Fanex.Maintenance.Service.Impl;
    using Fanex.Maintenance.Service.Interface;
    using Fanex.NAP.Core.Configuration;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// Base controller.
    /// </summary>
    public class BaseController : Controller
    {
        protected readonly string Environment = NapSettings.Settings.Environment;

        protected readonly int SiteId = NapSettings.Settings.SiteId;

        protected readonly CurrentContext CurrentContext;

        protected readonly IServiceContext ServiceContext;

        protected readonly CurrentSession CurrentSession;

        private bool _issiteLogin = NapSettings.Settings.EnableDirectLogin;

        private ViewFlags _viewFlags;

        public BaseController()
        {
            CurrentContext = new CurrentContext();
            CurrentSession = new CurrentSession();

            ServiceContext = new ServiceContext
            {
                UserId = this.CurrentSession.UserId,
                Ip = this.CurrentContext.IP
            };

            ViewFlags = new ViewFlags
            {
                IsReCheckin = NapSettings.Settings.EnableReCheckIn
            };
        }

        /// <summary>
        /// Gets a value indicating whether the current site allows users login directly.
        /// </summary>
        /// <value>0 : users must login through NAP Hub.</value>
        public bool IsSiteLogin
        {
            get
            {
                return _issiteLogin;
            }
        }

        protected ViewFlags ViewFlags
        {
            get
            {
                return ViewBag.ViewFlags;
            }

            set
            {
                ViewBag.ViewFlags = _viewFlags = value;
            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }
    }
}

// --------------------------------------------------------------------------
// <copyright file="CommonErrorController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.25">Created</change>
// </history>

namespace Fanex.Maintenance.Controller
{
    using System.Web.Mvc;

    /// <summary>
    /// Common error controller.
    /// </summary>
    public class CommonErrorController : Controller
    {
        /// <summary>
        /// Error page.
        /// </summary>
        /// <param name="aspxErrorPath">Error path.</param>
        /// <returns>Action result.</returns>
        public ActionResult Error(string aspxErrorPath)
        {
            if (!string.IsNullOrEmpty(aspxErrorPath))
            {
                return RedirectToAction("Error");
            }

            return View();
        }

        /// <summary>
        /// Not found page.
        /// </summary>
        /// <param name="aspxErrorPath">Error path.</param>
        /// <param name="url">Request URL.</param>
        /// <returns>Action result.</returns>
        public ActionResult NotFound(string aspxErrorPath, string url)
        {
            if (!string.IsNullOrEmpty(aspxErrorPath))
            {
                ViewBag.RequestedUrl = aspxErrorPath;

                return RedirectToAction("NotFound", new { url = aspxErrorPath });
            }

            ViewBag.RequestedUrl = url;

            return View();
        }
    }
}

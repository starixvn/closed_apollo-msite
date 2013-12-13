// --------------------------------------------------------------------------
// <copyright file="AppSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.16">StyleCop</change>
// </history>

namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Web;
    using Fanex.Common;
    using Fanex.Logging;

    /// <summary>
    /// App setting.
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// Is Virtual.
        /// </summary>
        private static string _isVirtualDir = "false";

        private static int _minute = 10;

        private static long _getDataNotifyInterval = 60000;

        private static long _getServerResourcesInfoInterval = 2000;

        private static long _getCpuUtilizationInterval = 2000;

        private static long _getMemoryUtilizationInterval = 2000;

        private static bool _notifiServer = false;

        private static bool _notifiJob = false;

        static AppSetting()
        {
            _isVirtualDir = ConfigurationManager.AppSettings["isVirtualDirectory"];
            _notifiServer = Converter.ToBool(ConfigurationManager.AppSettings["isNotifiServer"].ToString());
            _notifiJob = Converter.ToBool(ConfigurationManager.AppSettings["isNotifiJob"].ToString());
            _minute = Converter.ToInt(ConfigurationManager.AppSettings["Minute"]);
            _getDataNotifyInterval = Converter.To<long>(ConfigurationManager.AppSettings["GetDataNotifyInterval"]) * 1000;
            _getServerResourcesInfoInterval = Converter.To<long>(ConfigurationManager.AppSettings["GetServerResourcesInfoInterval"]) * 1000;
            _getCpuUtilizationInterval = Converter.To<long>(ConfigurationManager.AppSettings["GetCpuUtilizationInterval"]) * 1000;
            _getMemoryUtilizationInterval = Converter.To<long>(ConfigurationManager.AppSettings["GetMemoryUtilizationInterval"]) * 1000;
        }

        public static long GetCpuUtilizationInterval
        {
            get 
            { 
                return AppSetting._getCpuUtilizationInterval; 
            }
        }

        public static long GetMemoryUtilizationInterval
        {
            get 
            { 
                return AppSetting._getMemoryUtilizationInterval; 
            }
        }

        public static bool IsVirtualDirectory
        {
            get
            {
                return string.Compare(_isVirtualDir, "true", true) == 0;
            }
        }

        public static int Minute
        {
            get
            {
                return _minute;
            }
        }

        public static long GetDataNotifyInterval
        {
            get
            {
                return _getDataNotifyInterval;
            }
        }

        public static long GetServerResourcesInfoInterval
        {
            get
            {
                return _getServerResourcesInfoInterval;
            }
        }

        public static CultureInfo Culture
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static bool NotifiJob
        {
            get 
            { 
                return _notifiJob; 
            }
        }

        public static bool NotifiServer
        {
            get 
            { 
                return _notifiServer; 
            }
        }
    }
}
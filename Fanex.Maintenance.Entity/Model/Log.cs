// --------------------------------------------------------------------------
// <copyright file="Log.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System;

    /// <summary>
    /// Log class.
    /// </summary>
    public class Log
    {
        public long LogId { get; set; }

        public int EventId { get; set; }

        public string Severity { get; set; }

        public string Title { get; set; }

        public DateTime TimeStamp { get; set; }

        public string AppDomainName { get; set; }

        public int Priority { get; set; }

        public int C_EventId { get; set; }

        public string ProcessId { get; set; }

        public string ProcessName { get; set; }

        public string Win32ThreadId { get; set; }

        public string ThreadName { get; set; }

        public string Message { get; set; }

        public string FormattedMessage { get; set; }

        public string MonitoringUser { get; set; }

        public bool IsMonitored { get; set; }

        public string FullMessage
        {
            get
            {
                return this.FormattedMessage.Replace("\n", "&lt;br/&gt;");
            }
        }

        public string LogDate
        {
            get
            {
                return this.TimeStamp.ToString("MMM dd,yyyy hh:mm:ss tt");
            }
        }

        public string GroupMessageIds { get; set; }

        public int NumMessage { get; set; }

        public LogCategory Category { get; set; }

        public Machine Machine { get; set; }
    }
}

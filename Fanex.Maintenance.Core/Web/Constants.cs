// --------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.Web
{
    using System.Collections.Generic;

    /// <summary>
    /// Status job.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Enable status.
        /// </summary>
        Enabled = 1,

        /// <summary>
        /// Disable status.
        /// </summary>
        Disabled = 0
    }

    /// <summary>
    /// Current execution status of job.
    /// </summary>
    public enum ExecutionStatus
    {
        /// <summary>
        /// Obsolete status.
        /// </summary>
        Obsolete = 0,

        /// <summary>
        /// Executing status.
        /// </summary>
        Executing = 1,

        /// <summary>
        /// WaitingFoThread status.
        /// </summary>
        WaitingFoThread = 2,

        /// <summary>
        /// BetweenRtries status.
        /// </summary>
        BetweenRtries = 3,

        /// <summary>
        /// Idle status.
        /// </summary>
        Idle = 4,

        /// <summary>
        /// Suspended status.
        /// </summary>
        Suspended = 5,

        /// <summary>
        /// PerformingCompletionActions status.
        /// </summary>
        PerformingCompletionActions = 6
    }

    /// <summary>
    /// Last run date and next run date.
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// Last date type.
        /// </summary>
        Last = 0,

        /// <summary>
        /// Next date type.
        /// </summary>
        Next = 1
    }

    /// <summary>
    /// Frequent Time to of Schedule.
    /// </summary>
    public enum FrequentTime : int
    {
        /// <summary>
        /// One time only.
        /// </summary>
        OneTimeOnly = 1,

        /// <summary>
        /// Daily frequency.
        /// </summary>
        Daily = 4,

        /// <summary>
        /// Weekly frequency.
        /// </summary>
        Weekly = 8,

        /// <summary>
        /// Monthly frequency.
        /// </summary>
        Monthly = 16,

        /// <summary>
        /// Monthly relative frequency.
        /// </summary>
        MonlyRelative = 32,

        /// <summary>
        /// SQL Agent start frequency.
        /// </summary>
        SqlAgentStart = 64,

        /// <summary>
        /// Computer idle frequency.
        /// </summary>
        ComputerIdle = 128
    }

    /// <summary>
    /// Frequent Sub Time of Schedule.
    /// </summary>
    public enum FrequentSubTime : int
    {
        /// <summary>
        /// Specific time.
        /// </summary>
        SpecificTime = 1,

        /// <summary>
        /// Seconds subtime.
        /// </summary>
        Seconds = 2,

        /// <summary>
        /// Minutes subtime.
        /// </summary>
        Minutes = 4,

        /// <summary>
        /// Hours subtime.
        /// </summary>
        Hours = 8
    }

    /// <summary>
    /// Search schedule by Date Type.
    /// </summary>
    public enum SearchDate : int
    {
        /// <summary>
        /// All dates.
        /// </summary>
        All = 0,

        /// <summary>
        /// Start date.
        /// </summary>
        StartDate = 1,

        /// <summary>
        /// End date value.
        /// </summary>
        EndDate = 2
    }

    /// <summary>
    /// Step run status.
    /// </summary>
    public enum StepRunStatus
    {
        /// <summary>
        /// Step run success.
        /// </summary>
        Success = 1,

        /// <summary>
        /// Step run fail.
        /// </summary>
        Fail = 0
    }

    /// <summary>
    /// Server Log Type.
    /// </summary>
    public enum LogType : int
    {
        /// <summary>
        /// SQL Server log.
        /// </summary>
        SqlServerLog = 1,

        /// <summary>
        /// SQL Agent log.
        /// </summary>
        SqlAgentLog = 2
    }

    /// <summary>
    /// Error Type.
    /// </summary>
    public enum ErrorType : int
    {
        /// <summary>
        /// Error type.
        /// </summary>
        Error = 1,

        /// <summary>
        /// Warning type.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Information type.
        /// </summary>
        Infomation = 3
    }

    /// <summary>
    /// Server Type for get Connection String.
    /// </summary>
    public enum ServerType : int
    {
        /// <summary>
        /// Normal server type.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Resource server type.
        /// </summary>
        Resource = 1
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Job Constant.
        /// </summary>
        public const string JOB = "JOB";

        /// <summary>
        /// Steps Constant.
        /// </summary>
        public const string STEPS = "STEPS";

        /// <summary>
        /// Schedules Constant.
        /// </summary>
        public const string SCHEDULES = "SCHEDULES";

        /// <summary>
        /// Targets Constant.
        /// </summary>
        public const string TARGETS = "TARGETS";

        /// <summary>
        /// Full Constant.
        /// </summary>
        public const string FULL = "FULL";

        public static readonly List<int> NotifyWhenJobStatus = new List<int>
        {
            0,
            3
        };

        /// <summary>
        /// Frequency Interval Values.
        /// </summary>
        public static readonly string[] FreqIntervalValues = new string[]
        {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Day",
            "Weekday",
            "Weekend day"
        };
    }
}

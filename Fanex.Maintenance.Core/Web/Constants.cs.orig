// -----------------------------------------------------------------------
// <copyright file="Contains.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Constants
    {
        public const string JOB = "JOB";

        public const string STEPS = "STEPS";

        public const string SCEDULES = "SCEDULES";

        public const string TARGETS = "TARGETS";

        public const string FULL = "FULL";      
    }

        public static string[] FreqIntervalValues = new string[]
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

    /// <summary>
    /// Status job
    /// </summary>
    public enum Status
    {
        Enabled = 1,

        Disabled = 0
    }

    /// <summary>
    /// Current execution status of job
    /// </summary>
    public enum ExecutionStatus
    {
        Obsolete = 0,

        Executing = 1,

        WaitingFoThread = 2,

        BetweenRtries = 3,

        Idle = 4,

        Suspended = 5,

        PerformingCompletionActions = 6
    }

    /// <summary>
    /// Last run date and next run date
    /// </summary>
    public enum DateType
    {
        Last = 0,
        Next = 1
    }

    public enum FrequentTime : int
    {
        OneTimeOnly = 1,
        Daily = 4,
        Weekly = 8,
        Monthly = 16,
        MonlyRelative = 32,
        SqlAgentStart = 64,
        ComputerIdle = 128
    }

    public enum FrequentSubTime : int
    {
        SpecificTime = 1,
        Seconds = 2,
        Minutes = 4,
        Hours = 8
    }

    public enum SearchDate : int
    {
        All = 0,
        StartDate = 1,
        EndDate = 2
    }

    public enum StepRunStatus
    {
        Success = 1,
        Fail = 0
    }
}

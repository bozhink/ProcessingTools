// <copyright file="ITaskSchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Tasks
{
    using System;
    using ProcessingTools.Enumerations.Tasks;

    /// <summary>
    /// Task schedule.
    /// </summary>
    public interface ITaskSchedule
    {
        /// <summary>
        /// Gets the task schedule identifier.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the task identifier.
        /// </summary>
        string TaskId { get; }

        /// <summary>
        /// Gets type of the task schedule.
        /// </summary>
        ScheduleType Type { get; }

        /// <summary>
        /// Gets or sets configuration data of the task schedule.
        /// </summary>
        string Data { get; set; }

        /// <summary>
        /// Gets optional parameters of the task schedule.
        /// </summary>
        string Parameters { get; }

        /// <summary>
        /// Gets a value indicating whether the task schedule is active.
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Gets next run time of the task schedule.
        /// </summary>
        DateTime NextRunTime { get; }

        /// <summary>
        /// Gets last run time of the task schedule.
        /// </summary>
        DateTime? LastRunTime { get; }
    }
}

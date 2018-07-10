// <copyright file="IDailySchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Schedules
{
    /// <summary>
    /// Schedule with daily execution.
    /// </summary>
    public interface IDailySchedule : ISchedule
    {
        /// <summary>
        /// Gets or sets the period of repetition in days.
        /// </summary>
        int RepeatPeriodInDays { get; set; }
    }
}

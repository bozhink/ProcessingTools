// <copyright file="ISchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Schedules
{
    using System;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

    /// <summary>
    /// Schedule.
    /// </summary>
    public interface ISchedule
    {
        /// <summary>
        /// Gets the associated task schedule.
        /// </summary>
        ITaskSchedule TaskSchedule { get; }

        /// <summary>
        /// Gets the referent start time.
        /// </summary>
        DateTime Start { get; }

        /// <summary>
        /// Gets daily repetition start time.
        /// </summary>
        DateTime? FromTime { get; }

        /// <summary>
        /// Gets daily repetition end time.
        /// </summary>
        DateTime? ToTime { get; }

        /// <summary>
        /// Gets daily repetition interval in minutes.
        /// </summary>
        int? MinutesToRepeat { get; }

        /// <summary>
        /// Gets or sets a value indicating whether schedule has daily repetition.
        /// </summary>
        bool RepeatInDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to stop daily repetition on success.
        /// </summary>
        bool StopDailyRepetitionOnSuccess { get; set; }

        /// <summary>
        /// Gets next run time.
        /// </summary>
        /// <param name="taskSuccessFlag">Task success flag.</param>
        /// <returns>Calculated next run time.</returns>
        DateTime GetNextRunTime(bool taskSuccessFlag);

        /// <summary>
        /// Reads action configuration from the task action data.
        /// </summary>
        void ReadFromData();

        /// <summary>
        /// Writes action configuration to the task action data.
        /// </summary>
        void WriteToData();
    }
}

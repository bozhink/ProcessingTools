// <copyright file="IWeeklySchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Schedules
{
    /// <summary>
    /// Schedule with weekly execution.
    /// </summary>
    public interface IWeeklySchedule
    {
        /// <summary>
        /// Gets or sets a value indicating whether Monday is enabled.
        /// </summary>
        bool Monday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Tuesday is enabled.
        /// </summary>
        bool Tuesday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Wednesday is enabled.
        /// </summary>
        bool Wednesday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Thursday is enabled.
        /// </summary>
        bool Thursday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Friday is enabled.
        /// </summary>
        bool Friday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Saturday is enabled.
        /// </summary>
        bool Saturday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Sunday is enabled.
        /// </summary>
        bool Sunday { get; set; }

        /// <summary>
        /// Gets or sets the period of repetition in weeks.
        /// </summary>
        int RepeatPeriodInWeeks { get; set; }
    }
}

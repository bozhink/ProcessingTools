// <copyright file="IMonthlySchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Schedules
{
    using System.Collections.Generic;

    /// <summary>
    /// Schedule with monthly execution.
    /// </summary>
    public interface IMonthlySchedule : ISchedule
    {
        /// <summary>
        /// Gets or sets a value indicating whether January is enabled.
        /// </summary>
        bool January { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether February is enabled.
        /// </summary>
        bool February { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether March is enabled.
        /// </summary>
        bool March { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether April is enabled.
        /// </summary>
        bool April { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether May is enabled.
        /// </summary>
        bool May { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether June is enabled.
        /// </summary>
        bool June { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether July is enabled.
        /// </summary>
        bool July { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether August is enabled.
        /// </summary>
        bool August { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether September is enabled.
        /// </summary>
        bool September { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether October is enabled.
        /// </summary>
        bool October { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether November is enabled.
        /// </summary>
        bool November { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether December is enabled.
        /// </summary>
        bool December { get; set; }

        /// <summary>
        /// Gets or sets the collection of selected days.
        /// </summary>
        ICollection<int> Days { get; set; }
    }
}

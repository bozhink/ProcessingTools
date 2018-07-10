// <copyright file="ScheduleType.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Enumerations.Tasks
{
    /// <summary>
    /// Schedule type.
    /// </summary>
    public enum ScheduleType
    {
        /// <summary>
        /// Schedule with single execution.
        /// </summary>
        OneTime,

        /// <summary>
        /// Schedule with daily execution.
        /// </summary>
        Daily,

        /// <summary>
        /// Schedule with weekly execution.
        /// </summary>
        Weekly,

        /// <summary>
        /// Schedule with monthly execution.
        /// </summary>
        Monthly
    }
}

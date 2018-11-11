// <copyright file="ScheduleIdentifierAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Attributes.Tasks
{
    using System;
    using ProcessingTools.Common.Enumerations.Tasks;

    /// <summary>
    /// Action identifier.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ScheduleIdentifierAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleIdentifierAttribute"/> class.
        /// </summary>
        /// <param name="scheduleType">Schedule type.</param>
        public ScheduleIdentifierAttribute(ScheduleType scheduleType)
        {
            this.ScheduleType = scheduleType;
        }

        /// <summary>
        /// Gets the action type.
        /// </summary>
        public ScheduleType ScheduleType { get; }
    }
}

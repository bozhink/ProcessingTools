// <copyright file="ITaskAction.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Common.Enumerations.Tasks;

namespace ProcessingTools.Contracts.Tasks.Models.Tasks
{
    /// <summary>
    /// Task action.
    /// </summary>
    public interface ITaskAction
    {
        /// <summary>
        /// Gets the task action identifier.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the task identifier.
        /// </summary>
        string TaskId { get; }

        /// <summary>
        /// Gets order parameter for the task action.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets type of the task action.
        /// </summary>
        ActionType Type { get; }

        /// <summary>
        /// Gets or sets configuration data of the task action.
        /// </summary>
        string Data { get; set; }

        /// <summary>
        /// Gets a value indicating whether the task action is active.
        /// </summary>
        bool Active { get; }
    }
}

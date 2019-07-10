// <copyright file="IAction.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Tasks.Models.Tasks;

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    /// <summary>
    /// Action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Gets the associated task action.
        /// </summary>
        ITaskAction TaskAction { get; }

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

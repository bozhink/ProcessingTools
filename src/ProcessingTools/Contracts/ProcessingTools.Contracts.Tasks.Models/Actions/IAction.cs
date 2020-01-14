// <copyright file="IAction.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Tasks.Models.Actions
{
    using ProcessingTools.Contracts.Tasks.Models.Tasks;

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

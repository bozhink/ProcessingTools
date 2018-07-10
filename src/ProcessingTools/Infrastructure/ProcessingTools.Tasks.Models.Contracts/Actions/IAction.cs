// <copyright file="IAction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Actions
{
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

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

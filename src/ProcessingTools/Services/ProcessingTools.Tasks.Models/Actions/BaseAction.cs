// <copyright file="BaseAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Actions
{
    using Newtonsoft.Json;
    using System;
    using ProcessingTools.Contracts.Tasks.Models.Actions;
    using ProcessingTools.Contracts.Tasks.Models.Tasks;

    /// <summary>
    /// Base action.
    /// </summary>
    public abstract class BaseAction : IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAction"/> class.
        /// </summary>
        /// <param name="taskAction">Task action to be associated to this action.</param>
        protected BaseAction(ITaskAction taskAction)
        {
            this.TaskAction = taskAction ?? throw new ArgumentNullException(nameof(taskAction));
        }

        /// <inheritdoc/>
        [JsonIgnore]
        public ITaskAction TaskAction { get; }

        /// <inheritdoc/>
        public abstract void ReadFromData();

        /// <inheritdoc/>
        public abstract void WriteToData();
    }
}

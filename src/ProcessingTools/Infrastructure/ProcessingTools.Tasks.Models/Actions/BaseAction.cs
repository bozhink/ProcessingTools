// <copyright file="BaseAction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Actions
{
    using System;
    using Newtonsoft.Json;
    using ProcessingTools.Tasks.Models.Contracts.Actions;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

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

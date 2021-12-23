// <copyright file="CopyFilesAction.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Actions
{
    using Newtonsoft.Json;
    using ProcessingTools.Common.Attributes.Tasks;
    using ProcessingTools.Common.Enumerations.Tasks;
    using ProcessingTools.Contracts.Tasks.Models.Actions;
    using ProcessingTools.Contracts.Tasks.Models.Tasks;

    /// <summary>
    /// Copy files action.
    /// </summary>
    [ActionIdentifier(ActionType.CopyFiles)]
    public class CopyFilesAction : BaseAction, ICopyFilesAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFilesAction"/> class.
        /// </summary>
        /// <param name="taskAction">Task action.</param>
        public CopyFilesAction(ITaskAction taskAction)
            : base(taskAction)
        {
        }

        /// <inheritdoc/>
        public string SourceDirectory { get; set; }

        /// <inheritdoc/>
        public string FilterWildcard { get; set; }

        /// <inheritdoc/>
        public string DestinationDirectory { get; set; }

        /// <inheritdoc/>
        public bool OverwriteFile { get; set; }

        /// <inheritdoc/>
        public override void ReadFromData()
        {
            var data = JsonConvert.DeserializeObject<CopyFilesAction>(this.TaskAction.Data);
            this.DestinationDirectory = data.DestinationDirectory;
            this.SourceDirectory = data.SourceDirectory;
            this.FilterWildcard = data.FilterWildcard;
            this.OverwriteFile = data.OverwriteFile;
        }

        /// <inheritdoc/>
        public override void WriteToData()
        {
            this.TaskAction.Data = JsonConvert.SerializeObject(this);
        }
    }
}

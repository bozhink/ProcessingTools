// <copyright file="TagGeoEpithetsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Geo;

    /// <summary>
    /// Tag geo epithets command.
    /// </summary>
    [System.ComponentModel.Description("Tag geo epithets.")]
    public class TagGeoEpithetsCommand : DocumentTaggerCommand<IGeoEpithetsTagger>, ITagGeoEpithetsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagGeoEpithetsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IGeoEpithetsTagger"/>.</param>
        public TagGeoEpithetsCommand(IGeoEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}

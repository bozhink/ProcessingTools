// <copyright file="TagAltitudesCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Geo;

    /// <summary>
    /// Tag altitudes command.
    /// </summary>
    [System.ComponentModel.Description("Tag altitudes.")]
    public class TagAltitudesCommand : DocumentTaggerCommand<IAltitudesTagger>, ITagAltitudesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagAltitudesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IAltitudesTagger"/>.</param>
        public TagAltitudesCommand(IAltitudesTagger tagger)
            : base(tagger)
        {
        }
    }
}

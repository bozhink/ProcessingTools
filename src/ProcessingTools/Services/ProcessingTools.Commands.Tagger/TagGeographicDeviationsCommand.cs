// <copyright file="TagGeographicDeviationsCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Geo;

    /// <summary>
    /// Tag geographic deviations command.
    /// </summary>
    [System.ComponentModel.Description("Tag geographic deviations.")]
    public class TagGeographicDeviationsCommand : DocumentTaggerCommand<IGeographicDeviationsTagger>, ITagGeographicDeviationsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagGeographicDeviationsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IGeographicDeviationsTagger"/>.</param>
        public TagGeographicDeviationsCommand(IGeographicDeviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}

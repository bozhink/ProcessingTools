// <copyright file="TagCoordinatesCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Geo.Coordinates;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Tag coordinates command.
    /// </summary>
    [System.ComponentModel.Description("Tag coordinates.")]
    public class TagCoordinatesCommand : DocumentTaggerCommand<ICoordinatesTagger>, ITagCoordinatesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagCoordinatesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ICoordinatesTagger"/>.</param>
        public TagCoordinatesCommand(ICoordinatesTagger tagger)
            : base(tagger)
        {
        }
    }
}

﻿// <copyright file="ParseCoordinatesCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Services.Contracts.Geo.Coordinates;

    /// <summary>
    /// Parse coordinates command.
    /// </summary>
    [System.ComponentModel.Description("Parse coordinates.")]
    public class ParseCoordinatesCommand : XmlContextParserCommand<ICoordinatesParser>, IParseCoordinatesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseCoordinatesCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ICoordinatesParser"/>.</param>
        public ParseCoordinatesCommand(ICoordinatesParser parser)
            : base(parser)
        {
        }
    }
}

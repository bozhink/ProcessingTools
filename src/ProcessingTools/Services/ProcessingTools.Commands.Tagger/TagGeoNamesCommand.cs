﻿// <copyright file="TagGeoNamesCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Geo;

    /// <summary>
    /// Tag geo names command.
    /// </summary>
    [System.ComponentModel.Description("Tag geo names.")]
    public class TagGeoNamesCommand : DocumentTaggerCommand<IGeoNamesTagger>, ITagGeoNamesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagGeoNamesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IGeoNamesTagger"/>.</param>
        public TagGeoNamesCommand(IGeoNamesTagger tagger)
            : base(tagger)
        {
        }
    }
}

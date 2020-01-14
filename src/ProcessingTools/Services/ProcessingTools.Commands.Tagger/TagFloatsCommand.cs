﻿// <copyright file="TagFloatsCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Floats;

    /// <summary>
    /// Tag floats command.
    /// </summary>
    [System.ComponentModel.Description("Tag floats.")]
    public class TagFloatsCommand : XmlContextTaggerCommand<IFloatsTagger>, ITagFloatsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagFloatsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IFloatsTagger"/>.</param>
        public TagFloatsCommand(IFloatsTagger tagger)
            : base(tagger)
        {
        }
    }
}

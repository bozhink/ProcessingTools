﻿// <copyright file="TagSpecimenCountCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio;

    /// <summary>
    /// Tag specimen count command.
    /// </summary>
    [System.ComponentModel.Description("Tag specimen count.")]
    public class TagSpecimenCountCommand : DocumentTaggerCommand<ISpecimenCountTagger>, ITagSpecimenCountCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagSpecimenCountCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ISpecimenCountTagger"/>.</param>
        public TagSpecimenCountCommand(ISpecimenCountTagger tagger)
            : base(tagger)
        {
        }
    }
}

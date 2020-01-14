﻿// <copyright file="TagCollectionCodesCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Codes;

    /// <summary>
    /// Tag collection codes command.
    /// </summary>
    [System.ComponentModel.Description("Tag collection codes.")]
    public class TagCollectionCodesCommand : DocumentTaggerCommand<ICollectionCodesTagger>, ITagCollectionCodesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagCollectionCodesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ICollectionCodesTagger"/>.</param>
        public TagCollectionCodesCommand(ICollectionCodesTagger tagger)
            : base(tagger)
        {
        }
    }
}

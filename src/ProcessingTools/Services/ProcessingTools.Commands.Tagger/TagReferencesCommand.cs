﻿// <copyright file="TagReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.References;

    /// <summary>
    /// Tag references command.
    /// </summary>
    [System.ComponentModel.Description("Tag references.")]
    public class TagReferencesCommand : ITagReferencesCommand
    {
        private readonly IReferencesTagger tagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagReferencesCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IReferencesTagger"/>.</param>
        public TagReferencesCommand(IReferencesTagger tagger)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
        }

        /// <inheritdoc/>
        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            // TODO
            return this.tagger.TagAsync(document.XmlDocument.DocumentElement, null);
        }
    }
}

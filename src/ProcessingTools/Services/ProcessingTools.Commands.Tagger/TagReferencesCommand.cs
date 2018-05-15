// <copyright file="TagReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.References;

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

            return this.tagger.TagAsync(document.XmlDocument.DocumentElement);
        }
    }
}

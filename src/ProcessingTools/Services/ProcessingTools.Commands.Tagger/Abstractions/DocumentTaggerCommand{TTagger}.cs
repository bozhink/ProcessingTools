// <copyright file="DocumentTaggerCommand{TTagger}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// Document tagger command.
    /// </summary>
    /// <typeparam name="TTagger">Type of tagger.</typeparam>
    public class DocumentTaggerCommand<TTagger> : ITaggerCommand
        where TTagger : class, IDocumentTagger
    {
        private readonly TTagger tagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTaggerCommand{TTagger}"/> class.
        /// </summary>
        /// <param name="tagger">Tagger to be invoked.</param>
        public DocumentTaggerCommand(TTagger tagger)
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

            return this.tagger.TagAsync(document);
        }
    }
}

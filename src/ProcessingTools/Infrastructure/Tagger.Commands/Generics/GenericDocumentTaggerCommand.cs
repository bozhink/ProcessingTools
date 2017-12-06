﻿namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors;

    public class GenericDocumentTaggerCommand<TTagger> : ITaggerCommand
        where TTagger : class, IDocumentTagger
    {
        private readonly TTagger tagger;

        public GenericDocumentTaggerCommand(TTagger tagger)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
        }

        public async Task<object> Run(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return await this.tagger.TagAsync(document).ConfigureAwait(false);
        }
    }
}

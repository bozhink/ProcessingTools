namespace ProcessingTools.Tagger.Core.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;

    public class GenericDocumentTaggerCommand<TTagger> : ITaggerCommand
        where TTagger : IDocumentTagger
    {
        private readonly TTagger tagger;

        public GenericDocumentTaggerCommand(TTagger tagger)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return await this.tagger.Tag(document);
        }
    }
}

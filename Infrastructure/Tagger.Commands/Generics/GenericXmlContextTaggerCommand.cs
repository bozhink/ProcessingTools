namespace ProcessingTools.Tagger.Commands.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;

    public class GenericXmlContextTaggerCommand<TTagger> : ITaggerCommand
        where TTagger : IXmlContextTagger
    {
        private readonly TTagger tagger;

        public GenericXmlContextTaggerCommand(TTagger tagger)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
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

            return await this.tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}

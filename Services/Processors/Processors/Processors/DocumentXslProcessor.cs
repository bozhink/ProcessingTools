namespace ProcessingTools.Processors
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class DocumentXslProcessor : IDocumentXslProcessor
    {
        private readonly IModifiableXslTransformer transformer;

        public DocumentXslProcessor(IModifiableXslTransformer transformer)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.transformer = transformer;
        }

        public string XslFilePath
        {
            get
            {
                return this.transformer.XslFilePath;
            }

            set
            {
                this.transformer.XslFilePath = value;
            }
        }

        public async Task Process(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformer.Transform(context.Xml);
            context.Xml = content;
        }
    }
}

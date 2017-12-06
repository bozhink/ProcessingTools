namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Xml.Contracts.Factories;

    public class DocumentXslProcessor : IDocumentXslProcessor
    {
        private readonly IXslTransformerFactory factory;

        public DocumentXslProcessor(IXslTransformerFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public string XslFileName { get; set; }

        public async Task ProcessAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.factory
                .CreateTransformer(xslFileName: this.XslFileName)
                .TransformAsync(context.Xml);

            context.Xml = content;
        }
    }
}

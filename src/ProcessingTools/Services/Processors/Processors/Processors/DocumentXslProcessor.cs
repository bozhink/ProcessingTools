namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors;
    using ProcessingTools.Xml.Contracts.Factories;

    public class DocumentXslProcessor : IDocumentXslProcessor
    {
        private readonly IXslTransformerFactory factory;

        public DocumentXslProcessor(IXslTransformerFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public string XslFileFullName { get; set; }

        public async Task Process(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.factory
                .CreateTransformer(xslFileName: this.XslFileFullName)
                .Transform(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;
        }
    }
}

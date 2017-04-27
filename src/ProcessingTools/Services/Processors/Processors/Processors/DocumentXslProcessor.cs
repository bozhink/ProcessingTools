namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Processors;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Contracts.Factories;

    public class DocumentXslProcessor : IDocumentXslProcessor
    {
        private readonly IXslTransformerFactory factory;

        public DocumentXslProcessor(IXslTransformerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.factory = factory;
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
                .Transform(context.Xml);

            context.Xml = content;
        }
    }
}

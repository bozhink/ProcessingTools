namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Xml.Contracts.Factories;

    public class DocumentXQueryProcessor : IDocumentXQueryProcessor
    {
        private readonly IXQueryTransformerFactory factory;

        public DocumentXQueryProcessor(IXQueryTransformerFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public string XQueryFileName { get; set; }

        public async Task ProcessAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.factory
                .CreateTransformer(xqueryFileName: this.XQueryFileName)
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;
        }
    }
}

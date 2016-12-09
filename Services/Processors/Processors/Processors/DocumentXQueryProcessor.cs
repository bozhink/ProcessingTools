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
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.factory = factory;
        }

        public string XQueryFileFullName { get; set; }

        public async Task Process(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.factory
                .CreateTransformer(xqueryFileName: this.XQueryFileFullName)
                .Transform(context.Xml);

            context.Xml = content;
        }
    }
}

namespace ProcessingTools.Processors.Processors.Bio.ZooBank
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Factories;
    using ProcessingTools.Processors.Contracts.Processors.Bio.ZooBank;

    public class ZooBankRegistrationXmlGenerator : IZooBankRegistrationXmlGenerator
    {
        private readonly IRegistrationTransformersFactory transformerFactory;

        public ZooBankRegistrationXmlGenerator(IRegistrationTransformersFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        public async Task<object> Generate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var content = await this.transformerFactory
                .GetZooBankRegistrationTransformer()
                .Transform(document.Xml);

            document.Xml = content;

            return true;
        }
    }
}

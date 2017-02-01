namespace ProcessingTools.Processors.Processors.Bio.ZooBank
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Factories;
    using Contracts.Processors.Bio.ZooBank;
    using ProcessingTools.Contracts;

    public class ZooBankRegistrationXmlGenerator : IZooBankRegistrationXmlGenerator
    {
        private readonly IRegistrationTransformersFactory transformerFactory;

        public ZooBankRegistrationXmlGenerator(IRegistrationTransformersFactory transformerFactory)
        {
            if (transformerFactory == null)
            {
                throw new ArgumentNullException(nameof(transformerFactory));
            }

            this.transformerFactory = transformerFactory;
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

namespace ProcessingTools.Layout.Processors.Factories
{
    using System;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Layout.Processors.Contracts.Factories;

    public class InitialFormatTransformerFactory : IInitialFormatTransformerFactory
    {
        private readonly IFormatTransformersFactory transformersFactory;

        public InitialFormatTransformerFactory(IFormatTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public IXmlTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.transformersFactory.GetNlmInitialFormatTransformer();

                default:
                    return this.transformersFactory.GetSystemInitialFormatTransformer();
            }
        }
    }
}

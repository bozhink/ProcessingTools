namespace ProcessingTools.Layout.Processors.Factories
{
    using System;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Layout.Processors.Contracts.Factories;
    using ProcessingTools.Processors.Contracts;

    public class NormalizationTransformerFactory : INormalizationTransformerFactory
    {
        private readonly IFormatTransformersFactory transformersFactory;

        public NormalizationTransformerFactory(IFormatTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public IXmlTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.transformersFactory.GetFormatToNlmTransformer();

                default:
                    return this.transformersFactory.GetFormatToSystemTransformer();
            }
        }
    }
}

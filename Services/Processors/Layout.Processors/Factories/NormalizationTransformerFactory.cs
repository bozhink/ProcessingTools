namespace ProcessingTools.Layout.Processors.Factories
{
    using System;
    using Contracts.Factories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class NormalizationTransformerFactory : INormalizationTransformerFactory
    {
        private readonly IFormatTransformersFactory transformersFactory;

        public NormalizationTransformerFactory(IFormatTransformersFactory transformersFactory)
        {
            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.transformersFactory = transformersFactory;
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

namespace ProcessingTools.Layout.Processors.Factories
{
    using System;

    using Contracts;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class InitialFormatTransformerFactory : IInitialFormatTransformerFactory
    {
        private readonly IXslTransformer<INlmInitialFormatXslTransformProvider> nlmInitialFormatXslTransformer;
        private readonly IXslTransformer<ISystemInitialFormatXslTransformProvider> systemInitialFormatXslTransformer;

        public InitialFormatTransformerFactory(
            IXslTransformer<INlmInitialFormatXslTransformProvider> nlmInitialFormatXslTransformer,
            IXslTransformer<ISystemInitialFormatXslTransformProvider> systemInitialFormatXslTransformer)
        {
            if (nlmInitialFormatXslTransformer == null)
            {
                throw new ArgumentNullException(nameof(nlmInitialFormatXslTransformer));
            }

            if (systemInitialFormatXslTransformer == null)
            {
                throw new ArgumentNullException(nameof(systemInitialFormatXslTransformer));
            }

            this.nlmInitialFormatXslTransformer = nlmInitialFormatXslTransformer;
            this.systemInitialFormatXslTransformer = systemInitialFormatXslTransformer;
        }

        public IXslTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.nlmInitialFormatXslTransformer;

                default:
                    return this.systemInitialFormatXslTransformer;
            }
        }
    }
}

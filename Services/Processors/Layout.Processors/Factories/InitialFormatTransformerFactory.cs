namespace ProcessingTools.Layout.Processors.Factories
{
    using System;

    using Contracts.Factories;
    using Contracts.Transformers;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class InitialFormatTransformerFactory : IInitialFormatTransformerFactory
    {
        private readonly INlmInitialFormatTransformer nlmInitialFormatTransformer;
        private readonly ISystemInitialFormatTransformer systemInitialFormatTransformer;

        public InitialFormatTransformerFactory(
            INlmInitialFormatTransformer nlmInitialFormatTransformer,
            ISystemInitialFormatTransformer systemInitialFormatTransformer)
        {
            if (nlmInitialFormatTransformer == null)
            {
                throw new ArgumentNullException(nameof(nlmInitialFormatTransformer));
            }

            if (systemInitialFormatTransformer == null)
            {
                throw new ArgumentNullException(nameof(systemInitialFormatTransformer));
            }

            this.nlmInitialFormatTransformer = nlmInitialFormatTransformer;
            this.systemInitialFormatTransformer = systemInitialFormatTransformer;
        }

        public IXmlTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.nlmInitialFormatTransformer;

                default:
                    return this.systemInitialFormatTransformer;
            }
        }
    }
}

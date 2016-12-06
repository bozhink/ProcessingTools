namespace ProcessingTools.Layout.Processors.Factories
{
    using System;
    using Contracts.Factories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class InitialFormatTransformerFactory : IInitialFormatTransformerFactory
    {
        private readonly IFormatTransformersFactory transformersFactory;

        public InitialFormatTransformerFactory(IFormatTransformersFactory transformersFactory)
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
                    return this.transformersFactory.GetNlmInitialFormatTransformer();

                default:
                    return this.transformersFactory.GetSystemInitialFormatTransformer();
            }
        }
    }
}

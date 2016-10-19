namespace ProcessingTools.Layout.Processors.Factories
{
    using System;

    using Contracts.Factories;
    using Contracts.Transformers;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Xml.Contracts.Transformers;

    public class NormalizationTransformerFactory : INormalizationTransformerFactory
    {
        private readonly IFormatToSystemTransformer formatToSystemTransformer;
        private readonly IFormatToNlmTransformer formatToNlmTransformer;

        public NormalizationTransformerFactory(
            IFormatToSystemTransformer formatToSystemTransformer,
            IFormatToNlmTransformer formatToNlmTransformer)
        {
            if (formatToSystemTransformer == null)
            {
                throw new ArgumentNullException(nameof(formatToSystemTransformer));
            }

            if (formatToNlmTransformer == null)
            {
                throw new ArgumentNullException(nameof(formatToNlmTransformer));
            }

            this.formatToSystemTransformer = formatToSystemTransformer;
            this.formatToNlmTransformer = formatToNlmTransformer;
        }

        public IXmlTransformer Create(SchemaType schemaType)
        {
            switch (schemaType)
            {
                case SchemaType.Nlm:
                    return this.formatToNlmTransformer;

                default:
                    return this.formatToSystemTransformer;
            }
        }
    }
}

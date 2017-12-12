namespace ProcessingTools.Layout.Processors.Abstractions.Normalizers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Xml;

    public class XmlContextNormalizer : IXmlContextNormalizer
    {
        private readonly IXmlTransformer transformer;

        public XmlContextNormalizer(IXmlTransformer transformer)
        {
            this.transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
        }

        public Task<string> NormalizeAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.transformer.TransformAsync(context);
        }
    }
}

namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    using ProcessingTools.Layout.Processors.Abstractions.Normalizers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Layout.Processors.Contracts.Transformers;

    public class SystemXmlNormalizer : AbstractXmlNormalizer, ISystemXmlNormalizer
    {
        public SystemXmlNormalizer(IFormatToSystemTransformer transformer)
            : base(transformer)
        {
        }
    }
}

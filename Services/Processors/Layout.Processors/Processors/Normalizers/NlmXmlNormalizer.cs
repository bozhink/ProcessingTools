namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    using ProcessingTools.Layout.Processors.Abstractions.Normalizers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Layout.Processors.Contracts.Transformers;

    public class NlmXmlNormalizer : AbstractXmlNormalizer, INlmXmlNormalizer
    {
        public NlmXmlNormalizer(IFormatToNlmTransformer transformer)
            : base(transformer)
        {
        }
    }
}

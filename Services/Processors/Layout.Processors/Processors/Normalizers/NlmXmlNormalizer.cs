namespace ProcessingTools.Layout.Processors.Normalizers
{
    using Abstracts.Normalizers;
    using Contracts.Normalizers;
    using Contracts.Transformers;

    public class NlmXmlNormalizer : AbstractXmlNormalizer, INlmXmlNormalizer
    {
        public NlmXmlNormalizer(IFormatToNlmTransformer transformer)
            : base(transformer)
        {
        }
    }
}

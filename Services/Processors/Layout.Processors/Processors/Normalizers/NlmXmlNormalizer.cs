using ProcessingTools.Layout.Processors.Abstractions.Normalizers;
using ProcessingTools.Layout.Processors.Contracts.Normalizers;
using ProcessingTools.Layout.Processors.Contracts.Transformers;

namespace ProcessingTools.Layout.Processors.Processors.Normalizers
{
    public class NlmXmlNormalizer : AbstractXmlNormalizer, INlmXmlNormalizer
    {
        public NlmXmlNormalizer(IFormatToNlmTransformer transformer)
            : base(transformer)
        {
        }
    }
}

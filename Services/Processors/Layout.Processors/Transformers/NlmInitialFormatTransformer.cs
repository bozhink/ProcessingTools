namespace ProcessingTools.Layout.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class NlmInitialFormatTransformer : XslTransformer<INlmInitialFormatXslTransformProvider>, INlmInitialFormatTransformer
    {
        public NlmInitialFormatTransformer(INlmInitialFormatXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

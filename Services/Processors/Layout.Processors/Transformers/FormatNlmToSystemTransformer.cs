namespace ProcessingTools.Layout.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class FormatNlmToSystemTransformer : XslTransformer<IFormatNlmToSystemXslTransformProvider>, IFormatNlmToSystemTransformer
    {
        public FormatNlmToSystemTransformer(IFormatNlmToSystemXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

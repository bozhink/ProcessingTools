namespace ProcessingTools.Layout.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class FormatToSystemTransformer : XslTransformer<IFormatNlmToSystemXslTransformProvider>, IFormatToSystemTransformer
    {
        public FormatToSystemTransformer(IFormatNlmToSystemXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

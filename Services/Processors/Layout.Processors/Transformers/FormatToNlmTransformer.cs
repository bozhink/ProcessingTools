namespace ProcessingTools.Layout.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class FormatToNlmTransformer : XslTransformer<IFormatSystemToNlmXslTransformProvider>, IFormatToNlmTransformer
    {
        public FormatToNlmTransformer(IFormatSystemToNlmXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

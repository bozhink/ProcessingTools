namespace ProcessingTools.Layout.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class FormatSystemToNlmTransformer : XslTransformer<IFormatSystemToNlmXslTransformProvider>, IFormatSystemToNlmTransformer
    {
        public FormatSystemToNlmTransformer(IFormatSystemToNlmXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

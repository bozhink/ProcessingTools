namespace ProcessingTools.Harvesters.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class GetTextContentTransformer : XslTransformer<ITextContentXslTransformProvider>, IGetTextContentTransformer
    {
        public GetTextContentTransformer(ITextContentXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

namespace ProcessingTools.Harvesters.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class GetExternalLinksTransformer : XslTransformer<IGetExternalLinksXslTransformProvider>, IGetExternalLinksTransformer
    {
        public GetExternalLinksTransformer(IGetExternalLinksXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

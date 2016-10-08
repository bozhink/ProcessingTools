namespace ProcessingTools.Xml.Transformers
{
    using Contracts.Providers;
    using Contracts.Transformers;

    public class XslTransformer : XslTransformer<IXslTransformProvider>, IXslTransformer
    {
        public XslTransformer(IXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

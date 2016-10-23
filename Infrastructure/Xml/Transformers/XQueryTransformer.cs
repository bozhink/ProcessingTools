namespace ProcessingTools.Xml.Transformers
{
    using Contracts.Providers;
    using Contracts.Transformers;

    public class XQueryTransformer : XQueryTransformer<IXQueryTransformProvider>, IXQueryTransformer
    {
        public XQueryTransformer(IXQueryTransformProvider xqueryTransformProvider)
            : base(xqueryTransformProvider)
        {
        }
    }
}

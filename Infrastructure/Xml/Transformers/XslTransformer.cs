namespace ProcessingTools.Xml.Transformers
{
    using Contracts;

    public class XslTransformer : XslTransformer<IXslTransformProvider>, IXslTransformer
    {
        public XslTransformer(IXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

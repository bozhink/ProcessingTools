namespace ProcessingTools.Xml.Transformers
{
    using Contracts;

    public class XslTransformer<T> : XslTransformer, IXslTransformer<T>
        where T : IXslTransformProvider
    {
        public XslTransformer(T xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}

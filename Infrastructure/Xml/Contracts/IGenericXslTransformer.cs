namespace ProcessingTools.Xml.Contracts
{
    public interface IXslTransformer<T> : IXslTransformer
        where T : IXslTransformProvider
    {
    }
}

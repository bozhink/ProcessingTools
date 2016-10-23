namespace ProcessingTools.Xml.Contracts.Transformers
{
    using Providers;

    public interface IXQueryTransformer<T> : IXQueryTransformer
        where T : IXQueryTransformProvider
    {
    }
}

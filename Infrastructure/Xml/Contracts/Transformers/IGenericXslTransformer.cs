namespace ProcessingTools.Xml.Contracts.Transformers
{
    using Providers;

    public interface IXslTransformer<T> : IXslTransformer
        where T : IXslTransformProvider
    {
    }
}

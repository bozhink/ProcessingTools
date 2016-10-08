namespace ProcessingTools.Layout.Processors.Contracts.Transformers
{
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Transformers;

    public interface INlmInitialFormatTransformer : IXslTransformer<INlmInitialFormatXslTransformProvider>, IXmlTransformer
    {
    }
}

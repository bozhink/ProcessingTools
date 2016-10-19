namespace ProcessingTools.Xml.Contracts.Transformers
{
    using Providers;

    public interface IModifiableXslTransformer : IXslTransformer<IModifiableXslTransformProvider>
    {
        string XslFilePath { get; set; }
    }
}

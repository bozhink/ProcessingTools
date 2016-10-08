namespace ProcessingTools.Xml.Contracts
{
    using Providers;

    public interface IModifiableXslTransformer : IXslTransformer<IModifiableXslTransformProvider>
    {
        string XslFilePath { get; set; }
    }
}

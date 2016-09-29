namespace ProcessingTools.Xml.Contracts
{
    public interface IModifiableXslTransformer : IXslTransformer<IModifiableXslTransformProvider>
    {
        string XslFilePath { get; set; }
    }
}

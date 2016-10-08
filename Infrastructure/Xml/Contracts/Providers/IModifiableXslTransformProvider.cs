namespace ProcessingTools.Xml.Contracts.Providers
{
    public interface IModifiableXslTransformProvider : IXslTransformProvider
    {
        string XslFilePath { get; set; }
    }
}

namespace ProcessingTools.Xml.Contracts
{
    public interface IModifiableXslTransformProvider : IXslTransformProvider
    {
        string XslFilePath { get; set; }
    }
}

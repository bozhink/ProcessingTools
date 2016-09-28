namespace ProcessingTools.Xml.Contracts
{
    using System.Xml.Xsl;
    using ProcessingTools.Contracts;

    public interface IXslTransformProvider : IProvider
    {
        XslCompiledTransform GetXslTransform();
    }
}

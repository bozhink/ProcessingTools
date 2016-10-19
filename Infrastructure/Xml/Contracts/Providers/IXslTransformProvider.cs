namespace ProcessingTools.Xml.Contracts.Providers
{
    using System.Xml.Xsl;
    using ProcessingTools.Contracts;

    public interface IXslTransformProvider : IProvider
    {
        XslCompiledTransform GetXslTransform();
    }
}

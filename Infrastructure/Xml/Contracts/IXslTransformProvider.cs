namespace ProcessingTools.Xml.Contracts
{
    using System.Xml.Xsl;

    public interface IXslTransformProvider
    {
        XslCompiledTransform GetXslTransform();
    }
}

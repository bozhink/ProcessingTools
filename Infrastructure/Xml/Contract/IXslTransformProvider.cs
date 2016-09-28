namespace ProcessingTools.Xml.Contract
{
    using System.Xml.Xsl;

    public interface IXslTransformProvider
    {
        XslCompiledTransform GetXslTransform();
    }
}

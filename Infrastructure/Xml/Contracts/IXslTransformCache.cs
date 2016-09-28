namespace ProcessingTools.Xml.Contracts
{
    using System.Xml.Xsl;

    public interface IXslTransformCache
    {
        XslCompiledTransform this[string xslFileName] { get; }

        bool Remove(string xslFileName);

        bool RemoveAll();
    }
}

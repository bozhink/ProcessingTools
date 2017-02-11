namespace ProcessingTools.Xml.Contracts.Wrappers
{
    using System.Xml;

    public interface IXmlContextWrapper
    {
        XmlDocument Create(XmlNode context);
    }
}

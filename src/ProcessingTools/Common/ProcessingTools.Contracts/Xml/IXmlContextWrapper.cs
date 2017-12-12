namespace ProcessingTools.Contracts.Xml
{
    using System.Xml;

    public interface IXmlContextWrapper
    {
        XmlDocument Create(XmlNode context);
    }
}

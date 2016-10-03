namespace ProcessingTools.Xml.Contracts
{
    using System.Xml;

    public interface IXmlContextWrapperProvider
    {
        XmlDocument Create(XmlNode context);
    }
}

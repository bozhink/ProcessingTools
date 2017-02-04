namespace ProcessingTools.Xml.Contracts.Wrappers
{
    using System.Xml;

    public interface IXmlContextWrapperProvider
    {
        XmlDocument Create(XmlNode context);
    }
}

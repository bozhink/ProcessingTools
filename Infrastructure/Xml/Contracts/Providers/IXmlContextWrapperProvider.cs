namespace ProcessingTools.Xml.Contracts.Providers
{
    using System.Xml;

    public interface IXmlContextWrapperProvider
    {
        XmlDocument Create(XmlNode context);
    }
}

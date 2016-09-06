namespace ProcessingTools.Xml.Contract
{
    using System.Xml;

    public interface IXmlContextWrapperProvider
    {
        XmlDocument Create(XmlNode context);
    }
}

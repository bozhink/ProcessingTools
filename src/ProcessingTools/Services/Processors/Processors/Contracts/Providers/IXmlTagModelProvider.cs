namespace ProcessingTools.Contracts.Processors.Providers
{
    using System;
    using System.Xml;

    public interface IXmlTagModelProvider
    {
        Func<XmlDocument, XmlElement> TagModel { get; }
    }
}

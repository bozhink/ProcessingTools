namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Contracts.Providers;

    public class TypeStatusTagModelProvider : ITypeStatusTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.TypeStatusContentType);

            return tagModel;
        };
    }
}

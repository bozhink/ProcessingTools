namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Content;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Contracts.Providers;

    public class DateTagModelProvider : IDateTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypeConstants.DateContentType);

            return tagModel;
        };
    }
}

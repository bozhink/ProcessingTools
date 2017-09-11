namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Contracts.Providers;

    public class AltitudeTagModelProvider : IAltitudeTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.AltitudeContentType);

            return tagModel;
        };
    }
}

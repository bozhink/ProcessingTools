namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using Contracts.Providers;
    using ProcessingTools.Constants.Schema;

    public class CoordinateTagModelProvider : ICoordinateTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.GeoCoordinateElementName);

            return tagModel;
        };
    }
}

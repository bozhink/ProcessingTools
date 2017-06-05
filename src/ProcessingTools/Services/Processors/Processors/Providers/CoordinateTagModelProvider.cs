namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Contracts.Providers;

    public class CoordinateTagModelProvider : ICoordinateTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.GeoCoordinate);

            return tagModel;
        };
    }
}

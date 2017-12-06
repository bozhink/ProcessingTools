﻿namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Processors.Providers;

    public class GeographicDeviationTagModelProvider : IGeographicDeviationTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.GeographicDeviationContentType);

            return tagModel;
        };
    }
}

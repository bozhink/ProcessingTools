﻿namespace ProcessingTools.Processors.Providers
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Models.Contracts;

    public class QuantityTagModelProvider : IQuantityTagModelProvider
    {
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.QuantityContentType);

            return tagModel;
        };
    }
}

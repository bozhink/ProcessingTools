// <copyright file="GeoEpithetTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Services.Models.Geo;

    /// <summary>
    /// Geo epithet tag model provider.
    /// </summary>
    public class GeoEpithetTagModelProvider : IGeoEpithetTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.GeoEpithetContentType);

            return tagModel;
        };
    }
}

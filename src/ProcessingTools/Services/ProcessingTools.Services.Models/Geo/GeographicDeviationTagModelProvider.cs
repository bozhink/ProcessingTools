// <copyright file="GeographicDeviationTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geographic deviation tag model provider.
    /// </summary>
    public class GeographicDeviationTagModelProvider : IGeographicDeviationTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.GeographicDeviationContentType);

            return tagModel;
        };
    }
}

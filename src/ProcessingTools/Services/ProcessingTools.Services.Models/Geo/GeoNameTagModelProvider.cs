﻿// <copyright file="GeoNameTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Geo
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Services.Models.Geo;

    /// <summary>
    /// Geo name tag model provider.
    /// </summary>
    public class GeoNameTagModelProvider : IGeoNameTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.GeoNameContentType);

            return tagModel;
        };
    }
}
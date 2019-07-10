﻿// <copyright file="AltitudeTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Services.Models.Contracts;

    /// <summary>
    /// Altitude tag model provider.
    /// </summary>
    public class AltitudeTagModelProvider : IAltitudeTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.AltitudeContentType);

            return tagModel;
        };
    }
}

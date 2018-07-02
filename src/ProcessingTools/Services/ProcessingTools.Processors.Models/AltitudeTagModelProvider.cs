// <copyright file="AltitudeTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Models.Contracts;

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

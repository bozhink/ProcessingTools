// <copyright file="MorphologicalEpithetTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Services.Models.Bio;

    /// <summary>
    /// Morphological epithet tag model provider.
    /// </summary>
    public class MorphologicalEpithetTagModelProvider : IMorphologicalEpithetTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.MorphologicalEpithetContentType);

            return tagModel;
        };
    }
}

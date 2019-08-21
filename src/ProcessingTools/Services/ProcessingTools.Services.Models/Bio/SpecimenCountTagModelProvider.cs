// <copyright file="SpecimenCountTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Specimen count tag model provider.
    /// </summary>
    public class SpecimenCountTagModelProvider : ISpecimenCountTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.SpecimenCountContentType);

            return tagModel;
        };
    }
}

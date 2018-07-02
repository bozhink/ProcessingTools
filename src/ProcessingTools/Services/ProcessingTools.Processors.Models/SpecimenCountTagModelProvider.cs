// <copyright file="SpecimenCountTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Models.Contracts;

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

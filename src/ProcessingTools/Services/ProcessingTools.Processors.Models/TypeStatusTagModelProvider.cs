// <copyright file="TypeStatusTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Models.Contracts;

    /// <summary>
    /// Type status tag model provider.
    /// </summary>
    public class TypeStatusTagModelProvider : ITypeStatusTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.TypeStatusContentType);

            return tagModel;
        };
    }
}

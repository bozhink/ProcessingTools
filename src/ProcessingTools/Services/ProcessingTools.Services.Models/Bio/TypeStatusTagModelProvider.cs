// <copyright file="TypeStatusTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models.Bio;

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

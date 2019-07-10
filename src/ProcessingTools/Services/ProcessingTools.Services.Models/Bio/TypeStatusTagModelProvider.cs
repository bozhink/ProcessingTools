// <copyright file="TypeStatusTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Xml;
using ProcessingTools.Common.Constants.Schema;
using ProcessingTools.Contracts.Services.Models.Bio;

namespace ProcessingTools.Services.Models.Bio
{
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

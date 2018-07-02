// <copyright file="ProductTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Processors.Models.Contracts;

    /// <summary>
    /// Product tag model provider.
    /// </summary>
    public class ProductTagModelProvider : IProductTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.ProductContentType);

            return tagModel;
        };
    }
}

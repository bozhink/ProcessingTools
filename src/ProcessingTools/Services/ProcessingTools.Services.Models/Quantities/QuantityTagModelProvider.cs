// <copyright file="QuantityTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Xml;
using ProcessingTools.Common.Constants.Schema;
using ProcessingTools.Contracts.Services.Models.Quantities;

namespace ProcessingTools.Services.Models.Quantities
{
    /// <summary>
    /// Quantity tag model provider.
    /// </summary>
    public class QuantityTagModelProvider : IQuantityTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.QuantityContentType);

            return tagModel;
        };
    }
}

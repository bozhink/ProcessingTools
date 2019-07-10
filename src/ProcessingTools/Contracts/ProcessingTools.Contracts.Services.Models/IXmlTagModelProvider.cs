// <copyright file="IXmlTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Xml;

namespace ProcessingTools.Contracts.Services.Models
{
    /// <summary>
    /// XML tag model provider.
    /// </summary>
    public interface IXmlTagModelProvider
    {
        /// <summary>
        /// Gets the tag model.
        /// </summary>
        Func<XmlDocument, XmlElement> TagModel { get; }
    }
}

// <copyright file="IXmlTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;
    using System.Xml;

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

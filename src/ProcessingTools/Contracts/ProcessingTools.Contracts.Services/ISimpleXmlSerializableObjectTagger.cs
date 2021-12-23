﻿// <copyright file="ISimpleXmlSerializableObjectTagger.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Services.Models.Content;

    /// <summary>
    /// Simple XML serializable object tagger.
    /// </summary>
    /// <typeparam name="T">Type of XML model.</typeparam>
    public interface ISimpleXmlSerializableObjectTagger<T>
    {
        /// <summary>
        /// Tag.
        /// </summary>
        /// <param name="context">Context to be processed.</param>
        /// <param name="namespaceManager"><see cref="XmlNamespaceManager"/>.</param>
        /// <param name="items">Items to be tagged.</param>
        /// <param name="xpath">XPath to select location for tagging.</param>
        /// <param name="settings">Setting for tagging.</param>
        /// <returns>Task.</returns>
        Task<object> TagAsync(XmlNode context, XmlNamespaceManager namespaceManager, IEnumerable<T> items, string xpath, IContentTaggerSettings settings);
    }
}

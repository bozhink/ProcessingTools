﻿// <copyright file="IStringTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;

    /// <summary>
    /// String tagger.
    /// </summary>
    public interface IStringTagger
    {
        /// <summary>
        /// Tag.
        /// </summary>
        /// <param name="document">Document to be processed.</param>
        /// <param name="items">Items to be tagged.</param>
        /// <param name="tagModel">Tag model.</param>
        /// <param name="xpath">XPath to select location for tagging.</param>
        /// <returns>Task.</returns>
        Task<object> TagAsync(IDocument document, IEnumerable<string> items, XmlElement tagModel, string xpath);
    }
}

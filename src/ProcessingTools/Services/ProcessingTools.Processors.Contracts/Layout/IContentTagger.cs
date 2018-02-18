// <copyright file="IContentTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Layout
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Processors.Layout;

    /// <summary>
    /// Content tagger.
    /// </summary>
    public interface IContentTagger
    {
        /// <summary>
        /// Tag content in document.
        /// </summary>
        /// <param name="textToTag">Text to tag.</param>
        /// <param name="tagModel">Tag model.</param>
        /// <param name="xpath">XPath to select location for tagging.</param>
        /// <param name="document">Document to be tagged.</param>
        /// <param name="settings">Settings of tagging.</param>
        /// <returns>Task</returns>
        Task TagContentInDocumentAsync(string textToTag, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings);

        /// <summary>
        /// Tag content in document.
        /// </summary>
        /// <param name="textToTagList">List of texts to be tagged.</param>
        /// <param name="tagModel">Tag model.</param>
        /// <param name="xpath">XPath to select location for tagging.</param>
        /// <param name="document">Document to be tagged.</param>
        /// <param name="settings">Settings of tagging.</param>
        /// <returns>Task</returns>
        Task TagContentInDocumentAsync(IEnumerable<string> textToTagList, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings);

        /// <summary>
        /// Tag content in document.
        /// </summary>
        /// <param name="nodeList">Node list to perform tagging.</param>
        /// <param name="settings">Settings of tagging.</param>
        /// <param name="items">Items to be tagged.</param>
        /// <returns>Task</returns>
        Task TagContentInDocumentAsync(IEnumerable<XmlNode> nodeList, IContentTaggerSettings settings, params XmlElement[] items);
    }
}

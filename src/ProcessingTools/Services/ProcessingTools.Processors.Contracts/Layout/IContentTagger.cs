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
        Task TagContentInDocument(string textToTag, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings);

        Task TagContentInDocument(IEnumerable<string> textToTagList, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings);

        Task TagContentInDocument(IEnumerable<XmlNode> nodeList, IContentTaggerSettings settings, params XmlElement[] items);
    }
}

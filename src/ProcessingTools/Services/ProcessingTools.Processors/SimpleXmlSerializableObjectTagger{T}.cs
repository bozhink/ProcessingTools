// <copyright file="SimpleXmlSerializableObjectTagger{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Serialization;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Models.Contracts;

    /// <summary>
    /// Simple XML serializable object tagger.
    /// </summary>
    /// <typeparam name="T">Type of serializable model.</typeparam>
    public class SimpleXmlSerializableObjectTagger<T> : ISimpleXmlSerializableObjectTagger<T>
    {
        private readonly IXmlSerializer<T> serializer;
        private readonly IContentTagger contentTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleXmlSerializableObjectTagger{T}"/> class.
        /// </summary>
        /// <param name="serializer">Serializer.</param>
        /// <param name="contentTagger">Content tagger.</param>
        public SimpleXmlSerializableObjectTagger(IXmlSerializer<T> serializer, IContentTagger contentTagger)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(XmlNode context, XmlNamespaceManager namespaceManager, IEnumerable<T> items, string xpath, IContentTaggerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.serializer.SetNamespaces(namespaceManager);

            var nodeList = context.SelectNodes(xpath, namespaceManager).Cast<XmlNode>().ToList();

            var dataList = items.ToList();
            var data = dataList.Select(x => this.serializer.SerializeAsync(x).Result)
                .Cast<XmlElement>()
                .OrderByDescending(i => i.InnerText.Length)
                .ToArray();

            await this.contentTagger.TagContentInDocumentAsync(nodeList, settings, data).ConfigureAwait(false);

            return true;
        }
    }
}

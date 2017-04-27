namespace ProcessingTools.Layout.Processors.Processors.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;
    using ProcessingTools.Serialization.Contracts;

    public class SimpleXmlSerializableObjectTagger<T> : ISimpleXmlSerializableObjectTagger<T>
    {
        private readonly IXmlSerializer<T> serializer;
        private readonly IContentTagger contentTagger;

        public SimpleXmlSerializableObjectTagger(
            IXmlSerializer<T> serializer,
            IContentTagger contentTagger)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.serializer = serializer;
            this.contentTagger = contentTagger;
        }

        public async Task<object> Tag(XmlNode context, XmlNamespaceManager namespaceManager, IEnumerable<T> data, string contentNodesXPath, IContentTaggerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (string.IsNullOrWhiteSpace(contentNodesXPath))
            {
                throw new ArgumentNullException(nameof(contentNodesXPath));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.serializer.SetNamespaces(namespaceManager);

            var nodeList = context.SelectNodes(contentNodesXPath, namespaceManager)
                .Cast<XmlNode>();

            var items = data.ToList()
                .Select(x => this.serializer.Serialize(x).Result)
                .Cast<XmlElement>()
                .OrderByDescending(i => i.InnerText.Length)
                .ToArray();

            await this.contentTagger.TagContentInDocument(nodeList, settings, items);

            return true;
        }
    }
}

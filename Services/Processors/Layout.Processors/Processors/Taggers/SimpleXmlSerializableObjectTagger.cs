namespace ProcessingTools.Layout.Processors.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Serialization.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class SimpleXmlSerializableObjectTagger<T>
    {
        private readonly IXmlSerializer<T> serializer;
        private readonly ILogger logger;

        public SimpleXmlSerializableObjectTagger(IXmlSerializer<T> serializer, ILogger logger)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            this.serializer = serializer;
            this.logger = logger;
        }

        public async Task<object> Tag(XmlNode context, XmlNamespaceManager namespaceManager, IEnumerable<T> data, string contentNodesXPath, bool caseSensitive, bool minimalTextSelect)
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

            this.serializer.SetNamespaces(namespaceManager);

            var nodeList = context.SelectNodes(contentNodesXPath, namespaceManager)
                .Cast<XmlNode>();

            var items = data.ToList()
                .Select(x => this.serializer.Serialize(x).Result)
                .Cast<XmlElement>()
                .OrderByDescending(i => i.InnerText.Length)
                .ToList();

            foreach (var item in items)
            {
                await item.TagContentInDocument(nodeList, caseSensitive, minimalTextSelect, this.logger);
            }

            return true;
        }
    }
}

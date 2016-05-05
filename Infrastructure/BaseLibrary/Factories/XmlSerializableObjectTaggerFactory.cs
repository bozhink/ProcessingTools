namespace ProcessingTools.BaseLibrary.Factories
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public abstract class XmlSerializableObjectTaggerFactory<T> : TaxPubDocument, ITagger
    {
        private readonly XmlSerializerNamespaces xmlns;
        private readonly XmlSerializer serializer;
        private XmlDocument bufferXml;

        public XmlSerializableObjectTaggerFactory(string xml, XmlNamespaceManager namespaceManager)
            : base(xml)
        {
            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            this.bufferXml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            this.serializer = new XmlSerializer(typeof(T));

            this.xmlns = new XmlSerializerNamespaces();
            var ns = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.All);
            foreach (var prefix in ns.Keys)
            {
                this.xmlns.Add(prefix, ns[prefix]);
            }
        }

        public abstract Task Tag();

        protected XmlElement SerializeObject(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                this.serializer.Serialize(stream, obj, this.xmlns);
                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream);
                this.bufferXml.LoadXml(reader.ReadToEnd());
            }

            return (XmlElement)this.bufferXml.DocumentElement.CloneNode(true);
        }
    }
}

namespace ProcessingTools.Serialization.Serializers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using Contracts;

    public class XmlSerializer<T> : IXmlSerializer<T>
    {
        private readonly XmlSerializer serializer;
        private XmlDocument bufferXml;
        private XmlSerializerNamespaces xmlns;

        public XmlSerializer()
        {
            this.serializer = new XmlSerializer(typeof(T));
            this.xmlns = null;

            this.bufferXml = new XmlDocument
            {
                PreserveWhitespace = true
            };
        }

        public Task<XmlNode> Serialize(T @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            return Task.Run(() => this.SerializeSync(@object));
        }

        public void SetNamespaces(XmlNamespaceManager namespaceManager)
        {
            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            this.xmlns = new XmlSerializerNamespaces();
            var ns = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.All);
            foreach (var prefix in ns.Keys)
            {
                this.xmlns.Add(prefix, ns[prefix]);
            }
        }

        private XmlNode SerializeSync(T @object)
        {
            using (var stream = new MemoryStream())
            {
                if (this.xmlns == null)
                {
                    this.serializer.Serialize(stream, @object);
                }
                else
                {
                    this.serializer.Serialize(stream, @object, this.xmlns);
                }

                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream);
                this.bufferXml.LoadXml(reader.ReadToEnd());

                stream.Close();
            }

            return this.bufferXml.DocumentElement.CloneNode(true);
        }
    }
}

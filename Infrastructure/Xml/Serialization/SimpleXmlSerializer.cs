namespace ProcessingTools.Xml.Serialization
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    public class SimpleXmlSerializer<T>
    {
        private readonly XmlSerializerNamespaces xmlSerializerNamespaces;
        private readonly XmlSerializer serializer;

        public SimpleXmlSerializer(XmlNamespaceManager namespaceManager)
        {
            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            this.serializer = new XmlSerializer(typeof(T));

            this.xmlSerializerNamespaces = new XmlSerializerNamespaces();
            var namespaces = namespaceManager.GetNamespacesInScope(XmlNamespaceScope.All);
            foreach (var prefix in namespaces.Keys)
            {
                this.xmlSerializerNamespaces.Add(prefix, namespaces[prefix]);
            }
        }

        public async Task<string> SerializeObjectToString(T obj)
        {
            string result = null;

            using (var stream = new MemoryStream())
            {
                this.serializer.Serialize(stream, obj, this.xmlSerializerNamespaces);
                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream);
                result = await reader.ReadToEndAsync();
            }

            return result;
        }

        public async Task<XmlElement> SerializeObject(T obj)
        {
            var bufferXml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            bufferXml.LoadXml(await this.SerializeObjectToString(obj));

            return bufferXml.DocumentElement;
        }
    }
}

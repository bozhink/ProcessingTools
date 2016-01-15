namespace ProcessingTools.BaseLibrary
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;

    public class XmlSerializableObjectTagger<T> : Base, ITagger
    {
        private string contentNodesXPathTemplate;
        private IQueryable<T> data;
        private ILogger logger;
        private XmlDocument bufferXml;
        private XmlSerializer serializer;

        public XmlSerializableObjectTagger(string xml, IQueryable<T> data, string contentNodesXPathTemplate, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.contentNodesXPathTemplate = contentNodesXPathTemplate;
            this.logger = logger;

            this.bufferXml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            this.serializer = new XmlSerializer(typeof(T));
        }

        public void Tag()
        {
            this.data.ToList()
                .Select(this.SerializeObject)
                .OrderByDescending(i => i.InnerText.Length)
                .TagContentInDocument(
                    this.contentNodesXPathTemplate,
                    this.XmlDocument,
                    false,
                    true,
                    this.logger);
        }

        private XmlElement SerializeObject(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                this.serializer.Serialize(stream, obj);
                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream);
                this.bufferXml.LoadXml(reader.ReadToEnd());
            }

            return (XmlElement)this.bufferXml.DocumentElement.CloneNode(true);
        }
    }
}
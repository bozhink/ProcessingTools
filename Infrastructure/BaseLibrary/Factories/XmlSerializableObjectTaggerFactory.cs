namespace ProcessingTools.BaseLibrary.Factories
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public abstract class XmlSerializableObjectTaggerFactory<T> : TaxPubDocument, ITagger
    {
        private XmlDocument bufferXml;
        private XmlSerializer serializer;

        public XmlSerializableObjectTaggerFactory(string xml)
            : base(xml)
        {
            this.bufferXml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            this.serializer = new XmlSerializer(typeof(T));
        }

        public abstract Task Tag();

        protected XmlElement SerializeObject(T obj)
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

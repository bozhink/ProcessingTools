namespace ProcessingTools.Serialization.Tests
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void SerializeExternalLinkModel_ShouldWork()
        {
            var link = new ExternalLinkSerializableModel()
            {
                Value = "http://example.com",
                ExtLinkType = "uri",
                Href = "http://example.com"
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ExternalLinkSerializableModel));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, link);
                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream, new UTF8Encoding());
                var result = reader.ReadToEnd();

                var xml = new XmlDocument();
                xml.LoadXml(result);

                System.Console.WriteLine(xml.DocumentElement.OuterXml);
            }
        }

        [TestMethod]
        public void SerializeExternalLinkModel_WithoutHref_ShouldWork()
        {
            var link = new ExternalLinkSerializableModel()
            {
                Value = "http://example.com",
                ExtLinkType = "uri"
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ExternalLinkSerializableModel));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, link);
                stream.Flush();
                stream.Position = 0;

                var reader = new StreamReader(stream, new UTF8Encoding());
                var result = reader.ReadToEnd();

                var xml = new XmlDocument();
                xml.LoadXml(result);

                System.Console.WriteLine(xml.DocumentElement.OuterXml);
            }
        }
    }
}

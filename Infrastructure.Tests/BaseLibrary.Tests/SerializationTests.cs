namespace ProcessingTools.BaseLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using System.IO;
    using System.Xml.Serialization;
    using System.Text;

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
                stream.Position = 0;

                var reader = new StreamReader(stream, new UTF8Encoding());
                var result = reader.ReadToEnd();

                System.Console.WriteLine(result);
            }
        }
    }
}
// <copyright file="SerializationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Integration.Tests.Serialization
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Code.Tests.Models;

    /// <summary>
    /// Serialization Tests.
    /// </summary>
    [TestClass]
    public class SerializationTests
    {
        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Serialize <see cref="ExternalLinkSerializableModel"/> should work.
        /// </summary>
        [TestMethod]
        public void SerializeExternalLinkModel_ShouldWork()
        {
            var link = new ExternalLinkSerializableModel
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

                this.TestContext.WriteLine(xml.DocumentElement.OuterXml);
            }
        }

        /// <summary>
        /// Serialize <see cref="ExternalLinkSerializableModel"/> without @href should work.
        /// </summary>
        [TestMethod]
        public void SerializeExternalLinkModel_WithoutHref_ShouldWork()
        {
            var link = new ExternalLinkSerializableModel
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

                this.TestContext.WriteLine(xml.DocumentElement.OuterXml);
            }
        }
    }
}

// <copyright file="GlobalNamesResolverXmlModelsTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Integration.Tests.Bio.Taxonomy.GlobalNamesResolver
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.GlobalNamesResolver.Xml;

    /// <summary>
    /// Global Names Resolver XML models tests.
    /// </summary>
    [TestClass]
    public class GlobalNamesResolverXmlModelsTests
    {
        /// <summary>
        /// Global Names Resolver XML models deserialization of first sample file should work.
        /// </summary>
        [TestMethod]
        public void GlobalNamesResolver_XmlModels_DeserializationOfFirstSampleFile_ShouldWork()
        {
            // Arrange
            XmlSerializer serializer = new XmlSerializer(typeof(Hash));
            Hash hash = null;

            string fileName = @"data-files/gnr-sample-1.xml";

            // Act
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    hash = serializer.Deserialize(reader) as Hash;
                }
            }

            // Assert
            Assert.IsNotNull(hash);

            Assert.IsNotNull(hash.Id);
            Assert.AreEqual("hiij5yf4b0jb", hash.Id.Value);

            Assert.IsNotNull(hash.Url);
            Assert.AreEqual("http://resolver.globalnames.org/name_resolvers/hiij5yf4b0jb.xml", hash.Url.Value);

            Assert.IsNotNull(hash.DataSources);
            Assert.AreEqual("array", hash.DataSources.Type);

            Assert.IsNotNull(hash.Data);
            Assert.AreEqual("array", hash.Data.Type);

            Assert.IsNotNull(hash.Data.Datum);
            Assert.AreEqual(42, hash.Data.Datum.Length);
        }
    }
}

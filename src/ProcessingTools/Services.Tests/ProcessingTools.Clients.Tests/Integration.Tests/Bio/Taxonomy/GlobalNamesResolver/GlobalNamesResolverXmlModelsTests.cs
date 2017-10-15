// <copyright file="GlobalNamesResolverXmlModelsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.Taxonomy.GlobalNamesResolver
{
    using System.IO;
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
            XmlSerializer serializer = new XmlSerializer(typeof(Hash));
            Hash hash = null;

            using (var stream = new FileStream(@"DataFiles\Bio\Taxonomy\GlobalNamesResolver\gnr-sample-1.xml", FileMode.Open))
            {
                hash = serializer.Deserialize(stream) as Hash;
            }

            Assert.IsNotNull(hash, "Hash should not be null.");

            Assert.IsNotNull(hash.Id, "Hash.Id should not be null.");
            Assert.AreEqual("hiij5yf4b0jb", hash.Id.Value, "Hash.Id.Value should be set to correct value.");

            Assert.IsNotNull(hash.Url, "Hash.Url should not be null.");
            Assert.AreEqual("http://resolver.globalnames.org/name_resolvers/hiij5yf4b0jb.xml", hash.Url.Value, "Hash.Url.Value should be set to correct value.");

            Assert.IsNotNull(hash.DataSources, "Hash.DataSources should not be null.");
            Assert.AreEqual("array", hash.DataSources.Type, "Hash.DataSources.Type should be set to correct value.");

            Assert.IsNotNull(hash.Data, "Hash.Data should not be null.");
            Assert.AreEqual("array", hash.Data.Type, "Hash.Data.Type should be set to correct value.");

            Assert.IsNotNull(hash.Data.Datum, "Hash.Data.Datum should not be null.");
            Assert.AreEqual(42, hash.Data.Datum.Length, "Hash.Data.Datum.Length should be set to correct value.");
        }
    }
}

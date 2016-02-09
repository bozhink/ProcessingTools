namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Tests
{
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models.Xml;

    [TestClass]
    public class GlobalNamesResolverXmlModelsTests
    {
        private const string ShouldNotBeNullMessage = " should not be null.";
        private const string ShouldBeSetToCorrectValueMessage = " should be set to correct value.";

        [TestMethod]
        public void GlobalNamesResolver_XmlModels_DeserializationOfFirstSampleFile_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Hash));
            Hash hash = null;

            using (var stream = new FileStream(@"DataFiles\gnr-sample-1.xml", FileMode.Open))
            {
                hash = serializer.Deserialize(stream) as Hash;
            }

            Assert.IsNotNull(hash, "Hash" + ShouldNotBeNullMessage);

            Assert.IsNotNull(hash.Id, "Hash.Id" + ShouldNotBeNullMessage);
            Assert.AreEqual("hiij5yf4b0jb", hash.Id.Value, "Hash.Id.Value" + ShouldBeSetToCorrectValueMessage);

            Assert.IsNotNull(hash.Url, "Hash.Url" + ShouldNotBeNullMessage);
            Assert.AreEqual("http://resolver.globalnames.org/name_resolvers/hiij5yf4b0jb.xml", hash.Url.Value, "Hash.Url.Value" + ShouldBeSetToCorrectValueMessage);

            Assert.IsNotNull(hash.DataSources, "Hash.DataSources" + ShouldNotBeNullMessage);
            Assert.AreEqual("array", hash.DataSources.Type, "Hash.DataSources.Type" + ShouldBeSetToCorrectValueMessage);

            Assert.IsNotNull(hash.Data, "Hash.Data" + ShouldNotBeNullMessage);
            Assert.AreEqual("array", hash.Data.Type, "Hash.Data.Type" + ShouldBeSetToCorrectValueMessage);

            Assert.IsNotNull(hash.Data.Datum, "Hash.Data.Datum" + ShouldNotBeNullMessage);
            Assert.AreEqual(42, hash.Data.Datum.Length, "Hash.Data.Datum.Length" + ShouldBeSetToCorrectValueMessage);
        }
    }
}

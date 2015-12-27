namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Tests
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    using Infrastructure.Serialization.Json;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class GbifApiResponseModelDeserializationTests
    {
        private const string SampleGbifResponseJsonColeoptera = @"DataFiles\\Coleoptera-gbif-response.json";

        [TestMethod]
        public void GbifApiResponseModel_Deserialization_UsingSystemSerializer_ShouldWork()
        {
            string jsonString = File.ReadAllText(SampleGbifResponseJsonColeoptera);
            Assert.IsFalse(string.IsNullOrWhiteSpace(jsonString));

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var serializer = new DataContractJsonSerializer(typeof(GbifApiResponseModel));

            var gbifObject = (GbifApiResponseModel)serializer.ReadObject(stream);

            string scientificName = "Coleoptera";
            Assert.AreEqual(scientificName, gbifObject.CanonicalName, "CanonicalName should match.");
        }

        [TestMethod]
        public void GbifApiResponseModel_Deserialization_InfrastructureJsonSerializer_ShouldWork()
        {
            string jsonString = File.ReadAllText(SampleGbifResponseJsonColeoptera);
            Assert.IsFalse(string.IsNullOrWhiteSpace(jsonString));

            var gbifObject = JsonSerializer.Deserialize<GbifApiResponseModel>(jsonString);

            string scientificName = "Coleoptera";
            Assert.AreEqual(scientificName, gbifObject.CanonicalName, "CanonicalName should match.");
        }
    }
}
namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Tests
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

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
            var serializer = new DataContractJsonSerializer(typeof(GbifApiV09ResponseModel));

            var gbifObject = (GbifApiV09ResponseModel)serializer.ReadObject(stream);

            string scientificName = "Coleoptera";
            Assert.AreEqual(scientificName, gbifObject.CanonicalName, "CanonicalName should match.");
        }

        [TestMethod]
        public void GbifApiResponseModel_Deserialization_InfrastructureJsonSerializer_ShouldWork()
        {
            string jsonString = File.ReadAllText(SampleGbifResponseJsonColeoptera);
            Assert.IsFalse(string.IsNullOrWhiteSpace(jsonString));

            GbifApiV09ResponseModel gbifObject = null;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(GbifApiV09ResponseModel));
                gbifObject = (GbifApiV09ResponseModel)serializer.ReadObject(stream);
            }

            string scientificName = "Coleoptera";
            Assert.AreEqual(scientificName, gbifObject.CanonicalName, "CanonicalName should match.");
        }
    }
}
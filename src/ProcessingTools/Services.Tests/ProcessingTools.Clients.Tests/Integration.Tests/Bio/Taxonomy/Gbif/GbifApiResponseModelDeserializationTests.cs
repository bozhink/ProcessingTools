// <copyright file="GbifApiResponseModelDeserializationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.Taxonomy.Gbif
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;

    /// <summary>
    /// GBIF response model deserialization tests.
    /// </summary>
    [TestClass]
    public class GbifApiResponseModelDeserializationTests
    {
        private const string SampleGbifResponseJsonColeoptera = @"DataFiles\Bio\Taxonomy\Gbif\Coleoptera-gbif-response.json";

        /// <summary>
        /// GBIF response model deserialization using system serializer should work.
        /// </summary>
        [TestMethod]
        public void GbifApiResponseModel_Deserialization_UsingSystemSerializer_ShouldWork()
        {
            string jsonString = File.ReadAllText(SampleGbifResponseJsonColeoptera);
            Assert.IsFalse(string.IsNullOrWhiteSpace(jsonString));

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var serializer = new DataContractJsonSerializer(typeof(GbifApiV09ResponseModel));

            var gbifObject = (GbifApiV09ResponseModel)serializer.ReadObject(stream);

            Assert.AreEqual("Coleoptera", gbifObject.CanonicalName, "CanonicalName should match.");
        }

        /// <summary>
        /// GBIF response model deserialization infrastructure json serializer should work.
        /// </summary>
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

            Assert.AreEqual("Coleoptera", gbifObject.CanonicalName, "CanonicalName should match.");
        }
    }
}

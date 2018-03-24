// <copyright file="ExtractHcmrModelsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.ExtractHcmr
{
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml;

    /// <summary>
    /// EXTRACT HCMR models tests.
    /// </summary>
    [TestClass]
    public class ExtractHcmrModelsTests
    {
        /// <summary>
        /// <see cref="ExtractHcmrResponseModel"/> deserialization tests.
        /// </summary>
        [TestMethod]
        public void ExtractHcmrResponseModel_Deserialization_Tests()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ExtractHcmrResponseModel));
            ExtractHcmrResponseModel response = null;

            using (var stream = new FileStream(@"DataFiles\Bio\ExtractHcmr\sample.xml", FileMode.Open))
            {
                response = serializer.Deserialize(stream) as ExtractHcmrResponseModel;
            }

            Assert.IsNotNull(response, "Response should not be null");

            Assert.IsNotNull(response.Items, "Response items should not be null");

            const int NumberOfReponseItems = 3;

            Assert.AreEqual(
                NumberOfReponseItems,
                response.Items.Length,
                $"The number of response items should be {NumberOfReponseItems}");

            var volcanoItem = response.Items.FirstOrDefault(i => i.Name == "Volcano");

            Assert.IsNotNull(volcanoItem, "Volcano item should not be null.");

            Assert.AreEqual(
                "ENVO:00000247",
                volcanoItem.Entities.FirstOrDefault().Identifier,
                "Volcano identifier should match.");

            var zetaproteobacteriaItem = response.Items.FirstOrDefault(i => i.Name == "Zetaproteobacteria");

            Assert.IsNotNull(zetaproteobacteriaItem, "Zetaproteobacteria item should not be null.");

            Assert.AreEqual(
                "580370",
                zetaproteobacteriaItem.Entities.FirstOrDefault().Identifier,
                "Zetaproteobacteria identifier should match.");

            var sedimentsItem = response.Items.FirstOrDefault(i => i.Name == "sediments");

            Assert.IsNotNull(sedimentsItem, "Sediments item should not be null.");

            Assert.AreEqual(
                "ENVO:00002007",
                sedimentsItem.Entities.FirstOrDefault().Identifier,
                "Sediments identifier should match.");
        }
    }
}

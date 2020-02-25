// <copyright file="ExtractHcmrModelsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Integration.Tests.Bio.ExtractHcmr
{
    using System.IO;
    using System.Linq;
    using System.Xml;
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
            // Arrange
            XmlSerializer serializer = new XmlSerializer(typeof(ExtractHcmrResponseModel));
            ExtractHcmrResponseModel response = null;

            string fileName = @"data-files/extract-hcmr-sample.xml";

            // Act
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    response = serializer.Deserialize(reader) as ExtractHcmrResponseModel;
                }
            }

            // Assert
            Assert.IsNotNull(response);

            Assert.IsNotNull(response.Items);

            const int NumberOfReponseItems = 3;

            Assert.AreEqual(NumberOfReponseItems, response.Items.Length);

            var volcanoItem = response.Items.FirstOrDefault(i => i.Name == "Volcano");

            Assert.IsNotNull(volcanoItem);

            Assert.AreEqual("ENVO:00000247", volcanoItem.Entities.FirstOrDefault().Identifier);

            var zetaproteobacteriaItem = response.Items.FirstOrDefault(i => i.Name == "Zetaproteobacteria");

            Assert.IsNotNull(zetaproteobacteriaItem);

            Assert.AreEqual("580370", zetaproteobacteriaItem.Entities.FirstOrDefault().Identifier);

            var sedimentsItem = response.Items.FirstOrDefault(i => i.Name == "sediments");

            Assert.IsNotNull(sedimentsItem);

            Assert.AreEqual("ENVO:00002007", sedimentsItem.Entities.FirstOrDefault().Identifier);
        }
    }
}

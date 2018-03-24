// <copyright file="CatalogueOfLifeModelsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Tests.Integration.Tests.Bio.Taxonomy.CatalogueOfLife
{
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml;

    /// <summary>
    /// Catalogue of Life models tests.
    /// </summary>
    [TestClass]
    public class CatalogueOfLifeModelsTests
    {
        /// <summary>
        /// Catalogue of Life models tests deserialization.
        /// </summary>
        [TestMethod]
        public void CoLModel_Test_Deserialization()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceResponseModel));
            CatalogueOfLifeApiServiceResponseModel response = null;

            using (var stream = new FileStream(@"DataFiles\Bio\Taxonomy\CatalogueOfLife\Coleoptera-example-response.xml", FileMode.Open))
            {
                response = serializer.Deserialize(stream) as CatalogueOfLifeApiServiceResponseModel;
            }

            const string ScientificName = "Coleoptera";
            Assert.AreEqual(ScientificName, response.Name, "response.Name should match.");

            var firstMatchingResult = response.Results
                .FirstOrDefault(r => r.Name == ScientificName);

            Assert.AreEqual(ScientificName, firstMatchingResult.Name, "firstMatchingResult.Name should match.");
        }

        /// <summary>
        /// Classification <see cref="Taxon"/> model deserialization should work.
        /// </summary>
        [TestMethod]
        public void ClassificationTaxonModel_Deserialization_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Taxon));
            Taxon response = null;

            using (var reader = new StreamReader(@"DataFiles\Bio\Taxonomy\CatalogueOfLife\classification-taxon.xml"))
            {
                response = (Taxon)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(response, "Deserialized object should not be null.");
            Assert.AreEqual("Insecta", response.Name, "Name should match.");
        }

        /// <summary>
        /// <see cref="Reference"/> model deserialization should work.
        /// </summary>
        [TestMethod]
        public void ReferenceModel_Deserialization_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Reference));
            Reference response = null;

            using (var reader = new StreamReader(@"DataFiles\Bio\Taxonomy\CatalogueOfLife\reference.xml"))
            {
                response = (Reference)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(response, "Deserialized object should not be null.");
            Assert.AreEqual("H. Lindstr. & Soop", response.Author, "Author should match.");
            Assert.AreEqual("1999", response.Year, "Year should match.");
        }

        /// <summary>
        /// <see cref="Result"/> model deserialization should work.
        /// </summary>
        [TestMethod]
        public void ResultModel_Deserialization_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Result));
            Result response = null;

            using (var reader = new StreamReader(@"DataFiles\Bio\Taxonomy\CatalogueOfLife\default-result.xml"))
            {
                response = (Result)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(response, "Deserialized object should not be null.");
            Assert.AreEqual("Coleoptera", response.Name, "Name should match.");
            Assert.AreEqual("Order", response.Rank, "Rank should match.");

            // Classification
            const int NumberOfClassificationItems = 3;
            Assert.AreEqual(NumberOfClassificationItems, response.Classification.Length, $"The number of classifications items should be {NumberOfClassificationItems}.");

            Assert.AreEqual("Animalia", response.Classification[0].Name, "The name of the first classification item should match.");
            Assert.AreEqual("Kingdom", response.Classification[0].Rank, "The rank of the first classification item should match.");

            Assert.AreEqual("Arthropoda", response.Classification[1].Name, "The name of the second classification item should match.");
            Assert.AreEqual("Phylum", response.Classification[1].Rank, "The rank of the second classification item should match.");

            Assert.AreEqual("Insecta", response.Classification[2].Name, "The name of the third classification item should match.");
            Assert.AreEqual("Class", response.Classification[2].Rank, "The rank of the third classification item should match.");

            // ChildTaxa
            const int NumberOfChildTaxa = 33;
            Assert.AreEqual(NumberOfChildTaxa, response.ChildTaxa.Length, $"The number of child items should be {NumberOfChildTaxa}.");

            Assert.AreEqual("Amphizoidae", response.ChildTaxa[0].Name, "The name of the first child item should match.");
            Assert.AreEqual("Family", response.ChildTaxa[0].Rank, "The rank of the first child item should match.");
            Assert.AreEqual(false, response.ChildTaxa[0].IsExtinct, "IsExtinct property of the first child item should match.");
        }
    }
}

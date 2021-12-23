// <copyright file="CatalogueOfLifeApiServiceXmlResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Integration.Tests.Bio.Taxonomy.CatalogueOfLife
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using NUnit.Framework;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;

    /// <summary>
    /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(CatalogueOfLifeApiServiceXmlResponseModel), Author = "Bozhin Karaivanov")]
    public class CatalogueOfLifeApiServiceXmlResponseModelIntegrationTests
    {
        /// <summary>
        /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel"/> deserialization of valid XML should return valid object.
        /// </summary>
        [Test(TestOf = typeof(CatalogueOfLifeApiServiceXmlResponseModel), Author = "Bozhin Karaivanov", Description = "CatalogueOfLifeApiServiceXmlResponseModel deserialization of valid XML should return valid object.")]
        public void CatalogueOfLifeApiServiceXmlResponseModel_DeserializationOfValidXml_ShouldReturnValidObject()
        {
            // Arrange
            string fileName = @"data-files/col-coleoptera-response.xml";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));
            byte[] bytes = Encoding.UTF8.GetBytes(fileContent);

            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
            {
                ValidationType = ValidationType.None,
                DtdProcessing = DtdProcessing.Ignore,
                CloseInput = true,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
            };

            Stream stream = new MemoryStream(bytes);

            XmlReader reader = XmlReader.Create(stream, xmlReaderSettings);

            XmlSerializer serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceXmlResponseModel));

            // Act
            var result = (CatalogueOfLifeApiServiceXmlResponseModel)serializer.Deserialize(reader);

            stream.Close();
            stream.Dispose();

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel"/> deserialization should work.
        /// </summary>
        [Test(TestOf = typeof(CatalogueOfLifeApiServiceXmlResponseModel), Author = "Bozhin Karaivanov", Description = "CatalogueOfLifeApiServiceXmlResponseModel deserialization should work.")]
        public void CatalogueOfLifeApiServiceXmlResponseModel_Deserialization_ShouldWork()
        {
            // Arrange
            string scientificName = "Coleoptera";
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceXmlResponseModel));
            Stream stream = new FileStream(@"data-files/col-coleoptera-example-response.xml", FileMode.Open);

            // Act
            CatalogueOfLifeApiServiceXmlResponseModel response = serializer.Deserialize(stream) as CatalogueOfLifeApiServiceXmlResponseModel;

            stream.Close();
            stream.Dispose();

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(scientificName, response.Name);

            // Act
            var firstMatchingResult = response.Results.FirstOrDefault(r => r.Name == scientificName);

            // Assert
            Assert.IsNotNull(firstMatchingResult);
            Assert.AreEqual(scientificName, firstMatchingResult.Name);
        }

        /// <summary>
        /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel.Taxon"/> deserialization should work.
        /// </summary>
        [Test(TestOf = typeof(CatalogueOfLifeApiServiceXmlResponseModel.Taxon), Author = "Bozhin Karaivanov", Description = "CatalogueOfLifeApiServiceXmlResponseModel.Taxon deserialization should work.")]
        public void CatalogueOfLifeApiServiceXmlResponseModelTaxon_Deserialization_ShouldWork()
        {
            // Arrange
            string scientificName = "Insecta";
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceXmlResponseModel.Taxon));
            StreamReader reader = new StreamReader(@"data-files/col-classification-taxon.xml");

            // Act
            CatalogueOfLifeApiServiceXmlResponseModel.Taxon response = serializer.Deserialize(reader) as CatalogueOfLifeApiServiceXmlResponseModel.Taxon;

            reader.Close();
            reader.Dispose();

            // Act
            Assert.IsNotNull(response);
            Assert.AreEqual(scientificName, response.Name);
        }

        /// <summary>
        /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel.Reference"/> deserialization should work.
        /// </summary>
        [Test(TestOf = typeof(CatalogueOfLifeApiServiceXmlResponseModel.Reference), Author = "Bozhin Karaivanov", Description = "CatalogueOfLifeApiServiceXmlResponseModel.Reference deserialization should work.")]
        public void CatalogueOfLifeApiServiceXmlResponseModelReference_Deserialization_ShouldWork()
        {
            // Arrange
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceXmlResponseModel.Reference));
            StreamReader reader = new StreamReader(@"data-files/col-reference.xml");

            // Act
            CatalogueOfLifeApiServiceXmlResponseModel.Reference response = serializer.Deserialize(reader) as CatalogueOfLifeApiServiceXmlResponseModel.Reference;

            reader.Close();
            reader.Dispose();

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("H. Lindstr. & Soop", response.Author);
            Assert.AreEqual(1999, response.Year);
        }

        /// <summary>
        /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel.Result"/> deserialization should work.
        /// </summary>
        [Test]
        public void CatalogueOfLifeApiServiceXmlResponseModelResult_Deserialization_ShouldWork()
        {
            // Arrange
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogueOfLifeApiServiceXmlResponseModel.Result));
            StreamReader reader = new StreamReader(@"data-files/col-default-result.xml");

            // Act
            CatalogueOfLifeApiServiceXmlResponseModel.Result response = serializer.Deserialize(reader) as CatalogueOfLifeApiServiceXmlResponseModel.Result;

            reader.Close();
            reader.Dispose();

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Coleoptera", response.Name);
            Assert.AreEqual("Order", response.Rank);

            // Assert Classification
            const int NumberOfClassificationItems = 3;
            Assert.AreEqual(NumberOfClassificationItems, response.Classification.Length);

            Assert.AreEqual("Animalia", response.Classification[0].Name);
            Assert.AreEqual("Kingdom", response.Classification[0].Rank);

            Assert.AreEqual("Arthropoda", response.Classification[1].Name);
            Assert.AreEqual("Phylum", response.Classification[1].Rank);

            Assert.AreEqual("Insecta", response.Classification[2].Name);
            Assert.AreEqual("Class", response.Classification[2].Rank);

            // Assert ChildTaxa
            const int NumberOfChildTaxa = 33;
            Assert.AreEqual(NumberOfChildTaxa, response.ChildTaxa.Length);

            Assert.AreEqual("Amphizoidae", response.ChildTaxa[0].Name);
            Assert.AreEqual("Family", response.ChildTaxa[0].Rank);
            Assert.AreEqual(false, response.ChildTaxa[0].IsExtinct);
        }
    }
}

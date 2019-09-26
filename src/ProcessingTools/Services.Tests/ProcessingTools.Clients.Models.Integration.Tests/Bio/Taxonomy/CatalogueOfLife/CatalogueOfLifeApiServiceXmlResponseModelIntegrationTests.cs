// <copyright file="CatalogueOfLifeApiServiceXmlResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Integration.Tests.Bio.Taxonomy.CatalogueOfLife
{
    using System.IO;
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
        /// <see cref="CatalogueOfLifeApiServiceXmlResponseModel"/> deserialization should work.
        /// </summary>
        [Test(TestOf = typeof(CatalogueOfLifeApiServiceXmlResponseModel), Author = "Bozhin Karaivanov", Description = "CatalogueOfLifeApiServiceXmlResponseModel deserialization should work.")]
        public void CatalogueOfLifeApiServiceXmlResponseModel_Deserialization_ShouldWork()
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

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

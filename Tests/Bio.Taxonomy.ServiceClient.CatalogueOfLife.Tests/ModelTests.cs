namespace ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Tests
{
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class ModelTests
    {
        // TODO
        [TestMethod]
        public void CoLModel_Test_Deserialization()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogOfLifeApiServiceResponseModel));
            CatalogOfLifeApiServiceResponseModel response = null;

            using (var stream = new FileStream(@"DataFiles\\Coleoptera-example-response.xml", FileMode.Open))
            {
                response = serializer.Deserialize(stream) as CatalogOfLifeApiServiceResponseModel;
            }

            const string ScientificName = "Coleoptera";
            Assert.AreEqual(ScientificName, response.Name, "response.Name schould match.");

            var firstMatchingResult = response.Results
                .FirstOrDefault(r => r.Name == ScientificName);

            Assert.AreEqual(ScientificName, firstMatchingResult.Name, "firstMatchingResult.Name schould match.");
        }

        [TestMethod]
        public void ClassificationTaxonModel_Deserialization_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TaxonModel));
            TaxonModel response = null;

            using (var reader = new StreamReader(@"DataFiles\classification-taxon.xml"))
            {
                response = (TaxonModel)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(response, "Deserialized object schould not be null.");
            Assert.AreEqual("Insecta", response.Name, "Name schould match.");
        }

        [TestMethod]
        public void ReferenceModel_Deserialization_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReferenceModel));
            ReferenceModel response = null;

            using (var reader = new StreamReader(@"DataFiles\reference.xml"))
            {
                response = (ReferenceModel)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(response, "Deserialized object schould not be null.");
            Assert.AreEqual("H. Lindstr. & Soop", response.Author, "Author schould match.");
            Assert.AreEqual("1999", response.Year, "Year schould match.");
        }

        [TestMethod]
        public void ResultModel_Deserialization_ShouldWork()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ResultModel));
            ResultModel response = null;

            using (var reader = new StreamReader(@"DataFiles\default-result.xml"))
            {
                response = (ResultModel)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(response, "Deserialized object schould not be null.");
            Assert.AreEqual("Coleoptera", response.Name, "Name schould match.");
            Assert.AreEqual("Order", response.Rank, "Rank schould match.");

            // Classification
            const int NumberOfClassificationItems = 3;
            Assert.AreEqual(NumberOfClassificationItems, response.Classification.Length, $"The number of classifications items schould be {NumberOfClassificationItems}.");

            Assert.AreEqual("Animalia", response.Classification[0].Name, "The name of the first classification item schould match.");
            Assert.AreEqual("Kingdom", response.Classification[0].Rank, "The rank of the first classification item schould match.");

            Assert.AreEqual("Arthropoda", response.Classification[1].Name, "The name of the second classification item schould match.");
            Assert.AreEqual("Phylum", response.Classification[1].Rank, "The rank of the second classification item schould match.");

            Assert.AreEqual("Insecta", response.Classification[2].Name, "The name of the third classification item schould match.");
            Assert.AreEqual("Class", response.Classification[2].Rank, "The rank of the third classification item schould match.");

            // ChildTaxa
            const int NumberOfChildTaxa = 33;
            Assert.AreEqual(NumberOfChildTaxa, response.ChildTaxa.Length, $"The number of child items schould be {NumberOfChildTaxa}.");

            Assert.AreEqual("Amphizoidae", response.ChildTaxa[0].Name, "The name of the first child item schould match.");
            Assert.AreEqual("Family", response.ChildTaxa[0].Rank, "The rank of the first child item schould match.");
            Assert.AreEqual(false, response.ChildTaxa[0].IsExtinct, "IsExtinct property of the first child item schould match.");
        }
    }
}
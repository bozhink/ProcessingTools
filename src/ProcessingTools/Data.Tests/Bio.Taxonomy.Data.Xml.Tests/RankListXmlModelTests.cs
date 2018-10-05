namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Tests
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Bio.Taxonomy.Xml;

    [TestClass]
    public class RankListXmlModelTests
    {
        [TestMethod]
        public void RankListXmlModel_Deserialize_ShouldWork()
        {
            const int NumberOfListItems = 5;
            string directoryFileName = AppSettings.DataFilesDirectoryName;
            string fileName = AppSettings.RankListSampleFileName;

            RankListXmlModel list = null;
            using (var stream = new FileStream($"{directoryFileName}/{fileName}", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RankListXmlModel));
                list = serializer.Deserialize(stream) as RankListXmlModel;
            }

            Assert.IsNotNull(list, "RankList object should not be null.");

            Assert.IsTrue(list.Taxa.Length > 0, "Number of items should be greater than zero.");

            Assert.AreEqual(NumberOfListItems, list.Taxa.Length, $"The number of RankList items should be {NumberOfListItems}.");

            int taxonNumber = 0;
            Assert.AreEqual("Culicidae", list.Taxa[taxonNumber].Parts[0].Value, "First taxon name should match.");
            Assert.AreEqual("family", list.Taxa[taxonNumber].Parts[0].Ranks.Values[0], "First taxon rank should match.");
            Assert.AreEqual(false, list.Taxa[taxonNumber].IsWhiteListed, "First taxon should not be white-listed.");

            taxonNumber = 1;
            Assert.AreEqual("Chilophiurina", list.Taxa[taxonNumber].Parts[0].Value, "Second taxon name should match.");
            Assert.AreEqual("suborder", list.Taxa[taxonNumber].Parts[0].Ranks.Values[0], "Second taxon rank should match.");
            Assert.AreEqual(false, list.Taxa[taxonNumber].IsWhiteListed, "Second taxon should not be white-listed.");

            taxonNumber = 2;
            Assert.AreEqual("Aves", list.Taxa[taxonNumber].Parts[0].Value, "Third taxon name should match.");
            Assert.AreEqual("class", list.Taxa[taxonNumber].Parts[0].Ranks.Values[0], "Third taxon rank should match.");
            Assert.AreEqual(true, list.Taxa[taxonNumber].IsWhiteListed, "Third taxon should not be white-listed.");

            taxonNumber = 3;
            Assert.AreEqual("Phytomastigophora", list.Taxa[taxonNumber].Parts[0].Value, "Fourth taxon name should match.");
            Assert.AreEqual("class", list.Taxa[taxonNumber].Parts[0].Ranks.Values[0], "Fourth taxon rank should match.");
            Assert.AreEqual(true, list.Taxa[taxonNumber].IsWhiteListed, "Fourth taxon should not be white-listed.");

            taxonNumber = 4;
            Assert.AreEqual("Malthinini", list.Taxa[taxonNumber].Parts[0].Value, "Fifth taxon name should match.");
            Assert.AreEqual("tribe", list.Taxa[taxonNumber].Parts[0].Ranks.Values[0], "Fifth taxon rank should match.");
            Assert.AreEqual(false, list.Taxa[taxonNumber].IsWhiteListed, "Fifth taxon should not be white-listed.");
        }

        [TestMethod]
        public void RankListXmlModel_SerializeObjectWithSingleRankValue_ShouldWork()
        {
            const string TaxonName = "Aves";
            const string ClassTaxonRank = "class";

            var ranks = new TaxonRankXmlModel
            {
                Values = new[]
                {
                    ClassTaxonRank
                }
            };

            var taxonPart = new TaxonPartXmlModel
            {
                Value = TaxonName,
                Ranks = ranks
            };

            var taxon = new TaxonXmlModel
            {
                IsWhiteListed = true,
                Parts = new[]
                {
                    taxonPart
                }
            };

            var list = new RankListXmlModel
            {
                Taxa = new[]
                {
                    taxon
                }
            };

            Assert.IsNotNull(list, "List object should not be null.");

            XmlDocument document = null;
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RankListXmlModel));
                serializer.Serialize(stream, list);

                stream.Position = 0;
                document = new XmlDocument();
                document.Load(stream);
            }

            Assert.IsNotNull(document, "XmlDocument should not be null");

            var root = document.DocumentElement;
            Assert.AreEqual(
                XmlModelsConstants.RankListXmlRootNodeName,
                root.Name,
                $"Document root node name should be '{XmlModelsConstants.RankListXmlRootNodeName}'.");

            Assert.AreEqual(1, root.ChildNodes.Count, "Number of child nodes should be 1.");

            var xmlTaxon = root.FirstChild;
            Assert.IsNotNull(xmlTaxon, "First child node should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlModelElementName,
                xmlTaxon.Name,
                $"Taxon element name should be {XmlModelsConstants.RankListTaxonXmlModelElementName}");

            Assert.AreEqual(1, xmlTaxon.Attributes.Count, "Taxon element should have 1 attribute.");

            var whiteListedXmlAttribute = xmlTaxon.Attributes[0];
            Assert.IsNotNull(whiteListedXmlAttribute, "White-listed attribute of the taxon element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListIsWhiteListedXmlAttributeName,
                whiteListedXmlAttribute.Name,
                $"White-listed attribute of the taxon element should have name '{XmlModelsConstants.RankListIsWhiteListedXmlAttributeName}'");
            Assert.AreEqual("true", whiteListedXmlAttribute.InnerText, "White-listed attribute of the taxon element should have value true.");

            Assert.AreEqual(1, xmlTaxon.ChildNodes.Count, "Taxon element should have 1 child node.");

            var xmlTaxonPart = xmlTaxon.FirstChild;
            Assert.IsNotNull(xmlTaxonPart, "Taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartElementName,
                xmlTaxonPart.Name,
                $"Taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartElementName}'.");

            Assert.AreEqual(2, xmlTaxonPart.ChildNodes.Count, "Taxon part element should have 2 child noded.");

            var xmlTaxonPartValue = xmlTaxonPart.FirstChild;
            Assert.IsNotNull(xmlTaxonPartValue, "Value element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartValueElementName,
                xmlTaxonPartValue.Name,
                $"Value element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartValueElementName}'.");
            Assert.AreEqual(TaxonName, xmlTaxonPartValue.InnerText, $"Value element of the taxon part element should have name value '{TaxonName}'.");

            var xmlTaxonPartRanks = xmlTaxonPart.LastChild;
            Assert.IsNotNull(xmlTaxonPartRanks, "Ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankElementName,
                xmlTaxonPartRanks.Name,
                $"Ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankElementName}'.");

            Assert.AreEqual(1, xmlTaxonPartRanks.ChildNodes.Count, "Ranks element of the taxon part element should have 1 child node.");

            var xmlTaxonPartRankValue = xmlTaxonPartRanks.FirstChild;
            Assert.IsNotNull(xmlTaxonPartRankValue, "First child of the ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankValueElementName,
                xmlTaxonPartRankValue.Name,
                $"First child of the ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankValueElementName}'.");
            Assert.AreEqual(ClassTaxonRank, xmlTaxonPartRankValue.InnerText, $"First child of the ranks element of the taxon part element should have value '{ClassTaxonRank}'.");
        }

        [TestMethod]
        public void RankListXmlModel_SerializeObjectWithTwoRankValues_ShouldWork()
        {
            const string Aves = "Aves";
            const string ClassTaxonRank1 = "class";
            const string ClassTaxonRank2 = "classis";

            var ranks = new TaxonRankXmlModel
            {
                Values = new[]
                {
                    ClassTaxonRank1,
                    ClassTaxonRank2
                }
            };

            var taxonPart = new TaxonPartXmlModel
            {
                Value = Aves,
                Ranks = ranks
            };

            var taxon = new TaxonXmlModel
            {
                IsWhiteListed = true,
                Parts = new[]
                {
                    taxonPart
                }
            };

            var list = new RankListXmlModel
            {
                Taxa = new[]
                {
                    taxon
                }
            };

            Assert.IsNotNull(list, "List object should not be null.");

            XmlDocument document = null;
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RankListXmlModel));
                serializer.Serialize(stream, list);

                stream.Position = 0;
                document = new XmlDocument();
                document.Load(stream);
            }

            Assert.IsNotNull(document, "XmlDocument should not be null");

            var root = document.DocumentElement;
            Assert.AreEqual(
                XmlModelsConstants.RankListXmlRootNodeName,
                root.Name,
                $"Document root node name should be '{XmlModelsConstants.RankListXmlRootNodeName}'.");

            Assert.AreEqual(1, root.ChildNodes.Count, "Number of child nodes should be 1.");

            var xmlTaxon = root.FirstChild;
            Assert.IsNotNull(xmlTaxon, "First child node should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlModelElementName,
                xmlTaxon.Name,
                $"Taxon element name should be {XmlModelsConstants.RankListTaxonXmlModelElementName}");

            Assert.AreEqual(1, xmlTaxon.Attributes.Count, "Taxon element should have 1 attribute.");

            var whiteListedXmlAttribute = xmlTaxon.Attributes[0];
            Assert.IsNotNull(whiteListedXmlAttribute, "White-listed attribute of the taxon element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListIsWhiteListedXmlAttributeName,
                whiteListedXmlAttribute.Name,
                $"White-listed attribute of the taxon element should have name '{XmlModelsConstants.RankListIsWhiteListedXmlAttributeName}'");
            Assert.AreEqual("true", whiteListedXmlAttribute.InnerText, "White-listed attribute of the taxon element should have value true.");

            Assert.AreEqual(1, xmlTaxon.ChildNodes.Count, "Taxon element should have 1 child node.");

            var xmlTaxonPart = xmlTaxon.FirstChild;
            Assert.IsNotNull(xmlTaxonPart, "Taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartElementName,
                xmlTaxonPart.Name,
                $"Taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartElementName}'.");

            Assert.AreEqual(2, xmlTaxonPart.ChildNodes.Count, "Taxon part element should have 2 child noded.");

            var xmlTaxonPartValue = xmlTaxonPart.FirstChild;
            Assert.IsNotNull(xmlTaxonPartValue, "Value element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartValueElementName,
                xmlTaxonPartValue.Name,
                $"Value element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartValueElementName}'.");
            Assert.AreEqual(Aves, xmlTaxonPartValue.InnerText, $"Value element of the taxon part element should have name value '{Aves}'.");

            var xmlTaxonPartRanks = xmlTaxonPart.LastChild;
            Assert.IsNotNull(xmlTaxonPartRanks, "Ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankElementName,
                xmlTaxonPartRanks.Name,
                $"Ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankElementName}'.");

            Assert.AreEqual(2, xmlTaxonPartRanks.ChildNodes.Count, "Ranks element of the taxon part element should have 2 child node.");

            var xmlTaxonPartRankFirstValue = xmlTaxonPartRanks.FirstChild;
            Assert.IsNotNull(xmlTaxonPartRankFirstValue, "First child of the ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankValueElementName,
                xmlTaxonPartRankFirstValue.Name,
                $"First child of the ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankValueElementName}'.");
            Assert.AreEqual(ClassTaxonRank1, xmlTaxonPartRankFirstValue.InnerText, $"First child of the ranks element of the taxon part element should have value '{ClassTaxonRank1}'.");

            var xmlTaxonPartRankLastValue = xmlTaxonPartRanks.LastChild;
            Assert.IsNotNull(xmlTaxonPartRankLastValue, "Last child of the ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankValueElementName,
                xmlTaxonPartRankLastValue.Name,
                $"Last child of the ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankValueElementName}'.");
            Assert.AreEqual(ClassTaxonRank2, xmlTaxonPartRankLastValue.InnerText, $"Last child of the ranks element of the taxon part element should have value '{ClassTaxonRank2}'.");
        }

        [TestMethod]
        public void RankListXmlModel_SerializeObjectWithSingleRankValue_WithNotSetWhiteListedAttribute_ShouldSetDefaultValueFalse()
        {
            const string TaxonName = "Malthinini";
            const string ClassTaxonRank = "tribe";

            var ranks = new TaxonRankXmlModel
            {
                Values = new[]
                {
                    ClassTaxonRank
                }
            };

            var taxonPart = new TaxonPartXmlModel
            {
                Value = TaxonName,
                Ranks = ranks
            };

            var taxon = new TaxonXmlModel
            {
                Parts = new[]
                {
                    taxonPart
                }
            };

            var list = new RankListXmlModel
            {
                Taxa = new[]
                {
                    taxon
                }
            };

            Assert.IsNotNull(list, "List object should not be null.");

            XmlDocument document = null;
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RankListXmlModel));
                serializer.Serialize(stream, list);

                stream.Position = 0;
                document = new XmlDocument();
                document.Load(stream);
            }

            Assert.IsNotNull(document, "XmlDocument should not be null");

            var root = document.DocumentElement;
            Assert.AreEqual(
                XmlModelsConstants.RankListXmlRootNodeName,
                root.Name,
                $"Document root node name should be '{XmlModelsConstants.RankListXmlRootNodeName}'.");

            Assert.AreEqual(1, root.ChildNodes.Count, "Number of child nodes should be 1.");

            var xmlTaxon = root.FirstChild;
            Assert.IsNotNull(xmlTaxon, "First child node should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlModelElementName,
                xmlTaxon.Name,
                $"Taxon element name should be {XmlModelsConstants.RankListTaxonXmlModelElementName}");

            Assert.AreEqual(1, xmlTaxon.Attributes.Count, "Taxon element should have 1 attribute.");

            var whiteListedXmlAttribute = xmlTaxon.Attributes[0];
            Assert.IsNotNull(whiteListedXmlAttribute, "White-listed attribute of the taxon element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListIsWhiteListedXmlAttributeName,
                whiteListedXmlAttribute.Name,
                $"White-listed attribute of the taxon element should have name '{XmlModelsConstants.RankListIsWhiteListedXmlAttributeName}'");
            Assert.AreEqual("false", whiteListedXmlAttribute.InnerText, "White-listed attribute of the taxon element should have value true.");

            Assert.AreEqual(1, xmlTaxon.ChildNodes.Count, "Taxon element should have 1 child node.");

            var xmlTaxonPart = xmlTaxon.FirstChild;
            Assert.IsNotNull(xmlTaxonPart, "Taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartElementName,
                xmlTaxonPart.Name,
                $"Taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartElementName}'.");

            Assert.AreEqual(2, xmlTaxonPart.ChildNodes.Count, "Taxon part element should have 2 child noded.");

            var xmlTaxonPartValue = xmlTaxonPart.FirstChild;
            Assert.IsNotNull(xmlTaxonPartValue, "Value element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartValueElementName,
                xmlTaxonPartValue.Name,
                $"Value element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartValueElementName}'.");
            Assert.AreEqual(TaxonName, xmlTaxonPartValue.InnerText, $"Value element of the taxon part element should have name value '{TaxonName}'.");

            var xmlTaxonPartRanks = xmlTaxonPart.LastChild;
            Assert.IsNotNull(xmlTaxonPartRanks, "Ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankElementName,
                xmlTaxonPartRanks.Name,
                $"Ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankElementName}'.");

            Assert.AreEqual(1, xmlTaxonPartRanks.ChildNodes.Count, "Ranks element of the taxon part element should have 1 child node.");

            var xmlTaxonPartRankValue = xmlTaxonPartRanks.FirstChild;
            Assert.IsNotNull(xmlTaxonPartRankValue, "First child of the ranks element of the taxon part element should not be null.");
            Assert.AreEqual(
                XmlModelsConstants.RankListTaxonXmlPartRankValueElementName,
                xmlTaxonPartRankValue.Name,
                $"First child of the ranks element of the taxon part element should have name '{XmlModelsConstants.RankListTaxonXmlPartRankValueElementName}'.");
            Assert.AreEqual(ClassTaxonRank, xmlTaxonPartRankValue.InnerText, $"First child of the ranks element of the taxon part element should have value '{ClassTaxonRank}'.");
        }
    }
}

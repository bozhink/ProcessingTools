namespace ProcessingTools.Processors.Tests.Integration.Tests.Models.Bio.Taxonomy
{
    using System.Xml;
    using NUnit.Framework;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Models.Bio.Taxonomy;

    [TestFixture]
    public class TaxonNamePartTests
    {
        [TestCase(
            @"<tn-part type=""genus"" full-name=""Zospeum"">Zospeum</tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "Zospeum",
            "",
            false)]
        [TestCase(
            @"<tn-part type=""genus"" full-name=""Zospeum"">Z.</tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "Z.",
            "",
            true)]
        [TestCase(
            @"<tn-part type=""genus"">Zospeum</tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "Zospeum",
            "",
            false)]
        [TestCase(
            @"<tn-part type=""genus"">Z.</tn-part>",
            SpeciesPartType.Genus,
            "",
            "Z.",
            "",
            true)]
        [TestCase(
            @"<tn-part type=""genus""></tn-part>",
            SpeciesPartType.Genus,
            "",
            "",
            "",
            true)]
        [TestCase(
            @"<tn-part></tn-part>",
            SpeciesPartType.Undefined,
            "",
            "",
            "",
            true)]
        [TestCase(
            @"<tn-part type=""genus"" full-name=""Zospeum""></tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "",
            "",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""genus"" full-name=""Zospeum"">Zospeum</tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "Zospeum",
            "TN1",
            false)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""genus"" full-name=""Zospeum"">Z.</tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "Z.",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""genus"">Zospeum</tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "Zospeum",
            "TN1",
            false)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""genus"">Z.</tn-part>",
            SpeciesPartType.Genus,
            "",
            "Z.",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""genus""></tn-part>",
            SpeciesPartType.Genus,
            "",
            "",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1""></tn-part>",
            SpeciesPartType.Undefined,
            "",
            "",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""genus"" full-name=""Zospeum""></tn-part>",
            SpeciesPartType.Genus,
            "Zospeum",
            "",
            "TN1",
            true)]
        public void TaxonNamePart_WithValidNodeParameterInConstructor_ShouldReturnValidObject(string nodeString, SpeciesPartType taxonRank, string fullName, string taxonName, string id, bool abbreviated)
        {
            // Arrange
            var document = new XmlDocument();
            document.LoadXml(nodeString);

            // Act
            var taxonNamePart = new TaxonNamePart(document.DocumentElement);

            // Assert
            Assert.IsNotNull(taxonNamePart, "Object should not be null.");

            Assert.AreEqual(abbreviated, taxonNamePart.IsAbbreviated, "Taxon name part should " + (abbreviated ? "not " : string.Empty) + "be abbreviated.");
            Assert.AreEqual(taxonRank, taxonNamePart.Rank, $"Rank should be '{taxonRank}'.");
            Assert.AreEqual(taxonName, taxonNamePart.Name, $"Name should be '{taxonName}'.");
            Assert.AreEqual(fullName, taxonNamePart.FullName, $"Full Name should be '{fullName}'.");
            Assert.AreEqual(id, taxonNamePart.Id, $"Id should be '{id}'.");
        }
    }
}

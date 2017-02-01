namespace ProcessingTools.Processors.Tests.Integration.Tests.Models.Bio.Taxonomy
{
    using System.Xml;
    using NUnit.Framework;
    using ProcessingTools.Processors.Models.Bio.Taxonomy.Parsers;

    [TestFixture]
    public class TaxonNamePartTests
    {
        [TestCase(
            @"<tn-part type=""order"" full-name=""Coleoptera"">Coleoptera</tn-part>",
            "order",
            "Coleoptera",
            "Coleoptera",
            "",
            false)]
        [TestCase(
            @"<tn-part type=""order"" full-name=""Coleoptera"">C.</tn-part>",
            "order",
            "Coleoptera",
            "C.",
            "",
            true)]
        [TestCase(
            @"<tn-part type=""order"">Coleoptera</tn-part>",
            "order",
            "Coleoptera",
            "Coleoptera",
            "",
            false)]
        [TestCase(
            @"<tn-part type=""order"">C.</tn-part>",
            "order",
            "",
            "C.",
            "",
            true)]
        [TestCase(
            @"<tn-part type=""order""></tn-part>",
            "order",
            "",
            "",
            "",
            true)]
        [TestCase(
            @"<tn-part></tn-part>",
            "",
            "",
            "",
            "",
            true)]
        [TestCase(
            @"<tn-part type=""order"" full-name=""Coleoptera""></tn-part>",
            "order",
            "Coleoptera",
            "",
            "",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""order"" full-name=""Coleoptera"">Coleoptera</tn-part>",
            "order",
            "Coleoptera",
            "Coleoptera",
            "TN1",
            false)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""order"" full-name=""Coleoptera"">C.</tn-part>",
            "order",
            "Coleoptera",
            "C.",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""order"">Coleoptera</tn-part>",
            "order",
            "Coleoptera",
            "Coleoptera",
            "TN1",
            false)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""order"">C.</tn-part>",
            "order",
            "",
            "C.",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""order""></tn-part>",
            "order",
            "",
            "",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1""></tn-part>",
            "",
            "",
            "",
            "TN1",
            true)]
        [TestCase(
            @"<tn-part id=""TN1"" type=""order"" full-name=""Coleoptera""></tn-part>",
            "order",
            "Coleoptera",
            "",
            "TN1",
            true)]
        public void TaxonNamePart_WithValidNodeParameterInConstructor_ShouldReturnValidObject(string nodeString, string taxonRank, string fullName, string taxonName, string id, bool abbreviated)
        {
            var document = new XmlDocument();
            document.LoadXml(nodeString);

            var taxonNamePart = new TaxonNamePart(document.DocumentElement);
            Assert.IsNotNull(taxonNamePart, "Object should not be null.");

            Assert.AreEqual(abbreviated, taxonNamePart.IsAbbreviated, "Taxon name part should " + (abbreviated ? "not " : string.Empty) + "be abbreviated.");
            Assert.AreEqual(taxonRank, taxonNamePart.Rank, $"Rank should be '{taxonRank}'.");
            Assert.AreEqual(taxonName, taxonNamePart.Name, $"Name should be '{taxonName}'.");
            Assert.AreEqual(fullName, taxonNamePart.FullName, $"Full Name should be '{fullName}'.");
            Assert.AreEqual(id, taxonNamePart.Id, $"Id should be '{id}'.");
        }
    }
}

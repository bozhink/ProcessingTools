// <copyright file="TaxonNamePartTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Tests.Integration.Tests
{
    using System.Xml;
    using NUnit.Framework;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Processors.Models.Bio.Taxonomy;

    /// <summary>
    /// <see cref="TaxonNamePart"/> tests.
    /// </summary>
    [TestFixture]
    public class TaxonNamePartTests
    {
        /// <summary>
        /// <see cref="TaxonNamePart"/> with valid node parameter in constructor should return valid object.
        /// </summary>
        /// <param name="nodeString">XML node as string.</param>
        /// <param name="taxonRank">Taxon rank.</param>
        /// <param name="fullName">Full name of the taxon part.</param>
        /// <param name="taxonName">Display name of the taxon part.</param>
        /// <param name="id">ID of the taxon part.</param>
        /// <param name="abbreviated">Is taxon part abbreviated or not.</param>
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

// <copyright file="GbifApiV09ResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Models.Tests
{
    using System.IO;
    using System.Text.Json;
    using NUnit.Framework;

    /// <summary>
    /// <see cref="GbifApiV09ResponseModel"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(GbifApiV09ResponseModel))]
    public class GbifApiV09ResponseModelIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifApiV09ResponseModel"/> deserialization should return valid object.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09ResponseModel), Description = "GbifApiV09ResponseModel deserialization should return valid object.")]
        public void GbifApiV09ResponseModel_Deserialization_ShouldReturnValidObject()
        {
            // Arrange
            string fileName = @"data-files/gbif-v09-coleoptera-response.json";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));

            // Act
            var model = JsonSerializer.Deserialize<GbifApiV09ResponseModel>(fileContent);

            // Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// <see cref="GbifApiV09ResponseModel"/> deserialization should correctly deserialize data.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09ResponseModel), Description = "GbifApiV09ResponseModel deserialization should correctly deserialize data.")]
        public void GbifApiV09ResponseModel_Deserialization_ShouldCorrectlyDeserializeData()
        {
            // Arrange
            string fileName = @"data-files/gbif-v09-coleoptera-response.json";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));

            // Act
            var model = JsonSerializer.Deserialize<GbifApiV09ResponseModel>(fileContent);

            // Assert
            Assert.AreEqual("Coleoptera", model.CanonicalName);
            Assert.AreEqual("ORDER", model.Rank.ToUpperInvariant());
        }
    }
}

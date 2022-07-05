// <copyright file="GbifApiV09ResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Integrations.Gbif.IntegrationModels.Tests
{
    using System.IO;
    using System.Text.Json;
    using NUnit.Framework;
    using ProcessingTools.Integrations.Gbif.IntegrationModels.V09;

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
            GbifApiV09ResponseModel? model = JsonSerializer.Deserialize<GbifApiV09ResponseModel>(fileContent);

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
            GbifApiV09ResponseModel? model = JsonSerializer.Deserialize<GbifApiV09ResponseModel>(fileContent);

            // Assert
            StringAssert.AreEqualIgnoringCase("Coleoptera", model?.CanonicalName);
            StringAssert.AreEqualIgnoringCase("order", model?.Rank);
        }
    }
}

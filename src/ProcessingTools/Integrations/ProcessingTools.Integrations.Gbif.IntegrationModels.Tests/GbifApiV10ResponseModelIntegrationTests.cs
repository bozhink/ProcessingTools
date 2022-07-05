// <copyright file="GbifApiV10ResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Integrations.Gbif.IntegrationModels.Tests
{
    using System.IO;
    using System.Text.Json;
    using NUnit.Framework;
    using ProcessingTools.Integrations.Gbif.IntegrationModels.V10;

    /// <summary>
    /// <see cref="GbifApiV10ResponseModel"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(GbifApiV10ResponseModel))]
    public class GbifApiV10ResponseModelIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifApiV10ResponseModel"/> deserialization should return valid object.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV10ResponseModel), Description = "GbifApiV10ResponseModel deserialization should return valid object.")]
        public void GbifApiV10ResponseModel_Deserialization_ShouldReturnValidObject()
        {
            // Arrange
            string fileName = @"data-files/gbif-v1-response.json";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));

            // Act
            GbifApiV10ResponseModel? model = JsonSerializer.Deserialize<GbifApiV10ResponseModel>(fileContent);

            // Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// <see cref="GbifApiV10ResponseModel"/> deserialization should correctly deserialize data.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV10ResponseModel), Description = "GbifApiV10ResponseModel deserialization should correctly deserialize data.")]
        public void GbifApiV10ResponseModel_Deserialization_ShouldCorrectlyDeserializeData()
        {
            // Arrange
            string fileName = @"data-files/gbif-v1-response.json";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));

            // Act
            GbifApiV10ResponseModel? model = JsonSerializer.Deserialize<GbifApiV10ResponseModel>(fileContent);

            // Assert
            Assert.That(model?.Offset, Is.EqualTo(0));
            Assert.That(model?.Limit, Is.EqualTo(20));
        }
    }
}

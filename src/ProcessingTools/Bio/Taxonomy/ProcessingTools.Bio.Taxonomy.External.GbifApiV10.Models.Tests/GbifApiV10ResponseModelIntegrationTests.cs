// <copyright file="GbifApiV10ResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Models.Tests
{
    using System.IO;
    using System.Text.Json;
    using NUnit.Framework;

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
            var model = JsonSerializer.Deserialize<GbifApiV10ResponseModel>(fileContent);

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
            var model = JsonSerializer.Deserialize<GbifApiV10ResponseModel>(fileContent);

            // Assert
            Assert.AreEqual(0, model.Offset);
            Assert.AreEqual(20, model.Limit);
        }
    }
}

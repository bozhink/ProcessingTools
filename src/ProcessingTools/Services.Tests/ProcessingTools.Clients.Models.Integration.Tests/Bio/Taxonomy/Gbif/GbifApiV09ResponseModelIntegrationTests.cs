// <copyright file="GbifApiV09ResponseModelIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Integration.Tests.Bio.Taxonomy.Gbif
{
    using System.IO;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;

    /// <summary>
    /// <see cref="GbifApiV09ResponseModel"/> integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(GbifApiV09ResponseModel), Author = "Bozhin Karaivanov")]
    public class GbifApiV09ResponseModelIntegrationTests
    {
        /// <summary>
        /// <see cref="GbifApiV09ResponseModel"/> deserialization with <see cref="Newtonsoft.Json"/> should return valid object.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09ResponseModel), Author = "Bozhin Karaivanov", Description = "GbifApiV09ResponseModel deserialization with Newtonsoft.Json should return valid object.")]
        public void GbifApiV09ResponseModel_DeserializationWithNewtonsoftJson_ShouldReturnValidObject()
        {
            // Arrange
            string fileName = @"data-files/gbif-v09-coleoptera-response.json";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));

            // Act
            var model = JsonConvert.DeserializeObject<GbifApiV09ResponseModel>(fileContent);

            // Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// <see cref="GbifApiV09ResponseModel"/> deserialization with <see cref="Newtonsoft.Json"/> should correctly deserialize data.
        /// </summary>
        [Test(TestOf = typeof(GbifApiV09ResponseModel), Author = "Bozhin Karaivanov", Description = "GbifApiV09ResponseModel deserialization with Newtonsoft.Json should correctly deserialize data.")]
        public void GbifApiV09ResponseModel_DeserializationWithNewtonsoftJson_ShouldCorrectlyDeserializeData()
        {
            // Arrange
            string fileName = @"data-files/gbif-v09-coleoptera-response.json";
            string fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));

            // Act
            var model = JsonConvert.DeserializeObject<GbifApiV09ResponseModel>(fileContent);

            // Assert
            Assert.AreEqual("Coleoptera", model.CanonicalName);
            Assert.AreEqual("ORDER", model.Rank.ToUpperInvariant());
        }
    }
}

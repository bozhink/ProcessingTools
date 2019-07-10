// <copyright file="CsvSeedModelTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Integration.Tests.Bio.Biorepositories
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Data.Seed.Bio.Biorepositories;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// CSV seed model tests.
    /// </summary>
    [TestClass]
    public class CsvSeedModelTests
    {
        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// CSV seed files deserialization should work.
        /// </summary>
        [TestMethod]
        public void CsvSeedFiles_Deserialization_ShouldWork()
        {
            var dataFilesDirectoryPath = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "DataFiles", "grbio");

            var modelTypes = typeof(BiorepositoriesDataSeeder).Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsClass && Attribute.IsDefined(t, typeof(FileNameAttribute)) && Attribute.IsDefined(t, typeof(CsvObjectAttribute)));

            foreach (var modelType in modelTypes)
            {
                this.TestContext.WriteLine(modelType.FullName);

                var fileNameAttribute = modelType.GetCustomAttributes(typeof(FileNameAttribute), false).FirstOrDefault() as FileNameAttribute;

                string fileName = $"{dataFilesDirectoryPath}/{fileNameAttribute.Name}";
                Assert.IsTrue(File.Exists(fileName), $"FileName ‘{fileName}’ should be valid.");

                string csvText = File.ReadAllText(fileName);
                Assert.IsFalse(string.IsNullOrWhiteSpace(csvText), $"Text of ‘{fileName}’ should not be null or whitespace.");

                var serializer = new CsvSerializer();
                var items = serializer.Deserialize(modelType, csvText)?.ToList();

                Assert.IsNotNull(items, "Deserialized items should not be null.");
                Assert.IsTrue(items?.Count > 0, "Number of deserialized items should be greater than 0.");
            }
        }
    }
}

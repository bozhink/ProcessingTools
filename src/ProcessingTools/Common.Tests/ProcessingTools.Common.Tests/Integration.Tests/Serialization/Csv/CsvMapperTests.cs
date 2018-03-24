// <copyright file="CsvMapperTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Tests.Integration.Tests.Serialization.Csv
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Serialization.Csv;
    using ProcessingTools.Common.Tests.Integration.Tests.Serialization.Csv.Models;

    /// <summary>
    /// CSV Mapper Tests.
    /// </summary>
    [TestClass]
    public class CsvMapperTests
    {
        /// <summary>
        /// Type: Map One Row CSV To Object Should Work.
        /// </summary>
        [TestMethod]
        public void CsvMapper_MapOneRowCsvToObject_Type_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

            var csv = new CsvTableReader();
            var csvRow = csv.ReadToTable(CsvText).Skip(1).ToArray()[0];

            var mapping = new ColumnIndexToPropertyNameMapping
            {
                Mapping = new Dictionary<string, int>
                {
                    { "Name", 0 },
                    { "Year", 1 },
                    { "Description", 2 }
                }
            };

            var result = csvRow.MapToObjectProperties(typeof(NameYearDescriptionSampleObject), mapping) as NameYearDescriptionSampleObject;

            Assert.AreEqual("John Smith", result.Name, "Name should match.");
            Assert.AreEqual(2015, result.Year, "Year should match.");
            Assert.AreEqual("No desription here", result.Description, "Description should match.");
        }

        /// <summary>
        /// Generic: Map One Row CSV To Object Should Work.
        /// </summary>
        [TestMethod]
        public void CsvMapper_MapOneRowCsvToObject_Generic_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

            var csv = new CsvTableReader();
            var csvRow = csv.ReadToTable(CsvText).Skip(1).ToArray()[0];

            var mapping = new ColumnIndexToPropertyNameMapping
            {
                Mapping = new Dictionary<string, int>
                {
                    { "Name", 0 },
                    { "Year", 1 },
                    { "Description", 2 }
                }
            };

            var result = csvRow.MapToObjectProperties<NameYearDescriptionSampleObject>(mapping);

            Assert.AreEqual("John Smith", result.Name, "Name should match.");
            Assert.AreEqual(2015, result.Year, "Year should match.");
            Assert.AreEqual("No desription here", result.Description, "Description should match.");
        }
    }
}

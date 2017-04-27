namespace ProcessingTools.Serialization.Tests.Csv
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    using ProcessingTools.Serialization.Csv;

    [TestClass]
    public class CsvMapperTests
    {
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
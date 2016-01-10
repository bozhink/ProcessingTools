namespace ProcessingTools.Csv.Serialization.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class CsvMapperTests
    {
        [TestMethod]
        public void CsvMapper_MapOneRowCsvToObject_Type_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

            var csv = new CsvTableObject();
            var csvRow = csv.ReadToTable(CsvText).Skip(1).ToArray()[0];

            var csvMapper = new CsvMapper();
            var mapping = new CsvToObjectMapping
            {
                Mapping = new Dictionary<string, int>
                {
                    { "Name", 0 },
                    { "Year", 1 },
                    { "Description", 2 }
                }
            };

            var result = csvMapper.MapCsvRowToObject(typeof(NameYearDescriptionSampleObject), csvRow, mapping) as NameYearDescriptionSampleObject;

            Assert.AreEqual("John Smith", result.Name, "Name should match.");
            Assert.AreEqual(2015, result.Year, "Year should match.");
            Assert.AreEqual("No desription here", result.Description, "Description should match.");
        }

        [TestMethod]
        public void CsvMapper_MapOneRowCsvToObject_Generic_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

            var csv = new CsvTableObject();
            var csvRow = csv.ReadToTable(CsvText).Skip(1).ToArray()[0];

            var csvMapper = new CsvMapper();
            var mapping = new CsvToObjectMapping
            {
                Mapping = new Dictionary<string, int>
                {
                    { "Name", 0 },
                    { "Year", 1 },
                    { "Description", 2 }
                }
            };

            var result = csvMapper.MapCsvRowToObject<NameYearDescriptionSampleObject>(csvRow, mapping);

            Assert.AreEqual("John Smith", result.Name, "Name should match.");
            Assert.AreEqual(2015, result.Year, "Year should match.");
            Assert.AreEqual("No desription here", result.Description, "Description should match.");
        }
    }
}
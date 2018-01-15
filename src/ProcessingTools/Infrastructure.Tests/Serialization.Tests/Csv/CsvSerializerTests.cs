namespace ProcessingTools.Serialization.Tests.Csv
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Serialization.Csv;
    using ProcessingTools.Serialization.Tests.Csv.Models;

    [TestClass]
    public class CsvSerializerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CsvSerializer_DeserializeType_AppliedOnInvalidObject_ShouldThrow()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here\nJane Smith,2016,Desription here!";

            var serializer = new CsvSerializer();
            serializer.Deserialize(typeof(NameYearDescriptionSampleObject), CsvText);
        }

        [TestMethod]
        public void CsvSerializer_DeserializeType_AppliedOnValidObject_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here\nJane Smith,2016,Desription here!";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize(typeof(NameYearDescriptionSampleCsvObject), CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects schould be 2.");

            var first = result[0] as NameYearDescriptionSampleCsvObject;
            Assert.AreEqual("John Smith", first.Name, "Name of the first object should be John Smith.");
            Assert.AreEqual(2015, first.Year, "Year of the first object schould be 2015.");
            Assert.AreEqual("No desription here", first.Description, "Description of the first object does not match.");

            var second = result[1] as NameYearDescriptionSampleCsvObject;
            Assert.AreEqual("Jane Smith", second.Name, "Name of the second object should be Jane Smith.");
            Assert.AreEqual(2016, second.Year, "Year of the second object schould be 2016.");
            Assert.AreEqual("Desription here!", second.Description, "Description of the second object does not match.");
        }

        [TestMethod]
        public void CsvSerializer_DeserializeT_AppliedOnValidObject_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here\nJane Smith,2016,Desription here!";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize<NameYearDescriptionSampleCsvObject>(CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects schould be 2.");

            var first = result[0];
            Assert.AreEqual("John Smith", first.Name, "Name of the first object should be John Smith.");
            Assert.AreEqual(2015, first.Year, "Year of the first object schould be 2015.");
            Assert.AreEqual("No desription here", first.Description, "Description of the first object does not match.");

            var second = result[1];
            Assert.AreEqual("Jane Smith", second.Name, "Name of the second object should be Jane Smith.");
            Assert.AreEqual(2016, second.Year, "Year of the second object schould be 2016.");
            Assert.AreEqual("Desription here!", second.Description, "Description of the second object does not match.");
        }

        [TestMethod]
        public void CsvSerializer_DeserializeType_AppliedOnValidObjectWithCustomNamedColumns_ShouldWork()
        {
            const string CsvText = "first name,last name\nJohn,Smith\nJane,Doe";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize(typeof(PersonCsvObject), CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects schould be 2.");

            var first = result[0] as PersonCsvObject;
            Assert.AreEqual("John", first.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Smith", first.LastName, "LastName of the first object should match.");

            var second = result[1] as PersonCsvObject;
            Assert.AreEqual("Jane", second.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Doe", second.LastName, "LastName of the first object should match.");
        }

        [TestMethod]
        public void CsvSerializer_DeserializeT_AppliedOnValidObjectWithCustomNamedColumns_ShouldWork()
        {
            const string CsvText = "first name,last name\nJohn,Smith\nJane,Doe";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize<PersonCsvObject>(CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects schould be 2.");

            var first = result[0];
            Assert.AreEqual("John", first.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Smith", first.LastName, "LastName of the first object should match.");

            var second = result[1];
            Assert.AreEqual("Jane", second.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Doe", second.LastName, "LastName of the first object should match.");
        }
    }
}

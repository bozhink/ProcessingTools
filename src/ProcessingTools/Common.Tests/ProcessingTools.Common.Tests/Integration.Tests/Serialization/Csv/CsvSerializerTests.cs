// <copyright file="CsvSerializerTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Tests.Integration.Tests.Serialization.Csv
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Serialization.Csv;
    using ProcessingTools.Common.Tests.Integration.Tests.Serialization.Csv.Models;

    /// <summary>
    /// <see cref="CsvSerializer"/> Tests.
    /// </summary>
    [TestClass]
    public class CsvSerializerTests
    {
        /// <summary>
        /// <see cref="CsvSerializer"/> Deserialize Type  Applied On Invalid Object Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CsvSerializer_DeserializeType_AppliedOnInvalidObject_ShouldThrow()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No description here\nJane Smith,2016,Description here!";

            var serializer = new CsvSerializer();
            serializer.Deserialize(typeof(NameYearDescriptionSampleObject), CsvText);
        }

        /// <summary>
        /// <see cref="CsvSerializer"/> Deserialize Type Applied On Valid Object Should Work.
        /// </summary>
        [TestMethod]
        public void CsvSerializer_DeserializeType_AppliedOnValidObject_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No description here\nJane Smith,2016,Description here!";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize(typeof(NameYearDescriptionSampleCsvObject), CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects should be 2.");

            var first = result[0] as NameYearDescriptionSampleCsvObject;
            Assert.AreEqual("John Smith", first.Name, "Name of the first object should be John Smith.");
            Assert.AreEqual(2015, first.Year, "Year of the first object should be 2015.");
            Assert.AreEqual("No description here", first.Description, "Description of the first object does not match.");

            var second = result[1] as NameYearDescriptionSampleCsvObject;
            Assert.AreEqual("Jane Smith", second.Name, "Name of the second object should be Jane Smith.");
            Assert.AreEqual(2016, second.Year, "Year of the second object should be 2016.");
            Assert.AreEqual("Description here!", second.Description, "Description of the second object does not match.");
        }

        /// <summary>
        /// <see cref="CsvSerializer"/> Deserialize T Applied On Valid Object Should Work.
        /// </summary>
        [TestMethod]
        public void CsvSerializer_DeserializeT_AppliedOnValidObject_ShouldWork()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No description here\nJane Smith,2016,Description here!";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize<NameYearDescriptionSampleCsvObject>(CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects should be 2.");

            var first = result[0];
            Assert.AreEqual("John Smith", first.Name, "Name of the first object should be John Smith.");
            Assert.AreEqual(2015, first.Year, "Year of the first object should be 2015.");
            Assert.AreEqual("No description here", first.Description, "Description of the first object does not match.");

            var second = result[1];
            Assert.AreEqual("Jane Smith", second.Name, "Name of the second object should be Jane Smith.");
            Assert.AreEqual(2016, second.Year, "Year of the second object should be 2016.");
            Assert.AreEqual("Description here!", second.Description, "Description of the second object does not match.");
        }

        /// <summary>
        /// <see cref="CsvSerializer"/> Deserialize Type Applied On Valid Object With Custom Named Columns Should Work.
        /// </summary>
        [TestMethod]
        public void CsvSerializer_DeserializeType_AppliedOnValidObjectWithCustomNamedColumns_ShouldWork()
        {
            const string CsvText = "first name,last name\nJohn,Smith\nJane,Doe";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize(typeof(PersonCsvObject), CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects should be 2.");

            var first = result[0] as PersonCsvObject;
            Assert.AreEqual("John", first.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Smith", first.LastName, "LastName of the first object should match.");

            var second = result[1] as PersonCsvObject;
            Assert.AreEqual("Jane", second.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Doe", second.LastName, "LastName of the first object should match.");
        }

        /// <summary>
        /// <see cref="CsvSerializer"/> Deserialize T Applied On Valid Object With Custom Named Columns Should Work.
        /// </summary>
        [TestMethod]
        public void CsvSerializer_DeserializeT_AppliedOnValidObjectWithCustomNamedColumns_ShouldWork()
        {
            const string CsvText = "first name,last name\nJohn,Smith\nJane,Doe";

            var serializer = new CsvSerializer();
            var result = serializer.Deserialize<PersonCsvObject>(CsvText).ToList();

            Assert.AreEqual(2, result.Count, "Number of deserialized objects should be 2.");

            var first = result[0];
            Assert.AreEqual("John", first.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Smith", first.LastName, "LastName of the first object should match.");

            var second = result[1];
            Assert.AreEqual("Jane", second.FirstName, "FirstName of the first object should match.");
            Assert.AreEqual("Doe", second.LastName, "LastName of the first object should match.");
        }
    }
}

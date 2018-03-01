// <copyright file="CsvTableReaderTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Tests.Integration.Tests.Serialization.Csv
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Serialization.Csv;

    /// <summary>
    /// <see cref="CsvTableReader"/> Tests.
    /// </summary>
    [TestClass]
    public class CsvTableReaderTests
    {
        /// <summary>
        /// CSV Object with empty constructor should create valid object.
        /// </summary>
        [TestMethod]
        public void CsvObject_WithEmptyConstructor_ShouldCreateValidObject()
        {
            var csv = new CsvTableReader();
            Assert.IsFalse(string.IsNullOrEmpty(csv.Configuration.FieldSeparator.ToString()), "FieldTerminator");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.FirstRow.ToString()), "FirstRow");
            Assert.IsFalse(string.IsNullOrEmpty(csv.Configuration.RowSeparator.ToString()), "RowTerminator");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.SingleCharEscapeSymbol.ToString()), "SingleCharEscapeSymbol");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.SeparatorEscapeLeftWrapSymbol.ToString()), "TerminatorEscapeLeftWrapSymbol");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.SeparatorEscapeRightWrapSymbol.ToString()), "TerminatorEscapeRightWrapSymbol");
        }

        /// <summary>
        /// CSV Object should deserialize simple CSV text.
        /// </summary>
        [TestMethod]
        public void CsvObject_ShouldDeserializeSimpleCsvText()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No description here";

            var csv = new CsvTableReader();
            var result = csv.ReadToTable(CsvText);

            Assert.AreEqual(2, result.Length, "Invalid number of rows.");
            Assert.AreEqual(3, result[0].Length, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result[1].Length, "Invalid number of columns in second row.");

            Assert.AreEqual("Name", result[0][0], "[0][0]");
            Assert.AreEqual("Year", result[0][1], "[0][1]");
            Assert.AreEqual("Description", result[0][2], "[0][2]");

            Assert.AreEqual("John Smith", result[1][0], "[1][0]");
            Assert.AreEqual("2015", result[1][1], "[1][1]");
            Assert.AreEqual("No description here", result[1][2], "[1][2]");
        }

        /// <summary>
        /// CSV Object should deserialize CSV text with single escape char in valid position.
        /// </summary>
        [TestMethod]
        public void CsvObject_ShouldDeserializeCsvTextWithSingleEscapeCharInValidPosition()
        {
            const string CsvText = "Name,Year,Description\nSmith\\, John,2015,No description here";

            var csv = new CsvTableReader();
            var result = csv.ReadToTable(CsvText);

            Assert.AreEqual(2, result.Length, "Invalid number of rows.");
            Assert.AreEqual(3, result[0].Length, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result[1].Length, "Invalid number of columns in second row.");

            Assert.AreEqual("Name", result[0][0], "[0][0]");
            Assert.AreEqual("Year", result[0][1], "[0][1]");
            Assert.AreEqual("Description", result[0][2], "[0][2]");

            Assert.AreEqual("Smith, John", result[1][0], "[1][0]");
            Assert.AreEqual("2015", result[1][1], "[1][1]");
            Assert.AreEqual("No description here", result[1][2], "[1][2]");
        }

        /// <summary>
        /// CSV Object with CSV text with single escape char in last position should throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(exceptionType: typeof(FormatException), AllowDerivedTypes = true)]
        public void CsvObject_WithCsvTextWithSingleEscapeCharInLastPosition_ShouldThrow()
        {
            const string CsvText = "Name,Year,Description\nSmith\\, John,2015,No description here\\";

            var csv = new CsvTableReader();
            csv.ReadToTable(CsvText);

            Assert.Fail();
        }

        /// <summary>
        /// CSV Object should deserialize CSV text with escaped range.
        /// </summary>
        [TestMethod]
        public void CsvObject_ShouldDeserializeCsvTextWithEscapedRange()
        {
            const string CsvText = "Name,Year,Description\n\"Smith, \\\"John\\\"\",2015,\"No\n description\" here";

            var csv = new CsvTableReader();
            var result = csv.ReadToTable(CsvText);

            Assert.AreEqual(2, result.Length, "Invalid number of rows.");
            Assert.AreEqual(3, result[0].Length, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result[1].Length, "Invalid number of columns in second row.");

            Assert.AreEqual("Name", result[0][0], "[0][0]");
            Assert.AreEqual("Year", result[0][1], "[0][1]");
            Assert.AreEqual("Description", result[0][2], "[0][2]");

            Assert.AreEqual("Smith, \"John\"", result[1][0], "[1][0]");
            Assert.AreEqual("2015", result[1][1], "[1][1]");
            Assert.AreEqual("No\n description here", result[1][2], "[1][2]");
        }
    }
}

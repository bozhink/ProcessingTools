namespace ProcessingTools.Serialization.Tests.Csv
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Serialization.Csv;

    [TestClass]
    public class CsvTableReaderTests
    {
        [TestMethod]
        public void CsvObject_WithEmptyConstuctor_ShouldCreateValidObject()
        {
            var csv = new CsvTableReader();
            Assert.IsFalse(string.IsNullOrEmpty(csv.Configuration.FieldTerminator.ToString()), "FieldTerminator");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.FirstRow.ToString()), "FirstRow");
            Assert.IsFalse(string.IsNullOrEmpty(csv.Configuration.RowTerminator.ToString()), "RowTerminator");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.SingleCharEscapeSymbol.ToString()), "SingleCharEscapeSymbol");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.TerminatorEscapeLeftWrapSymbol.ToString()), "TerminatorEscapeLeftWrapSymbol");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.Configuration.TerminatorEscapeRightWrapSymbol.ToString()), "TerminatorEscapeRightWrapSymbol");
        }

        [TestMethod]
        public void CsvObject_ShouldDeserializeSimpleCsvText()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

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
            Assert.AreEqual("No desription here", result[1][2], "[1][2]");
        }

        [TestMethod]
        public void CsvObject_ShouldDeserializeCsvTextWithSingleEscapeCharInValidPosition()
        {
            const string CsvText = "Name,Year,Description\nSmith\\, John,2015,No desription here";

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
            Assert.AreEqual("No desription here", result[1][2], "[1][2]");
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(FormatException), AllowDerivedTypes = true)]
        public void CsvObject_WithCsvTextWithSingleEscapeCharInLastPosition_ShouldThrow()
        {
            const string CsvText = "Name,Year,Description\nSmith\\, John,2015,No desription here\\";

            var csv = new CsvTableReader();
            csv.ReadToTable(CsvText);

            Assert.Fail();
        }

        [TestMethod]
        public void CsvObject_ShouldDeserializeCsvTextWithEscapedRange()
        {
            const string CsvText = "Name,Year,Description\n\"Smith, \\\"John\\\"\",2015,\"No\n desription\" here";

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
            Assert.AreEqual("No\n desription here", result[1][2], "[1][2]");
        }
    }
}

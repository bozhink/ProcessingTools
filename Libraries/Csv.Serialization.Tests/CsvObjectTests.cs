namespace ProcessingTools.Csv.Serialization.Tests
{
    using System;
    using System.ComponentModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CsvObjectTests
    {
        [TestMethod]
        public void CsvObject_WithEmptyConstuctor_ShouldCreateValidObject()
        {
            var csv = new CsvObject();
            Assert.IsFalse(string.IsNullOrEmpty(csv.FieldTerminator.ToString()), "FieldTerminator");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.FirstRow.ToString()), "FirstRow");
            Assert.IsFalse(string.IsNullOrEmpty(csv.RowTerminator.ToString()), "RowTerminator");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.SingleCharEscapeSymbol.ToString()), "SingleCharEscapeSymbol");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.TerminatorEscapeLeftWrapSymbol.ToString()), "TerminatorEscapeLeftWrapSymbol");
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv.TerminatorEscapeRightWrapSymbol.ToString()), "TerminatorEscapeRightWrapSymbol");
        }

        [TestMethod]
        public void CsvObject_ShouldDeserializeSimpleCsvText()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

            var csv = new CsvObject();
            var result = csv.ParseToTable(CsvText);

            Assert.AreEqual(2, result.Count, "Invalid number of rows.");
            Assert.AreEqual(3, result[0].Count, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result[1].Count, "Invalid number of columns in second row.");

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

            var csv = new CsvObject();
            var result = csv.ParseToTable(CsvText);

            Assert.AreEqual(2, result.Count, "Invalid number of rows.");
            Assert.AreEqual(3, result[0].Count, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result[1].Count, "Invalid number of columns in second row.");

            Assert.AreEqual("Name", result[0][0], "[0][0]");
            Assert.AreEqual("Year", result[0][1], "[0][1]");
            Assert.AreEqual("Description", result[0][2], "[0][2]");

            Assert.AreEqual("Smith, John", result[1][0], "[1][0]");
            Assert.AreEqual("2015", result[1][1], "[1][1]");
            Assert.AreEqual("No desription here", result[1][2], "[1][2]");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), AllowDerivedTypes = true)]
        public void CsvObject_WithCsvTextWithSingleEscapeCharInLastPosition_ShouldThrow()
        {
            const string CsvText = "Name,Year,Description\nSmith\\, John,2015,No desription here\\";

            var csv = new CsvObject();
            var result = csv.ParseToTable(CsvText);

            Assert.Fail();
        }

        [TestMethod]
        public void CsvObject_ShouldDeserializeCsvTextWithEscapedRange()
        {
            const string CsvText = "Name,Year,Description\n\"Smith, \\\"John\\\"\",2015,\"No\n desription\" here";

            var csv = new CsvObject();
            var result = csv.ParseToTable(CsvText);

            Assert.AreEqual(2, result.Count, "Invalid number of rows.");
            Assert.AreEqual(3, result[0].Count, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result[1].Count, "Invalid number of columns in second row.");

            Assert.AreEqual("Name", result[0][0], "[0][0]");
            Assert.AreEqual("Year", result[0][1], "[0][1]");
            Assert.AreEqual("Description", result[0][2], "[0][2]");

            Assert.AreEqual("Smith, \"John\"", result[1][0], "[1][0]");
            Assert.AreEqual("2015", result[1][1], "[1][1]");
            Assert.AreEqual("No\n desription here", result[1][2], "[1][2]");
        }
    }
}

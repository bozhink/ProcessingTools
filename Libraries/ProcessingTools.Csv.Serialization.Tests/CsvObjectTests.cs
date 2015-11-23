namespace ProcessingTools.Csv.Serialization.Tests
{
    using System;
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
        public void CsvObject_ShouldDeserializeSimpleCsv()
        {
            const string CsvText = "Name,Year,Description\nJohn Smith,2015,No desription here";

            var csv = new CsvObject();
            var result = csv.Deserialize(CsvText);

            Assert.AreEqual(2, result.Count, "Invalid number of rows.");
            Assert.AreEqual(3, result.Dequeue().Count, "Invalid number of columns in first row.");
            Assert.AreEqual(3, result.Dequeue().Count, "Invalid number of columns in second row.");
        }
    }
}

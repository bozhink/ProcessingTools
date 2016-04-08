namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MaterialCitationsParserTests
    {
        [TestMethod]
        public void MaterialCitationsParser_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var parser = new MaterialCitationsParser();
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullInConstructor_ShouldThrowArgumentNullException()
        {
            var parser = new MaterialCitationsParser(null);
        }
    }
}

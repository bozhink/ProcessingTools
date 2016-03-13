namespace ProcessingTools.DocumentProvider.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DocumentValidatorTests
    {
        [TestMethod]
        public void DocumentValidator_ValidateSampleXml_ShouldWork()
        {
            var document = new TaxPubDocument();
            document.Xml = "<article><front></front></article>";

            var validator = new DocumentValidator(document);

            validator.Validate().Wait();
        }
    }
}
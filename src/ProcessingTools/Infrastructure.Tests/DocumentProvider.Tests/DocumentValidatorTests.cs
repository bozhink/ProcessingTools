namespace ProcessingTools.DocumentProvider.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Loggers.Loggers;
    using ProcessingTools.Reporters;

    [TestClass]
    public class DocumentValidatorTests
    {
        [TestMethod]
        [Ignore]
        public void DocumentValidator_ValidateSampleXml_ShouldWork()
        {
            var document = new TaxPubDocument();
            document.Xml = "<article><front></front></article>";

            var validator = new DocumentValidator();
            var reporter = new LogReporter(new ConsoleLogger());

            validator.ValidateAsync(document, reporter).Wait();
        }
    }
}

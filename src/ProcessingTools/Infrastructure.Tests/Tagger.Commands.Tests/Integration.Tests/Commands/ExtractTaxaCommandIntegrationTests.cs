namespace ProcessingTools.Tagger.Commands.Tests.Integration.Tests.Commands
{
    using System.Threading.Tasks;
    using System.Xml;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger;
    using ProcessingTools.Contracts;
    using ProcessingTools.Harvesters.Contracts.Bio;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(ExtractTaxaCommand))]
    public class ExtractTaxaCommandIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract taxa - should not change document content.")]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractTaxa_ShouldNotChangeDocumentContent(bool extractLowerTaxa, bool extractHigherTaxa)
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(true);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(extractLowerTaxa);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(extractHigherTaxa);

            // Act
            string initialContent = xmldocumentStub.OuterXml;

            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            string finalContent = xmldocumentStub.OuterXml;

            // Assert
            Assert.AreEqual(initialContent, finalContent);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract only lower taxa - should not change document content.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractOnlyLowerTaxa_ShouldNotChangeDocumentContent()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(true);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(false);

            // Act
            string initialContent = xmldocumentStub.OuterXml;

            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            string finalContent = xmldocumentStub.OuterXml;

            // Assert
            Assert.AreEqual(initialContent, finalContent);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract only higher taxa - should not change document content.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractOnlyHigherTaxa_ShouldNotChangeDocumentContent()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(true);

            // Act
            string initialContent = xmldocumentStub.OuterXml;

            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            string finalContent = xmldocumentStub.OuterXml;

            // Assert
            Assert.AreEqual(initialContent, finalContent);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract lower and higher taxa - should not change document content.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractLowerAndHigherTaxa_ShouldNotChangeDocumentContent()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(true);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(true);

            // Act
            string initialContent = xmldocumentStub.OuterXml;

            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            string finalContent = xmldocumentStub.OuterXml;

            // Assert
            Assert.AreEqual(initialContent, finalContent);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - with no extract parameter - should not change document content.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_WithNoExtractParameter_ShouldNotChangeDocumentContent()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(false);

            // Act
            string initialContent = xmldocumentStub.OuterXml;

            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            string finalContent = xmldocumentStub.OuterXml;

            // Assert
            Assert.AreEqual(initialContent, finalContent);
        }
    }
}

namespace ProcessingTools.Tagger.Commands.Tests.Unit.Tests.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Tagger.Commands.Commands;
    using ProcessingTools.Tests.Library;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ParseLowerTaxaCommand))]
    public class ParseLowerTaxaCommandUnitTests
    {
        #region ConstructorTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ParseLowerTaxaCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName, "ParamName is not correct.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<ILowerTaxaParser>();
            var command = new ParseLowerTaxaCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command, "Initialized Command object should not be null.");

            var parser = PrivateField.GetInstanceField(
                typeof(ParseLowerTaxaCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser, "Private parser field should not be null.");
            Assert.AreSame(parserMock.Object, parser, "Private parser field should not be set correctly.");
        }

        #endregion ConstructorTests

        #region ExecutionTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var parserMock = new Mock<ILowerTaxaParser>();
            var command = new ParseLowerTaxaCommand(parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, null);
            });

            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ILowerTaxaParser>();
            var command = new ParseLowerTaxaCommand(parserMock.Object);
            var settingsMock = new Mock<ICommandSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName, "ParamName is not correct.");

            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ILowerTaxaParser>();
            var command = new ParseLowerTaxaCommand(parserMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName, "ParamName is not correct.");

            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand Run with valid document and valid program settings should call parser with correct parameter.")]
        [Timeout(2000)]
        public async Task ParseLowerTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var parserMock = new Mock<ILowerTaxaParser>();
            var command = new ParseLowerTaxaCommand(parserMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Once);
            parserMock.Verify(p => p.ParseAsync(xmldocumentStub.DocumentElement), Times.Once);
        }

        #endregion ExecutionTests
    }
}

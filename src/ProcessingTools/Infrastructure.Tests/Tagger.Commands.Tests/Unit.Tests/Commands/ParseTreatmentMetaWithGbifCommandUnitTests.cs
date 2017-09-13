namespace ProcessingTools.Tagger.Commands.Tests.Unit.Tests.Commands
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Tagger.Commands.Commands;
    using ProcessingTools.Tagger.Commands.Contracts;
    using ProcessingTools.Tests.Library;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ParseTreatmentMetaWithGbifCommand))]
    public class ParseTreatmentMetaWithGbifCommandUnitTests
    {
        #region ConstructorTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithGbifCommand), Description = "ParseTreatmentMetaWithGbifCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithGbifCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ParseTreatmentMetaWithGbifCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName, "ParamName is not correct.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithGbifCommand), Description = "ParseTreatmentMetaWithGbifCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithGbifCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithGbifCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command, "Initialized Command object should not be null.");

            var parser = PrivateField.GetInstanceField(
                typeof(ParseTreatmentMetaWithGbifCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser, "Private parser field should not be null.");
            Assert.AreSame(parserMock.Object, parser, "Private parser field should not be set correctly.");
        }

        #endregion ConstructorTests

        #region ExecutionTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithGbifCommand), Description = "ParseTreatmentMetaWithGbifCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithGbifCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithGbifCommand(parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(null, null);
            });

            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithGbifCommand), Description = "ParseTreatmentMetaWithGbifCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithGbifCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithGbifCommand(parserMock.Object);
            var settingsMock = new Mock<ICommandSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName, "ParamName is not correct.");

            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithGbifCommand), Description = "ParseTreatmentMetaWithGbifCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithGbifCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithGbifCommand(parserMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName, "ParamName is not correct.");

            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithGbifCommand), Description = "ParseTreatmentMetaWithGbifCommand Run with valid document and valid program settings should call parser with correct parameter.")]
        [Timeout(2000)]
        public async Task ParseTreatmentMetaWithGbifCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithGbifCommand(parserMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            // Act
            await command.Run(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Once);
            parserMock.Verify(p => p.Parse(documentMock.Object), Times.Once);
        }

        #endregion ExecutionTests
    }
}

namespace ProcessingTools.Tagger.Tests.Unit.Tests.Core.Commands
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Tagger.Contracts;
    using ProcessingTools.Tagger.Core.Commands;
    using ProcessingTools.Tests.Library;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand))]
    public class ParseTreatmentMetaWithAphiaCommandUnitTests
    {
        #region ConstructorTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand), Description = "ParseTreatmentMetaWithAphiaCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithAphiaCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var command = new ParseTreatmentMetaWithAphiaCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName, "ParamName is not correct.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand), Description = "ParseTreatmentMetaWithAphiaCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithAphiaCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithAphiaCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command, "Initialized Command object should not be null.");

            var parser = PrivateField.GetInstanceField(
                typeof(ParseTreatmentMetaWithAphiaCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser, "Private parser field should not be null.");
            Assert.AreSame(parserMock.Object, parser, "Private parser field should not be set correctly.");
        }

        #endregion ConstructorTests

        #region ExecutionTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand), Description = "ParseTreatmentMetaWithAphiaCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithAphiaCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithAphiaCommand(parserMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(null, null);
            });

            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand), Description = "ParseTreatmentMetaWithAphiaCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithAphiaCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithAphiaCommand(parserMock.Object);
            var settingsMock = new Mock<IProgramSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName, "ParamName is not correct.");

            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand), Description = "ParseTreatmentMetaWithAphiaCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithAphiaCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithAphiaCommand(parserMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName, "ParamName is not correct.");

            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithAphiaCommand), Description = "ParseTreatmentMetaWithAphiaCommand Run with valid document and valid program settings should call parser with correct parameter.")]
        [Timeout(2000)]
        public async Task ParseTreatmentMetaWithAphiaCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>();
            var command = new ParseTreatmentMetaWithAphiaCommand(parserMock.Object);
            var settingsMock = new Mock<IProgramSettings>();
            var documentMock = new Mock<IDocument>();

            // Act
            var result = await command.Run(documentMock.Object, settingsMock.Object);

            // Assert
            parserMock.Verify(p => p.Parse(It.IsAny<IDocument>()), Times.Once);
            parserMock.Verify(p => p.Parse(documentMock.Object), Times.Once);
        }

        #endregion ExecutionTests
    }
}

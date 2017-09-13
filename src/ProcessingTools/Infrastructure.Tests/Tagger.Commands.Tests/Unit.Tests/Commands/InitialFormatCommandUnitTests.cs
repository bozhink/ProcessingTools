namespace ProcessingTools.Tagger.Commands.Tests.Unit.Tests.Commands
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Formatters;
    using ProcessingTools.Tagger.Commands.Commands;
    using ProcessingTools.Tagger.Commands.Contracts;
    using ProcessingTools.Tests.Library;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(InitialFormatCommand))]
    public class InitialFormatCommandUnitTests
    {
        #region ConstructorTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand with null formatter should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void InitialFormatCommand_WithNullFormatter_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new InitialFormatCommand(null);
            });

            Assert.AreEqual(ParameterNames.Formatter, exception.ParamName, "ParamName is not correct.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand with valid formatter should correctly initialize object.")]
        [Timeout(2000)]
        public void InitialFormatCommand_WithValidFormatter_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var formatterMock = new Mock<IDocumentInitialFormatter>();
            var command = new InitialFormatCommand(formatterMock.Object);

            // Assert
            Assert.IsNotNull(command, "Initialized Command object should not be null.");

            var formatter = PrivateField.GetInstanceField(
                typeof(InitialFormatCommand).BaseType,
                command,
                ParameterNames.Formatter);

            Assert.IsNotNull(formatter, "Private formatter field should not be null.");
            Assert.AreSame(formatterMock.Object, formatter, "Private formatter field should not be set correctly.");
        }

        #endregion ConstructorTests

        #region ExecutionTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void InitialFormatCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var formatterMock = new Mock<IDocumentInitialFormatter>();
            var command = new InitialFormatCommand(formatterMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(null, null);
            });

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void InitialFormatCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var formatterMock = new Mock<IDocumentInitialFormatter>();
            var command = new InitialFormatCommand(formatterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName, "ParamName is not correct.");

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void InitialFormatCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var formatterMock = new Mock<IDocumentInitialFormatter>();
            var command = new InitialFormatCommand(formatterMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.Run(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName, "ParamName is not correct.");

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand Run with valid document and valid program settings should call formatter with correct parameter.")]
        [Timeout(2000)]
        public async Task InitialFormatCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var formatterMock = new Mock<IDocumentInitialFormatter>();
            var command = new InitialFormatCommand(formatterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            // Act
            await command.Run(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Once);
            formatterMock.Verify(p => p.FormatAsync(documentMock.Object), Times.Once);
        }

        #endregion ExecutionTests
    }
}

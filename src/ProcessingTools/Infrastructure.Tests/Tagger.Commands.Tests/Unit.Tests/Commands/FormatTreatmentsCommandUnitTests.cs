namespace ProcessingTools.Tagger.Commands.Tests.Unit.Tests.Commands
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Tests.Library;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(FormatTreatmentsCommand))]
    public class FormatTreatmentsCommandUnitTests
    {
        #region ConstructorTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand with null formatter should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_WithNullFormatter_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new FormatTreatmentsCommand(null);
            });

            Assert.AreEqual(ParameterNames.Formatter, exception.ParamName, "ParamName is not correct.");
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand with valid formatter should correctly initialize object.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_WithValidFormatter_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var formatterMock = new Mock<ITreatmentFormatter>();
            var command = new FormatTreatmentsCommand(formatterMock.Object);

            // Assert
            Assert.IsNotNull(command, "Initialized Command object should not be null.");

            var formatter = PrivateField.GetInstanceField(
                typeof(FormatTreatmentsCommand).BaseType,
                command,
                ParameterNames.Formatter);

            Assert.IsNotNull(formatter, "Private formatter field should not be null.");
            Assert.AreSame(formatterMock.Object, formatter, "Private formatter field should not be set correctly.");
        }

        #endregion ConstructorTests

        #region ExecutionTests

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var formatterMock = new Mock<ITreatmentFormatter>();
            var command = new FormatTreatmentsCommand(formatterMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, null);
            });

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var formatterMock = new Mock<ITreatmentFormatter>();
            var command = new FormatTreatmentsCommand(formatterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName, "ParamName is not correct.");

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var formatterMock = new Mock<ITreatmentFormatter>();
            var command = new FormatTreatmentsCommand(formatterMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName, "ParamName is not correct.");

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand Run with valid document and valid program settings should call formatter with correct parameter.")]
        [Timeout(2000)]
        public async Task FormatTreatmentsCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var formatterMock = new Mock<ITreatmentFormatter>();
            var command = new FormatTreatmentsCommand(formatterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Once);
            formatterMock.Verify(p => p.FormatAsync(documentMock.Object), Times.Once);
        }

        #endregion ExecutionTests
    }
}

// <copyright file="ParseTreatmentMetaWithInternalInformationCommandUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Tests.Unit.Tests
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand))]
    public class ParseTreatmentMetaWithInternalInformationCommandUnitTests
    {
        /// <summary>
        /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> with null parser should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand), Description = "ParseTreatmentMetaWithInternalInformationCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithInternalInformationCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ParseTreatmentMetaWithInternalInformationCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> with valid parser should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand), Description = "ParseTreatmentMetaWithInternalInformationCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithInternalInformationCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<ITreatmentMetaParserWithInternalInformation>();
            var command = new ParseTreatmentMetaWithInternalInformationCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var parser = PrivateField.GetInstanceField(
                typeof(ParseTreatmentMetaWithInternalInformationCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser);
            Assert.AreSame(parserMock.Object, parser);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand), Description = "ParseTreatmentMetaWithInternalInformationCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithInternalInformationCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithInternalInformation>();
            var command = new ParseTreatmentMetaWithInternalInformationCommand(parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, null);
            });

            parserMock.Verify(p => p.ParseAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand), Description = "ParseTreatmentMetaWithInternalInformationCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithInternalInformationCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithInternalInformation>();
            var command = new ParseTreatmentMetaWithInternalInformationCommand(parserMock.Object);
            var settingsMock = new Mock<ICommandSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName);

            parserMock.Verify(p => p.ParseAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand), Description = "ParseTreatmentMetaWithInternalInformationCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMetaWithInternalInformationCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithInternalInformation>();
            var command = new ParseTreatmentMetaWithInternalInformationCommand(parserMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName);

            parserMock.Verify(p => p.ParseAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMetaWithInternalInformationCommand"/> Run with valid document and valid program settings should call parser with correct parameter.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMetaWithInternalInformationCommand), Description = "ParseTreatmentMetaWithInternalInformationCommand Run with valid document and valid program settings should call parser with correct parameter.")]
        [Timeout(2000)]
        public async Task ParseTreatmentMetaWithInternalInformationCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMetaParserWithInternalInformation>();
            var command = new ParseTreatmentMetaWithInternalInformationCommand(parserMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            parserMock.Verify(p => p.ParseAsync(It.IsAny<IDocument>()), Times.Once);
            parserMock.Verify(p => p.ParseAsync(documentMock.Object), Times.Once);
        }
    }
}

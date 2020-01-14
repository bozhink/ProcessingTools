// <copyright file="FormatTreatmentsCommandUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Tests.Unit.Tests
{
    using System;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Commands.Tagger;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// <see cref="FormatTreatmentsCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(FormatTreatmentsCommand))]
    public class FormatTreatmentsCommandUnitTests
    {
        /// <summary>
        /// <see cref="FormatTreatmentsCommand"/> with null formatter should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand with null formatter should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_WithNullFormatter_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new FormatTreatmentsCommand(null);
            });

            Assert.AreEqual(ParameterNames.Formatter, exception.ParamName);
        }

        /// <summary>
        /// <see cref="FormatTreatmentsCommand"/> with valid formatter should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(FormatTreatmentsCommand), Description = "FormatTreatmentsCommand with valid formatter should correctly initialize object.")]
        [Timeout(2000)]
        public void FormatTreatmentsCommand_WithValidFormatter_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var formatterMock = new Mock<ITreatmentFormatter>();
            var command = new FormatTreatmentsCommand(formatterMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var formatter = PrivateField.GetInstanceField(
                typeof(FormatTreatmentsCommand).BaseType,
                command,
                ParameterNames.Formatter);

            Assert.IsNotNull(formatter);
            Assert.AreSame(formatterMock.Object, formatter);
        }

        /// <summary>
        /// <see cref="FormatTreatmentsCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
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

        /// <summary>
        /// <see cref="FormatTreatmentsCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
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

            Assert.AreEqual(ParameterNames.Document, exception.ParamName);

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="FormatTreatmentsCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
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

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName);

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="FormatTreatmentsCommand"/> Run with valid document and valid program settings should call formatter with correct parameter.
        /// </summary>
        /// <returns>Task.</returns>
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
    }
}

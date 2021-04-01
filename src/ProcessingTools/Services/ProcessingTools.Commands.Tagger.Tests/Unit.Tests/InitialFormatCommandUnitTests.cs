﻿// <copyright file="InitialFormatCommandUnitTests.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.Services.Layout;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// <see cref="InitialFormatCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(InitialFormatCommand))]
    public class InitialFormatCommandUnitTests
    {
        /// <summary>
        /// <see cref="InitialFormatCommand"/> with null formatter should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand with null formatter should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void InitialFormatCommand_WithNullFormatter_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new InitialFormatCommand(null);
            });

            Assert.AreEqual(ParameterNames.Formatter, exception.ParamName);
        }

        /// <summary>
        /// <see cref="InitialFormatCommand"/> with valid formatter should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(InitialFormatCommand), Description = "InitialFormatCommand with valid formatter should correctly initialize object.")]
        [Timeout(2000)]
        public void InitialFormatCommand_WithValidFormatter_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var formatterMock = new Mock<IDocumentInitialFormatter>();
            var command = new InitialFormatCommand(formatterMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var formatter = PrivateField.GetInstanceField(
                typeof(InitialFormatCommand).BaseType,
                command,
                ParameterNames.Formatter);

            Assert.IsNotNull(formatter);
            Assert.AreSame(formatterMock.Object, formatter);
        }

        /// <summary>
        /// <see cref="InitialFormatCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
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
                return command.RunAsync(null, null);
            });

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="InitialFormatCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
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
                return command.RunAsync(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName);

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="InitialFormatCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
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
                return command.RunAsync(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName);

            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="InitialFormatCommand"/> Run with valid document and valid program settings should call formatter with correct parameter.
        /// </summary>
        /// <returns>Task.</returns>
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
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            formatterMock.Verify(p => p.FormatAsync(It.IsAny<IDocument>()), Times.Once);
            formatterMock.Verify(p => p.FormatAsync(documentMock.Object), Times.Once);
        }
    }
}

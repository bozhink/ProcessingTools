﻿// <copyright file="ParseTreatmentMaterialsCommandUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Contracts.Services.Bio.Materials;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// <see cref="ParseTreatmentMaterialsCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ParseTreatmentMaterialsCommand))]
    public class ParseTreatmentMaterialsCommandUnitTests
    {
        /// <summary>
        /// <see cref="ParseTreatmentMaterialsCommand"/> with null parser should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMaterialsCommand), Description = "ParseTreatmentMaterialsCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMaterialsCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ParseTreatmentMaterialsCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMaterialsCommand"/> with valid parser should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMaterialsCommand), Description = "ParseTreatmentMaterialsCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ParseTreatmentMaterialsCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<ITreatmentMaterialsParser>();
            var command = new ParseTreatmentMaterialsCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var parser = PrivateField.GetInstanceField(
                typeof(ParseTreatmentMaterialsCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser);
            Assert.AreSame(parserMock.Object, parser);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMaterialsCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMaterialsCommand), Description = "ParseTreatmentMaterialsCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ParseTreatmentMaterialsCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMaterialsParser>();
            var command = new ParseTreatmentMaterialsCommand(parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, null);
            });

            parserMock.Verify(p => p.ParseAsync(It.IsAny<IDocument>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ParseTreatmentMaterialsCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMaterialsCommand), Description = "ParseTreatmentMaterialsCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMaterialsCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMaterialsParser>();
            var command = new ParseTreatmentMaterialsCommand(parserMock.Object);
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
        /// <see cref="ParseTreatmentMaterialsCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMaterialsCommand), Description = "ParseTreatmentMaterialsCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseTreatmentMaterialsCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMaterialsParser>();
            var command = new ParseTreatmentMaterialsCommand(parserMock.Object);
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
        /// <see cref="ParseTreatmentMaterialsCommand"/> Run with valid document and valid program settings should call parser with correct parameter.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseTreatmentMaterialsCommand), Description = "ParseTreatmentMaterialsCommand Run with valid document and valid program settings should call parser with correct parameter.")]
        [Timeout(2000)]
        public async Task ParseTreatmentMaterialsCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var parserMock = new Mock<ITreatmentMaterialsParser>();
            var command = new ParseTreatmentMaterialsCommand(parserMock.Object);
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
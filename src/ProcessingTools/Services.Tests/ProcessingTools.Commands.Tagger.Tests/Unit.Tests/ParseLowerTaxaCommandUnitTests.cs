// <copyright file="ParseLowerTaxaCommandUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Tests.Unit.Tests
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Commands.Tagger;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// <see cref="ParseLowerTaxaCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ParseLowerTaxaCommand))]
    public class ParseLowerTaxaCommandUnitTests
    {
        #region ConstructorTests

        /// <summary>
        /// <see cref="ParseLowerTaxaCommand"/> with null parser should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ParseLowerTaxaCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName);
        }

        /// <summary>
        /// <see cref="ParseLowerTaxaCommand"/> with valid parser should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ParseLowerTaxaCommand), Description = "ParseLowerTaxaCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ParseLowerTaxaCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<ILowerTaxaParser>();
            var command = new ParseLowerTaxaCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var parser = PrivateField.GetInstanceField(
                typeof(ParseLowerTaxaCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser);
            Assert.AreSame(parserMock.Object, parser);
        }

        #endregion ConstructorTests

        #region ExecutionTests

        /// <summary>
        /// <see cref="ParseLowerTaxaCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
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

        /// <summary>
        /// <see cref="ParseLowerTaxaCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
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

            Assert.AreEqual(ParameterNames.Document, exception.ParamName);

            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ParseLowerTaxaCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
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

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName);

            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ParseLowerTaxaCommand"/> Run with valid document and valid program settings should call parser with correct parameter.
        /// </summary>
        /// <returns>Task.</returns>
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

// <copyright file="ExpandLowerTaxaCommandUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Tests.Unit.Tests
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// <see cref="ExpandLowerTaxaCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ExpandLowerTaxaCommand))]
    public class ExpandLowerTaxaCommandUnitTests
    {
        /// <summary>
        /// <see cref="ExpandLowerTaxaCommand"/> with null parser should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaCommand), Description = "ExpandLowerTaxaCommand with null parser should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExpandLowerTaxaCommand_WithNullParser_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange + Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExpandLowerTaxaCommand(null);
            });

            Assert.AreEqual(ParameterNames.Parser, exception.ParamName);
        }

        /// <summary>
        /// <see cref="ExpandLowerTaxaCommand"/> with valid parser should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaCommand), Description = "ExpandLowerTaxaCommand with valid parser should correctly initialize object.")]
        [Timeout(2000)]
        public void ExpandLowerTaxaCommand_WithValidParser_ShouldCorrectlyInitializeObject()
        {
            // Arrange + Act
            var parserMock = new Mock<IExpander>();
            var command = new ExpandLowerTaxaCommand(parserMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var parser = PrivateField.GetInstanceField(
                typeof(ExpandLowerTaxaCommand).BaseType,
                command,
                ParameterNames.Parser);

            Assert.IsNotNull(parser);
            Assert.AreSame(parserMock.Object, parser);
        }

        /// <summary>
        /// <see cref="ExpandLowerTaxaCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaCommand), Description = "ExpandLowerTaxaCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ExpandLowerTaxaCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var parserMock = new Mock<IExpander>();
            var command = new ExpandLowerTaxaCommand(parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, null);
            });

            parserMock.Verify(p => p.ParseAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ExpandLowerTaxaCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaCommand), Description = "ExpandLowerTaxaCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExpandLowerTaxaCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<IExpander>();
            var command = new ExpandLowerTaxaCommand(parserMock.Object);
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
        /// <see cref="ExpandLowerTaxaCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaCommand), Description = "ExpandLowerTaxaCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExpandLowerTaxaCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var parserMock = new Mock<IExpander>();
            var command = new ExpandLowerTaxaCommand(parserMock.Object);
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
        /// <see cref="ExpandLowerTaxaCommand"/> Run with valid document and valid program settings should call parser with correct parameter.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaCommand), Description = "ExpandLowerTaxaCommand Run with valid document and valid program settings should call parser with correct parameter.")]
        [Timeout(2000)]
        public async Task ExpandLowerTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ShouldCallParserWithCorrectParameter()
        {
            // Arrange
            var parserMock = new Mock<IExpander>();
            var command = new ExpandLowerTaxaCommand(parserMock.Object);
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
    }
}

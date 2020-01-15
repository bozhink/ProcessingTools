// <copyright file="ExtractTaxaCommandUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Tests.Unit.Tests
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Tests;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Commands.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// <see cref="ExtractTaxaCommand"/> unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit", TestOf = typeof(ExtractTaxaCommand))]
    public class ExtractTaxaCommandUnitTests
    {
        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> with null harvester and null reporter should throw ArgumentNullException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand with null harvester and null reporter should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_WithNullHarvesterAndNullReporter_ShouldThrowArgumentNullException()
        {
            // Arrange + Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ExtractTaxaCommand(null, null);
            });
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> with null harvester and valid reporter should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand with null harvester and valid reporter should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_WithNullHarvesterAndValidReporter_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var reporterMock = new Mock<IReporter>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExtractTaxaCommand(null, reporterMock.Object);
            });

            Assert.AreEqual(ParameterNames.Harvester, exception.ParamName);
            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Never);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Never);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> with valid harvester and null reporter should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand with valid harvester and null reporter should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_WithValidHarvesterAndNullReporter_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExtractTaxaCommand(harvesterMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Reporter, exception.ParamName);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> with valid harvester and valid reporter should correctly initialize object.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand with valid harvester and valid reporter should correctly initialize object.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_WithValidHarvesterAndValidReporter_ShouldCorrectlyInitializeObject()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();

            // Act
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);

            // Assert
            Assert.IsNotNull(command);

            var harvester = PrivateField.GetInstanceField<ExtractTaxaCommand>(
                command,
                ParameterNames.Harvester);
            Assert.IsNotNull(harvester);
            Assert.AreSame(harvesterMock.Object, harvester);

            var reporter = PrivateField.GetInstanceField<ExtractTaxaCommand>(
                command,
                ParameterNames.Reporter);
            Assert.IsNotNull(reporter);
            Assert.AreSame(reporterMock.Object, reporter);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Never);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Never);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with null document and null program settings should throw ArgumentNullException.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with null document and null program settings should throw ArgumentNullException.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_RunWithNullDocumentAndNullProgramSettings_ShouldThrowArgumentNullException()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, null);
            });

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Never);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Never);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with null document and valid program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_RunWithNullDocumentAndValidProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(null, settingsMock.Object);
            });

            Assert.AreEqual(ParameterNames.Document, exception.ParamName);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Never);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Never);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and null program settings should throw ArgumentNullException with correct ParamName.")]
        [Timeout(2000)]
        public void ExtractTaxaCommand_RunWithValidDocumentAndNullProgramSettings_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var documentMock = new Mock<IDocument>();

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return command.RunAsync(documentMock.Object, null);
            });

            Assert.AreEqual(ParameterNames.Settings, exception.ParamName);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Never);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Never);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with valid document and valid program settings - extract lower and higher taxa - should work.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract lower and higher taxa - should work.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractLowerAndHigherTaxa_ShouldWork()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(true);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(true);

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            settingsMock.VerifyGet(s => s.ExtractTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractLowerTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractHigherTaxa, Times.Once);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Once);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(xmldocumentStub.DocumentElement), Times.Once);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Once);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(xmldocumentStub.DocumentElement), Times.Once);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Exactly(2));
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Once);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with valid document and valid program settings - extract only higher taxa - should work.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract only higher taxa - should work.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractOnlyHigherTaxa_ShouldWork()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(true);

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            settingsMock.VerifyGet(s => s.ExtractTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractLowerTaxa, Times.AtMostOnce);
            settingsMock.VerifyGet(s => s.ExtractHigherTaxa, Times.Once);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Once);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(xmldocumentStub.DocumentElement), Times.Once);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Once);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Once);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with valid document and valid program settings - extract only lower taxa - should work.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract only lower taxa - should work.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractOnlyLowerTaxa_ShouldWork()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(true);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(false);

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            settingsMock.VerifyGet(s => s.ExtractTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractLowerTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractHigherTaxa, Times.AtMostOnce);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Once);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(xmldocumentStub.DocumentElement), Times.Once);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Once);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Once);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with valid document and valid program settings - extract taxa - should work.
        /// </summary>
        /// <param name="extractLowerTaxa">Extract lower taxa.</param>
        /// <param name="extractHigherTaxa">Extract higher taxa.</param>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - extract taxa - should work.")]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_ExtractTaxa_ShouldWork(bool extractLowerTaxa, bool extractHigherTaxa)
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(true);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(extractLowerTaxa);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(extractHigherTaxa);

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            settingsMock.VerifyGet(s => s.ExtractTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractLowerTaxa, Times.Never);
            settingsMock.VerifyGet(s => s.ExtractHigherTaxa, Times.Never);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Once);
            harvesterMock.Verify(r => r.HarvestAsync(xmldocumentStub.DocumentElement), Times.Once);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Once);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Once);
        }

        /// <summary>
        /// <see cref="ExtractTaxaCommand"/> Run with valid document and valid program settings - with no extract parameter - should work.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExtractTaxaCommand), Description = "ExtractTaxaCommand Run with valid document and valid program settings - with no extract parameter - should work.")]
        [Timeout(2000)]
        public async Task ExtractTaxaCommand_RunWithValidDocumentAndValidProgramSettings_WithNoExtractParameter_ShouldWork()
        {
            // Arrange
            var harvesterMock = new Mock<ITaxonNamesHarvester>();
            var reporterMock = new Mock<IReporter>();
            var command = new ExtractTaxaCommand(harvesterMock.Object, reporterMock.Object);
            var settingsMock = new Mock<ICommandSettings>();
            var documentMock = new Mock<IDocument>();

            var xmldocumentStub = new XmlDocument();
            xmldocumentStub.LoadXml("<a />");

            documentMock
                .SetupGet(d => d.XmlDocument)
                .Returns(xmldocumentStub);

            settingsMock
                .SetupGet(s => s.ExtractTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractLowerTaxa)
                .Returns(false);
            settingsMock
                .SetupGet(s => s.ExtractHigherTaxa)
                .Returns(false);

            // Act
            await command.RunAsync(documentMock.Object, settingsMock.Object).ConfigureAwait(false);

            // Assert
            settingsMock.VerifyGet(s => s.ExtractTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractLowerTaxa, Times.Once);
            settingsMock.VerifyGet(s => s.ExtractHigherTaxa, Times.Once);

            harvesterMock.Verify(r => r.HarvestAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestLowerTaxaAsync(It.IsAny<XmlNode>()), Times.Never);
            harvesterMock.Verify(r => r.HarvestHigherTaxaAsync(It.IsAny<XmlNode>()), Times.Never);

            reporterMock.Verify(r => r.AppendContent(It.IsAny<string>()), Times.Never);
            reporterMock.Verify(r => r.MakeReportAsync(), Times.Once);
        }
    }
}

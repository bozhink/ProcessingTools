namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;
    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Tests.Library;

    [TestFixture(TestOf = typeof(ExpandLowerTaxaController))]
    public class ExpandLowerTaxaControllerTests
    {
        private const string CallShouldThrowSystemAggregateExceptionMessage = "Call should throw System.AggregateException.";
        private const string InnerExceptionShouldBeArgumentNullExceptionMessage = "InnerException should be System.ArgumentNullException.";
        private const string ContentShouldBeUnchangedMessage = "Content should be unchanged.";

        private const string DocumentFactoryFieldName = "documentFactory";
        private const string ParserFieldName = "parser";
        private const string ContextParameterName = "context";

        private static readonly Type ExpandLowerTaxaControllerType = typeof(ExpandLowerTaxaController);

        private XmlDocument document;
        private ProgramSettings settings;

        [SetUp]
        public void Init()
        {
            this.document = new XmlDocument();
            this.document.LoadXml("<root />");

            this.settings = new ProgramSettings();
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaController), Description = "ExpandLowerTaxaController with valid parameters in constructor should return correctly initialized object.")]
        [Timeout(500)]
        public void ExpandLowerTaxaController_WithValidParametersInConstructor_ShouldReturnCorrectlyInitializedObject()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var documentFactory = documentFactoryMock.Object;

            var parserMock = new Mock<IExpander>();
            var parser = parserMock.Object;

            // Act
            var controller = new ExpandLowerTaxaController(documentFactory, parser);

            // Asset
            Assert.IsNotNull(controller);

            var documentFactoryField = PrivateField.GetInstanceField(
                ExpandLowerTaxaControllerType.BaseType,
                controller,
                DocumentFactoryFieldName);
            Assert.IsNotNull(documentFactoryField);
            Assert.AreSame(documentFactory, documentFactoryField);

            var parserField = PrivateField.GetInstanceField(
                ExpandLowerTaxaControllerType,
                controller,
                ParserFieldName);
            Assert.IsNotNull(parserField);
            Assert.AreSame(parser, parserField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaController), Description = "ExpandLowerTaxaController with valid parameters in constructor should work.")]
        [Timeout(500)]
        public void ExpandLowerTaxaController_WithValidParametersInConstructor_ShouldWork()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act
            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.settings).Wait();

            string finalContent = this.document.OuterXml;

            // Assert
            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaController), Description = "ExpandLowerTaxaController run with null context and valid other parameters should throw async ArgumentNullException with correct ParamName.")]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndValidOtherParameters_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, this.settings);
            });

            Assert.AreEqual(ContextParameterName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExpandLowerTaxaController), Description = "ExpandLowerTaxaController run with null context and null namespaceManager and valid other parameters should throw async ArgumentNullException.")]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowArgumentNullException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, this.settings);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, this.settings);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();
            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, this.settings);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();
            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(null, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            try
            {
                controller.Run(null, this.settings).Wait();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(AggregateException), e.GetType(), CallShouldThrowSystemAggregateExceptionMessage);

                Exception innerException = e.InnerException;
                Assert.AreEqual(typeof(ArgumentNullException), innerException.GetType(), InnerExceptionShouldBeArgumentNullExceptionMessage);

                Assert.AreEqual("context", ((ArgumentNullException)innerException).ParamName, @"ParamName should be ""context"".");
            }
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(this.document.DocumentElement, this.settings);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(this.document.DocumentElement, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(this.document.DocumentElement, this.settings);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullNamespaceManagerAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(this.document.DocumentElement, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            try
            {
                controller.Run(this.document.DocumentElement, this.settings).Wait();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(AggregateException), e.GetType(), CallShouldThrowSystemAggregateExceptionMessage);

                Exception innerException = e.InnerException;
                Assert.AreEqual(typeof(ArgumentNullException), innerException.GetType(), InnerExceptionShouldBeArgumentNullExceptionMessage);

                Assert.AreEqual("namespaceManager", ((ArgumentNullException)innerException).ParamName, @"ParamName should be ""namespaceManager"".");
            }
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(this.document.DocumentElement, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return controller.Run(this.document.DocumentElement, null);
            });
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();

            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            try
            {
                controller.Run(this.document.DocumentElement, null).Wait();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(AggregateException), e.GetType(), CallShouldThrowSystemAggregateExceptionMessage);

                Exception innerException = e.InnerException;
                Assert.AreEqual(typeof(ArgumentNullException), innerException.GetType(), InnerExceptionShouldBeArgumentNullExceptionMessage);

                Assert.AreEqual("settings", ((ArgumentNullException)innerException).ParamName, @"ParamName should be ""settings"".");
            }
        }

        [Test]
        [Timeout(500)]
        public void ExpandLowerTaxaController_RunWithNullLoggerAndValidOtherParameters_ShouldWork()
        {
            // Arrange
            var documentFactoryMock = new Mock<IDocumentFactory>();
            var parserMock = new Mock<IExpander>();
            var controller = new ExpandLowerTaxaController(documentFactoryMock.Object, parserMock.Object);

            // Act + Assert
            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.settings).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }
    }
}

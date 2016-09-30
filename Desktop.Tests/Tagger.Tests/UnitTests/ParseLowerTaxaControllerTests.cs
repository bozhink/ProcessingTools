namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;

    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts;

    [TestFixture]
    public class ParseLowerTaxaControllerTests
    {
        private const string CallShouldThrowSystemAggregateExceptionMessage = "Call should throw System.AggregateException.";
        private const string InnerExceptionShouldBeArgumentNullExceptionMessage = "InnerException should be System.ArgumentNullException.";
        private const string ContentShouldBeUnchangedMessage = "Content should be unchanged.";

        private XmlDocument document;
        private XmlNamespaceManager namespaceManager;
        private ProgramSettings settings;
        private ILogger logger;
        private IDocumentFactory documentFactory;

        [SetUp]
        public void Init()
        {
            this.document = new XmlDocument();
            this.document.LoadXml("<root />");

            this.namespaceManager = new XmlNamespaceManager(this.document.NameTable);
            this.settings = new ProgramSettings();

            var loggerMock = new Mock<ILogger>();
            this.logger = loggerMock.Object;

            var documentFactoryMock = new Mock<IDocumentFactory>();
            this.documentFactory = documentFactoryMock.Object;
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_WithDefaultCnstructor_ShouldReturnValidObject()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.IsNotNull(controller, "Controller should not be null.");
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithValidParameters_ShouldWork()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.namespaceManager, this.settings, this.logger).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, this.settings, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, this.settings, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, this.settings, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, this.settings, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            try
            {
                controller.Run(null, this.namespaceManager, this.settings, this.logger).Wait();
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
        public void ParseLowerTaxaController_RunWithNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, this.settings, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, this.settings, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullNamespaceManagerAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            try
            {
                controller.Run(this.document.DocumentElement, null, this.settings, this.logger).Wait();
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
        public void ParseLowerTaxaController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, this.namespaceManager, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, this.namespaceManager, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ParseLowerTaxaController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            try
            {
                controller.Run(this.document.DocumentElement, this.namespaceManager, null, this.logger).Wait();
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
        public void ParseLowerTaxaController_RunWithNullLoggerAndValidOtherParameters_ShouldWork()
        {
            var controller = new ParseLowerTaxaController(this.documentFactory);

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.namespaceManager, this.settings, null).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }
    }
}

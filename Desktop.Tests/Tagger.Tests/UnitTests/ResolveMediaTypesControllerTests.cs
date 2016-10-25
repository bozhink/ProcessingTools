namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;
    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Floats;

    [TestFixture]
    public class ResolveMediaTypesControllerTests
    {
        private const string CallShouldThrowSystemAggregateExceptionMessage = "Call should throw System.AggregateException.";
        private const string CallShouldThrowSystemArgumentNullExceptionMessage = "Call should throw System.ArgumentNullException.";
        private const string InnerExceptionShouldBeArgumentNullExceptionMessage = "InnerException should be System.ArgumentNullException.";
        private const string ContentShouldBeUnchangedMessage = "Content should be unchanged.";

        private XmlDocument document;
        private XmlNamespaceManager namespaceManager;
        private ProgramSettings settings;
        private ILogger logger;
        private IDocumentFactory documentFactory;
        private IMediaTypesResolver parser;

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

            var parserMock = new Mock<IMediaTypesResolver>();
            this.parser = parserMock.Object;
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_WithDefaultCnstructor_ShouldReturnValidObject()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.IsNotNull(controller, "Controller should not be null.");
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_WithNullService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var controller = new ResolveMediaTypesController(this.documentFactory, null);
                },
                CallShouldThrowSystemArgumentNullExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_WithNullService_ShouldThrowArgumentNullExceptionWithParamName()
        {
            try
            {
                var controller = new ResolveMediaTypesController(this.documentFactory, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType(), CallShouldThrowSystemArgumentNullExceptionMessage);

                Assert.AreEqual("parser", ((ArgumentNullException)e).ParamName, @"ParamName should be ""parser"".");
            }
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithValidParameters_ShouldWork()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.settings).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

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
        public void ResolveMediaTypesController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void ResolveMediaTypesController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new ResolveMediaTypesController(this.documentFactory, this.parser);

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
    }
}

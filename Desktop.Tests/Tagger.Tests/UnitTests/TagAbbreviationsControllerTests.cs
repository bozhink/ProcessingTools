namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;
    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Abbreviations;

    [TestFixture]
    public class TagAbbreviationsControllerTests
    {
        private const string CallShouldThrowSystemAggregateExceptionMessage = "Call should throw System.AggregateException.";
        private const string InnerExceptionShouldBeArgumentNullExceptionMessage = "InnerException should be System.ArgumentNullException.";
        private const string ContentShouldBeUnchangedMessage = "Content should be unchanged.";

        private XmlDocument document;
        private XmlNamespaceManager namespaceManager;
        private ProgramSettings settings;
        private ILogger logger;
        private IDocumentFactory documentFactory;
        private IAbbreviationsTagger abbreviationsTagger;

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

            var abbreviationsTaggerMock = new Mock<IAbbreviationsTagger>();
            this.abbreviationsTagger = abbreviationsTaggerMock.Object;
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_WithDefaultCnstructor_ShouldReturnValidObject()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.IsNotNull(controller, "Controller should not be null.");
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithValidParameters_ShouldWork()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.settings).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

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
        public void TagAbbreviationsController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagAbbreviationsController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new TagAbbreviationsController(this.documentFactory, this.abbreviationsTagger);

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

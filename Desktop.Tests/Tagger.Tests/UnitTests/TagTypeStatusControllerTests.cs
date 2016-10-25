namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;
    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    [TestFixture]
    public class TagTypeStatusControllerTests
    {
        private const string CallShouldThrowSystemAggregateExceptionMessage = "Call should throw System.AggregateException.";
        private const string CallShouldThrowSystemArgumentNullExceptionMessage = "Call should throw System.ArgumentNullException.";
        private const string InnerExceptionShouldBeArgumentNullExceptionMessage = "InnerException should be System.ArgumentNullException.";
        private const string ContentShouldBeUnchangedMessage = "Content should be unchanged.";

        private XmlDocument document;
        private XmlNamespaceManager namespaceManager;
        private ProgramSettings settings;
        private ILogger logger;

        private ITypeStatusDataMiner miner;
        private IDocumentFactory documentFactory;
        private IStringTagger tagger;

        [SetUp]
        public void Init()
        {
            this.document = new XmlDocument();
            this.document.LoadXml("<root />");

            this.namespaceManager = new XmlNamespaceManager(this.document.NameTable);
            this.settings = new ProgramSettings();

            var loggerMock = new Mock<ILogger>();
            this.logger = loggerMock.Object;

            var minerMock = new Mock<ITypeStatusDataMiner>();
            this.miner = minerMock.Object;

            var documentFactoryMock = new Mock<IDocumentFactory>();
            this.documentFactory = documentFactoryMock.Object;

            var taggerMock = new Mock<IStringTagger>();
            this.tagger = taggerMock.Object;
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_WithDefaultCnstructor_ShouldReturnValidObject()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.IsNotNull(controller, "Controller should not be null.");
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_WithNullService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var controller = new TagTypeStatusController(null, this.documentFactory, this.tagger);
                },
                CallShouldThrowSystemArgumentNullExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_WithNullService_ShouldThrowArgumentNullExceptionWithParamName()
        {
            try
            {
                var controller = new TagTypeStatusController(null, this.documentFactory, this.tagger);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType(), CallShouldThrowSystemArgumentNullExceptionMessage);

                Assert.AreEqual("miner", ((ArgumentNullException)e).ParamName, @"ParamName should be ""miner"".");
            }
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithValidParameters_ShouldWork()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.settings).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

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
        public void TagTypeStatusController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagTypeStatusController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new TagTypeStatusController(this.miner, this.documentFactory, this.tagger);

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

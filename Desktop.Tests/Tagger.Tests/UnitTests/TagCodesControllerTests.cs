namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;
    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;

    [TestFixture]
    public class TagCodesControllerTests
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
        private IBiorepositoriesInstitutionsDataMiner institutionsMiner;
        private IBiorepositoriesCollectionsDataMiner collectionsMiner;

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

            var institutionsMinerMock = new Mock<IBiorepositoriesInstitutionsDataMiner>();
            this.institutionsMiner = institutionsMinerMock.Object;

            var collectionsMinerMock = new Mock<IBiorepositoriesCollectionsDataMiner>();
            this.collectionsMiner = collectionsMinerMock.Object;
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_WithDefaultCnstructor_ShouldReturnValidObject()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.IsNotNull(controller, "Controller should not be null.");
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_WithNullInstitutionsMinerAndValidCollectionsMinerService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var controller = new TagCodesController(this.documentFactory, null, this.collectionsMiner, this.logger);
                },
                CallShouldThrowSystemArgumentNullExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_WithNullInstitutionsMinerAndValidCollectionsMinerService_ShouldThrowArgumentNullExceptionWithParamName()
        {
            try
            {
                var controller = new TagCodesController(this.documentFactory, null, this.collectionsMiner, this.logger);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType(), CallShouldThrowSystemArgumentNullExceptionMessage);

                Assert.AreEqual(nameof(this.institutionsMiner), ((ArgumentNullException)e).ParamName, $"ParamName should be '{nameof(this.institutionsMiner)}'.");
            }
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_WithNullCollectionsMinerAndValidInstitutionsMinerService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, null, this.logger);
                },
                CallShouldThrowSystemArgumentNullExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_WithNullCollectionsMinerAndValidInstitutionsMinerService_ShouldThrowArgumentNullExceptionWithParamName()
        {
            try
            {
                var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, null, this.logger);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(ArgumentNullException), e.GetType(), CallShouldThrowSystemArgumentNullExceptionMessage);

                Assert.AreEqual(nameof(this.collectionsMiner), ((ArgumentNullException)e).ParamName, $"ParamName should be '{nameof(this.collectionsMiner)}'.");
            }
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithValidParameters_ShouldWork()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.settings).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.settings).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

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
        public void TagCodesController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        [Timeout(500)]
        public void TagCodesController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new TagCodesController(this.documentFactory, this.institutionsMiner, this.collectionsMiner, this.logger);

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

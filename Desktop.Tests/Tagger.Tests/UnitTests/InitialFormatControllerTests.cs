namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using System;
    using System.Xml;

    using Controllers;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts;

    [TestFixture]
    public class InitialFormatControllerTests
    {
        private const string CallShouldThrowSystemAggregateExceptionMessage = "Call should throw System.AggregateException.";
        private const string InnerExceptionShouldBeArgumentNullExceptionMessage = "InnerException should be System.ArgumentNullException.";
        private const string ContentShouldBeUnchangedMessage = "Content should be unchanged.";

        private XmlDocument document;
        private XmlNamespaceManager namespaceManager;
        private ProgramSettings settings;
        private ILogger logger;

        [SetUp]
        public void Init()
        {
            this.document = new XmlDocument();
            this.document.LoadXml("<root />");

            this.namespaceManager = new XmlNamespaceManager(this.document.NameTable);
            this.settings = new ProgramSettings();

            var loggerMock = new Mock<ILogger>();
            this.logger = loggerMock.Object;
        }

        [Test]
        public void InitialFormatController_WithDefaultCnstructor_ShouldReturnValidObject()
        {
            var controller = new InitialFormatController();

            Assert.IsNotNull(controller, "Controller should not be null.");
        }

        [Test]
        public void InitialFormatController_RunWithValidParameters_ShouldWork()
        {
            var controller = new InitialFormatController();

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.namespaceManager, this.settings, this.logger).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, this.settings, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, this.settings, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, this.settings, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, this.settings, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, this.namespaceManager, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(null, null, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullContextAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new InitialFormatController();

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
        public void InitialFormatController_RunWithNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, this.settings, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullNamespaceManagerAndNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullNamespaceManagerAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, this.settings, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullNamespaceManagerAndNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, null, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullNamespaceManagerAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new InitialFormatController();

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
        public void InitialFormatController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, this.namespaceManager, null, this.logger).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullProgramSettingsAndNullLoggerAndValidOtherParameters_ShouldThrowAggregateException()
        {
            var controller = new InitialFormatController();

            Assert.Throws<AggregateException>(
                () => controller.Run(this.document.DocumentElement, this.namespaceManager, null, null).Wait(),
                CallShouldThrowSystemAggregateExceptionMessage);
        }

        [Test]
        public void InitialFormatController_RunWithNullProgramSettingsAndValidOtherParameters_ShouldThrowAggregateExceptionWithInnerArgumentNullException()
        {
            var controller = new InitialFormatController();

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
        public void InitialFormatController_RunWithNullLoggerAndValidOtherParameters_ShouldWork()
        {
            var controller = new InitialFormatController();

            string initialContent = this.document.OuterXml;

            controller.Run(this.document.DocumentElement, this.namespaceManager, this.settings, null).Wait();

            string finalContent = this.document.OuterXml;

            Assert.AreEqual(initialContent, finalContent, ContentShouldBeUnchangedMessage);
        }
    }
}

namespace ProcessingTools.Documents.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;

    [TestClass]
    public partial class XmlPresenterTests
    {
        private const int NumberOfInnerArgumentNullExceptions = 1;
        private const string NumberOfInnerExceptionsShouldBeMessage = "Number of inner Exceptions should be 1.";

        private const string ObjectShouldNotBeNullMessage = "Object should not be null.";
        private const string ServiceShouldBeInitializedMessage = "Service should be initialized.";
        private const string ServiceShouldBeSetCorrectlyMessage = "Service should be set correctly.";
        private const string ServiceMockShouldReturnTrueMessage = "Service mock should return true.";

        private const string ServiceParamName = "service";
        private const string UserIdParamName = "userId";
        private const string ArticleIdParamName = "articleId";
        private const string DocumentIdParamName = "documentId";
        private const string DocumentParamName = "document";
        private const string ContentParamName = "content";

        private const string ValidContent = "<article />";
        private const string InvalidContent = "Invalid content";

        private IDocumentsDataService service;

        private object userId;
        private object articleId;
        private object documentId;

        private DocumentServiceModel document;

        [TestInitialize]
        public void Initialize()
        {
            var serviceMock = new Mock<IDocumentsDataService>();
            serviceMock.Setup(s => s.Update(
                It.IsAny<object>(),
                It.IsAny<object>(),
                It.IsAny<DocumentServiceModel>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult<object>(true));

            this.service = serviceMock.Object;

            this.userId = Guid.NewGuid();
            this.articleId = Guid.NewGuid();
            this.documentId = Guid.NewGuid();

            this.document = new DocumentServiceModel();
        }

        [TestMethod]
        public void XmlPresenter_WithValidServiceInConstructor_ShouldReturnValidObject()
        {
            var presenter = new XmlPresenter(this.service);
            Assert.IsNotNull(presenter, ObjectShouldNotBeNullMessage);

            string fieldName = nameof(this.service);
            var field = presenter.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldValue = field?.GetValue(presenter);

            Assert.IsNotNull(fieldValue, ServiceShouldBeInitializedMessage);
            Assert.AreSame(this.service, fieldValue, ServiceShouldBeSetCorrectlyMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void XmlPresenter_WithNullServiceInConstructor_ShouldThrowArgumentNullException()
        {
            var presenter = new XmlPresenter(null);
        }

        [TestMethod]
        public void XmlPresenter_WithNullServiceInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var presenter = new XmlPresenter(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(
                    ServiceParamName,
                    e.ParamName,
                    this.GetParamNameShouldBeMessage(ServiceParamName));
            }
        }

        [TestMethod]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidContentWithNbsp_ShouldWork()
        {
            const string Content = "<p>&nbsp;</p>";
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, Content).Result;
            Assert.IsTrue((bool)result, ServiceMockShouldReturnTrueMessage);
        }

        private string GetParamNameShouldBeMessage(string paramName)
        {
            return $"ParamName should be '{paramName}'.";
        }
    }
}

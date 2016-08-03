namespace ProcessingTools.Documents.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;

    [TestClass]
    public class XmlPresenterTests
    {
        private const string ArticleIdParamName = "articleId";
        private const string ContentParamName = "content";
        private const string DocumentIdParamName = "documentId";
        private const string DocumentParamName = "document";
        private const string InvalidContent = "Invalid content";
        private const int NumberOfInnerArgumentNullExceptions = 1;
        private const string NumberOfInnerExceptionsShouldBeMessage = "Number of inner Exceptions should be 1.";

        private const string ObjectShouldNotBeNullMessage = "Object should not be null.";
        private const string ServiceMockShouldReturnTrueMessage = "Service mock should return true.";
        private const string ServiceParamName = "service";
        private const string ServiceShouldBeInitializedMessage = "Service should be initialized.";
        private const string ServiceShouldBeSetCorrectlyMessage = "Service should be set correctly.";
        private const string UserIdParamName = "userId";
        private const string ValidContent = "<article />";
        private object articleId;
        private DocumentServiceModel document;
        private object documentId;
        private IDocumentsDataService service;

        private object userId;

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
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidContentWithNbsp_ShouldWork()
        {
            const string Content = "<p>&nbsp;</p>";
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, Content).Result;
            Assert.IsTrue((bool)result, ServiceMockShouldReturnTrueMessage);
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
        public void XmlPresenter_WithValidServiceInConstructor_ShouldInitializeServiceField()
        {
            var presenter = new XmlPresenter(this.service);

            string fieldName = nameof(this.service);
            var field = presenter.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldValue = field?.GetValue(presenter);

            Assert.IsNotNull(fieldValue, ServiceShouldBeInitializedMessage);
            Assert.AreSame(this.service, fieldValue, ServiceShouldBeSetCorrectlyMessage);
        }

        [TestMethod]
        public void XmlPresenter_WithValidServiceInConstructor_ShouldReturnValidObject()
        {
            var presenter = new XmlPresenter(this.service);
            Assert.IsNotNull(presenter, ObjectShouldNotBeNullMessage);
        }

        #region GetHtmlTests

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_GetHtml_WithNullArticleId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, null, this.documentId).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.GetHtml(this.userId, null, this.documentId).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ArticleIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ArticleIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_GetHtml_WithNullDocumentId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, null).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullDocumentId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.GetHtml(this.userId, this.articleId, null).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    DocumentIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(DocumentIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_GetHtml_WithNullUserId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(null, this.articleId, this.documentId).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.GetHtml(null, this.articleId, this.documentId).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    UserIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(UserIdParamName));
            }
        }

        #endregion GetHtmlTests

        #region GetXmlTests

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_GetXml_WithNullArticleId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(this.userId, null, this.documentId).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.GetXml(this.userId, null, this.documentId).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ArticleIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ArticleIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_GetXml_WithNullDocumentId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(this.userId, this.articleId, null).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullDocumentId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.GetXml(this.userId, this.articleId, null).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    DocumentIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(DocumentIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_GetXml_WithNullUserId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(null, this.articleId, this.documentId).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.GetXml(null, this.articleId, this.documentId).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    UserIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(UserIdParamName));
            }
        }

        #endregion GetXmlTests

        #region SaveHtmlTests

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithEmptyContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, string.Empty).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithEmptyContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, this.document, string.Empty).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ContentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ContentParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithInvalidXmlContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, InvalidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithInvalidXmlContent_ShouldThrowAggregateExceptionWithXmlException()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, this.document, InvalidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is XmlException),
                    NumberOfInnerExceptionsShouldBeMessage);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithNullArticleId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, null, this.document, ValidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(this.userId, null, this.document, ValidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ArticleIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ArticleIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithNullContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, null).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, this.document, null).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ContentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ContentParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithNullDocument_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, null, ValidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullDocument_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, null, ValidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    DocumentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(DocumentParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithNullUserId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(null, this.articleId, this.document, ValidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(null, this.articleId, this.document, ValidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    UserIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(UserIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveHtml_WithWhitespaceContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(
                this.userId,
                this.articleId,
                this.document,
                @"                   ").Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithWhitespaceContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveHtml(
                    this.userId,
                    this.articleId,
                    this.document,
                    @"                        ").Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ContentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ContentParamName));
            }
        }

        #endregion SaveHtmlTests

        #region SaveXmlTests

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithEmptyContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(this.userId, this.articleId, this.document, string.Empty).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithEmptyContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(this.userId, this.articleId, this.document, string.Empty).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ContentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ContentParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithInvalidXmlContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(this.userId, this.articleId, this.document, InvalidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithInvalidXmlContent_ShouldThrowAggregateExceptionWithXmlException()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(this.userId, this.articleId, this.document, InvalidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is XmlException),
                    NumberOfInnerExceptionsShouldBeMessage);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithNullArticleId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(this.userId, null, this.document, ValidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(this.userId, null, this.document, ValidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ArticleIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ArticleIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithNullContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(this.userId, this.articleId, this.document, null).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(this.userId, this.articleId, this.document, null).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ContentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ContentParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithNullDocument_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(this.userId, this.articleId, null, ValidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullDocument_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(this.userId, this.articleId, null, ValidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    DocumentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(DocumentParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithNullUserId_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(null, this.articleId, this.document, ValidContent).Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(null, this.articleId, this.document, ValidContent).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    UserIdParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(UserIdParamName));
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(AggregateException), AllowDerivedTypes = false)]
        public void XmlPresenter_SaveXml_WithWhitespaceContent_ShouldThrowAggregateException()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveXml(
                this.userId,
                this.articleId,
                this.document,
                @"                    ").Result;
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithWhitespaceContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            var presenter = new XmlPresenter(this.service);
            try
            {
                var result = presenter.SaveXml(
                    this.userId,
                    this.articleId,
                    this.document,
                    @"                        ").Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(
                    NumberOfInnerArgumentNullExceptions,
                    e.InnerExceptions.Count(ex => ex is ArgumentNullException),
                    NumberOfInnerExceptionsShouldBeMessage);

                var argumentNullException = e.InnerExceptions.First(ex => ex is ArgumentNullException) as ArgumentNullException;

                Assert.AreEqual(
                    ContentParamName,
                    argumentNullException.ParamName,
                    this.GetParamNameShouldBeMessage(ContentParamName));
            }
        }

        #endregion SaveXmlTests

        private string GetParamNameShouldBeMessage(string paramName)
        {
            return $"ParamName should be '{paramName}'.";
        }
    }
}

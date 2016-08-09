namespace ProcessingTools.Documents.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;

    [TestClass]
    public class XmlPresenterTests
    {
        private const string ServiceParamName = "service";
        private const string UserIdParamName = "userId";
        private const string ValidContent = "<article />";

        private const string HtmlXmlElemName = "article";
        private const string HtmlDocumentElementName = "div";
        private const string XmlDocumentElementName = "article";
        private const string ElemNameAttributeName = "elem-name";

        private const string HtmlDocumentElementNameShouldBeDivMessage = "HTML DocumentElement name should be '" + HtmlDocumentElementName + "'";
        private const string XmlDocumentElementNameShouldBeDivMessage = "Xml DocumentElement name should be '" + XmlDocumentElementName + "'";
        private const string HtmlXmlElemNameShouldBeArticleMessage = "HTML xml elem-name should be '" + HtmlXmlElemName + "'";
        private const string HtmlXmlElemNameAttributeShouldNotBeNullMessage = "HTML xml elem-name should not be null";

        private const string ArticleIdParamName = "articleId";
        private const string ContentParamName = "content";
        private const string DocumentIdParamName = "documentId";
        private const string DocumentParamName = "document";
        private const string InvalidContent = "Invalid content";
        private const int NumberOfInnerArgumentNullExceptions = 1;
        private const string NumberOfInnerExceptionsShouldBeMessage = "Number of inner Exceptions should be 1";
        private const string TextContentShouldNotBeNullOrWhitespace = "Text content should not be null or whitespace";
        private const string GetReadShouldBeExecutedExactlyOnceMessage = "GetRead should be executed exactly once";

        private const string ObjectShouldNotBeNullMessage = "Object should not be null";
        private const string ServiceMockShouldReturnTrueMessage = "Service mock should return true";
        private const string ServiceShouldBeInitializedMessage = "Service should be initialized";
        private const string ServiceShouldBeSetCorrectlyMessage = "Service should be set correctly";

        private Mock<IDocumentsDataService> serviceMock;
        private IDocumentsDataService service;

        private DocumentServiceModel document;

        private object userId;
        private object articleId;
        private object documentId;

        [TestInitialize]
        public void Initialize()
        {
            this.serviceMock = new Mock<IDocumentsDataService>();
            this.serviceMock.Setup(s => s.Update(
                It.IsAny<object>(),
                It.IsAny<object>(),
                It.IsAny<DocumentServiceModel>(),
                It.IsAny<string>()))
                .Returns(Task.FromResult<object>(true));

            this.serviceMock.Setup(s => s.GetReader(
                It.IsAny<object>(),
                It.IsAny<object>(),
                It.IsAny<object>()))
                .Returns(() =>
                {
                    return Task.Run(() =>
                    {
                        byte[] bytesContent = Encoding.UTF8.GetBytes(ValidContent);
                        var xmlReader = XmlReader.Create(
                            new MemoryStream(bytesContent),
                            new XmlReaderSettings
                            {
                                Async = true
                            });

                        return xmlReader;
                    });
                });

            this.service = this.serviceMock.Object;

            this.userId = Guid.NewGuid();
            this.articleId = Guid.NewGuid();
            this.documentId = Guid.NewGuid();

            this.document = new DocumentServiceModel();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void XmlPresenter_Constructor_WithNullService_ShouldThrowArgumentNullException()
        {
            var presenter = new XmlPresenter(null);
        }

        [TestMethod]
        public void XmlPresenter_Constructor_WithNullService_ShouldThrowArgumentNullExceptionWithCorrectParamName()
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
        public void XmlPresenter_Constructor_WithValidService_ShouldInitializeServiceField()
        {
            var presenter = new XmlPresenter(this.service);

            string fieldName = nameof(this.service);

            var privateObject = new PrivateObject(presenter);
            var field = privateObject.GetField(fieldName);

            Assert.IsNotNull(field, ServiceShouldBeInitializedMessage);
            Assert.AreSame(this.service, field, ServiceShouldBeSetCorrectlyMessage);
        }

        [TestMethod]
        public void XmlPresenter_Constructor_WithValidService_ShouldNotThrow()
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

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldExecuteGetReaderOnce()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            this.serviceMock.Verify(s => s.GetReader(this.userId, this.articleId, this.documentId), Times.Once, GetReadShouldBeExecutedExactlyOnceMessage);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnNonEmptyContent()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            Assert.IsFalse(string.IsNullOrWhiteSpace(result), TextContentShouldNotBeNullOrWhitespace);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnValidXmlContent()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            Assert.IsFalse(string.IsNullOrWhiteSpace(xmlDocument.OuterXml), TextContentShouldNotBeNullOrWhitespace);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithValidDocumentElement()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            Assert.AreEqual(HtmlDocumentElementName, xmlDocument.DocumentElement.Name, HtmlDocumentElementNameShouldBeDivMessage);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithNonNullElemNameAttribute()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);
            var elemName = xmlDocument.DocumentElement.Attributes[ElemNameAttributeName];

            Assert.IsNotNull(elemName, HtmlXmlElemNameAttributeShouldNotBeNullMessage);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithArticleElemName()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);
            var elemName = xmlDocument.DocumentElement.Attributes[ElemNameAttributeName];

            Assert.AreEqual(HtmlXmlElemName, elemName.InnerText, HtmlXmlElemNameShouldBeArticleMessage);
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

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldExecuteGetReaderOnce()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            this.serviceMock.Verify(s => s.GetReader(this.userId, this.articleId, this.documentId), Times.Once, GetReadShouldBeExecutedExactlyOnceMessage);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldReturnNonEmptyContent()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            Assert.IsFalse(string.IsNullOrWhiteSpace(result), TextContentShouldNotBeNullOrWhitespace);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldReturnValidXmlContent()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            Assert.IsFalse(string.IsNullOrWhiteSpace(xmlDocument.OuterXml), TextContentShouldNotBeNullOrWhitespace);
        }

        [TestMethod]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldReturnXmlWithValidDocumentElement()
        {
            var presenter = new XmlPresenter(this.service);
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            Assert.AreEqual(XmlDocumentElementName, xmlDocument.DocumentElement.Name, XmlDocumentElementNameShouldBeDivMessage);
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



        [TestMethod]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidContentWithNbsp_ShouldWork()
        {
            const string Content = "<p>&nbsp;</p>";
            var presenter = new XmlPresenter(this.service);
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, Content).Result;
            Assert.IsTrue((bool)result, ServiceMockShouldReturnTrueMessage);
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

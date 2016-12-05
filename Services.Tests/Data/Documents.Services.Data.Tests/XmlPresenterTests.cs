using ProcessingTools.Documents.Services.Data.Services;

namespace ProcessingTools.Documents.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using Moq;
    using NUnit.Framework;

    [TestFixture(Category = "Unit tests", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
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
        private const int NumberOfInnerExceptions = 1;
        private const string NumberOfInnerExceptionsShouldBeMessage = "Number of inner Exceptions should be 1";
        private const string TextContentShouldNotBeNullOrWhitespace = "Text content should not be null or whitespace";
        private const string ServiceGetReaderShouldBeInvokedExactlyOnceMessage = "service.GetReader should be invoked exactly once";
        private const string ServiceUpdateShouldBeInvokedExactlyOnceMessage = "service.Update should be invoked exactly once";
        private const string ServiceGetReaderShouldNotBeInvokedMessage = "service.GetReader should not be invoked";
        private const string ServiceUpdateShouldNotBeInvokedMessage = "service.Update should not be invoked";

        private const string InnerExceptionShouldBeOfTypeXmlExceptionMessage = "Inner exception should be of type XmlException";
        private const string InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage = "Inner exception should be of type ArgumentNullException";

        private const string ObjectShouldNotBeNullMessage = "Object should not be null";
        private const string ServiceMockShouldReturnPassedContentForUpdateMethodMessage = @"Service mock should return passed content for update method";
        private const string ServiceShouldBeInitializedMessage = "Service should be initialized";
        private const string ServiceShouldBeSetCorrectlyMessage = "Service should be set correctly";

        private Mock<IDocumentsDataService> serviceMock;
        private IDocumentsDataService service;

        private DocumentServiceModel document;

        private object userId;
        private object articleId;
        private object documentId;

        [SetUp]
        public void TestInitialize()
        {
            this.serviceMock = new Mock<IDocumentsDataService>();
            this.serviceMock.Setup(s => s.UpdateContent(
                It.IsAny<object>(),
                It.IsAny<object>(),
                It.IsAny<DocumentServiceModel>(),
                It.IsAny<string>()))
                .Returns((object u, object a, object d, string c) => Task.FromResult<object>(c));

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

        [Test(Description = @"XmlPresenter.ctor with null service should throw ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        public void XmlPresenter_Constructor_WithNullService_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var presenter = new XmlPresenter(null);
            });

            Assert.AreEqual(
                ServiceParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ServiceParamName));
        }

        [Test(Description = @"XmlPresenter.ctor with valid service should not throw", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        public void XmlPresenter_Constructor_WithValidService_ShouldNotThow()
        {
            var presenter = new XmlPresenter(this.service);
            Assert.IsNotNull(presenter, ObjectShouldNotBeNullMessage);
        }

        [Test(Description = @"XmlPresenter.ctor with valid service should correctly initialize service field", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        public void XmlPresenter_Constructor_WithValidService_ShouldCorrectlyInitializeServiceField()
        {
            var presenter = new XmlPresenter(this.service);

            string fieldName = nameof(this.service);

            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(presenter);
            var field = privateObject.GetField(fieldName);

            Assert.IsNotNull(field, ServiceShouldBeInitializedMessage);
            Assert.AreSame(this.service, field, ServiceShouldBeSetCorrectlyMessage);
        }

        #region GetHtmlTests

        [Test(Description = @"XmlPresenter.GetHtml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetHtml(this.userId, null, this.documentId).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                ArticleIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XmlPresenter.GetHtml with null articleId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullArticleId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetHtml(this.userId, null, this.documentId).Result;
            });

            this.serviceMock.Verify(s => s.GetReader(this.userId, null, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.GetHtml with null documentId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullDocumentId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetHtml(this.userId, this.articleId, null).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                DocumentIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(DocumentIdParamName));
        }

        [Test(Description = @"XmlPresenter.GetHtml with null documentId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullDocumentId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetHtml(this.userId, this.articleId, null).Result;
            });

            this.serviceMock.Verify(s => s.GetReader(this.userId, this.articleId, null), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.GetHtml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetHtml(null, this.articleId, this.documentId).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                UserIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XmlPresenter.GetHtml with null userId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithNullUserId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetHtml(null, this.articleId, this.documentId).Result;
            });

            this.serviceMock.Verify(s => s.GetReader(null, this.articleId, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.GetHtml with valid parameters should invoke service.GetReader exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldInvokeServiceGetReaderExactlyOnce()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            // Assert
            this.serviceMock.Verify(s => s.GetReader(this.userId, this.articleId, this.documentId), Times.Once, ServiceGetReaderShouldBeInvokedExactlyOnceMessage);
        }

        [Test(Description = @"XmlPresenter.GetHtml with valid parameters should return non-empty content", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnNonEmptyContent()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XmlPresenter.GetHtml with valid parameters should return valid xml content", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnValidXmlContent()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(xmlDocument.OuterXml), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XmlPresenter.GetHtml with valid parameters should return xml with valid DocumentElement", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithValidDocumentElement()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.AreEqual(HtmlDocumentElementName, xmlDocument.DocumentElement.Name, HtmlDocumentElementNameShouldBeDivMessage);
        }

        [Test(Description = @"XmlPresenter.GetHtml with valid parameters should return xml with non-null elem-name attribute", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithNonNullElemNameAttribute()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            var elemName = xmlDocument.DocumentElement.Attributes[ElemNameAttributeName];

            // Assert
            Assert.IsNotNull(elemName, HtmlXmlElemNameAttributeShouldNotBeNullMessage);
        }

        [Test(Description = @"XmlPresenter.GetHtml with valid parameters should return xml with elem-name=""article""", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithArticleElemName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetHtml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            var elemName = xmlDocument.DocumentElement.Attributes[ElemNameAttributeName];

            // Assert
            Assert.AreEqual(HtmlXmlElemName, elemName.InnerText, HtmlXmlElemNameShouldBeArticleMessage);
        }

        #endregion GetHtmlTests

        #region GetXmlTests

        [Test(Description = @"XmlPresenter.GetXml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetXml(this.userId, null, this.documentId).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                ArticleIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XmlPresenter.GetXml with null articleId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullArticleId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetXml(this.userId, null, this.documentId).Result;
            });

            this.serviceMock.Verify(s => s.GetReader(this.userId, null, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.GetXml with null documentId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullDocumentId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetXml(this.userId, this.articleId, null).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                DocumentIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(DocumentIdParamName));
        }

        [Test(Description = @"XmlPresenter.GetXml with null documentId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullDocumentId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetXml(this.userId, this.articleId, null).Result;
            });

            this.serviceMock.Verify(s => s.GetReader(this.userId, this.articleId, null), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.GetXml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetXml(null, this.articleId, this.documentId).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                UserIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XmlPresenter.GetXml with null userId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithNullUserId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.GetXml(null, this.articleId, this.documentId).Result;
            });

            this.serviceMock.Verify(s => s.GetReader(null, this.articleId, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.GetXml with valid parameters should invoke service.GetReader exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldInvokeServiceGetReaderExactlyOnce()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            // Assert
            this.serviceMock.Verify(s => s.GetReader(this.userId, this.articleId, this.documentId), Times.Once, ServiceGetReaderShouldBeInvokedExactlyOnceMessage);
        }

        [Test(Description = @"XmlPresenter.GetXml with valid parameters should return non-empty content", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldReturnNonEmptyContent()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XmlPresenter.GetXml with valid parameters should return valid xml content", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldReturnValidXmlContent()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(xmlDocument.OuterXml), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XmlPresenter.GetXml with valid parameters should return xml with valid DocumentElement", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_GetXml_WithValidParameters_ShouldReturnXmlWithValidDocumentElement()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.GetXml(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.AreEqual(XmlDocumentElementName, xmlDocument.DocumentElement.Name, XmlDocumentElementNameShouldBeDivMessage);
        }

        #endregion GetXmlTests

        #region SaveHtmlTests

        [TestCase("", Description = @"XmlPresenter.SaveHtml with empty content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(null, Description = @"XmlPresenter.SaveHtml with null content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"                   ", Description = @"XmlPresenter.SaveHtml with whitespace content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithEmptyContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                ContentParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(ContentParamName));
        }

        [TestCase("", Description = @"XmlPresenter.SaveHtml with empty content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(null, Description = @"XmlPresenter.SaveHtml with null content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"                   ", Description = @"XmlPresenter.SaveHtml with whitespace should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithEmptyContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(InvalidContent, Description = @"XmlPresenter.SaveHtml with invalid xml content should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithInvalidXmlContent_ShouldThrowAggregateExceptionWithXmlException(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<XmlException>(
                innerException,
                InnerExceptionShouldBeOfTypeXmlExceptionMessage);
        }

        [TestCase(InvalidContent, Description = @"XmlPresenter.SaveHtml with invalid xml content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithInvalidXmlContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.SaveHtml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveHtml(this.userId, null, this.document, ValidContent).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                ArticleIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XmlPresenter.SaveHtml with null articleId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullArticleId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtml(this.userId, null, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, null, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.SaveHtml with null document should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullDocument_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, null, ValidContent).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                DocumentParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(DocumentParamName));
        }

        [Test(Description = @"XmlPresenter.SaveHtml with null document should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullDocument_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtml(this.userId, this.articleId, null, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, null, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.SaveHtml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveHtml(null, this.articleId, this.document, ValidContent).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                UserIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XmlPresenter.SaveHtml with null userId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveHtml_WithNullUserId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtml(null, this.articleId, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(null, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p>1</p>", Description = @"XmlPresenter.SaveHtml with valid html content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<?xml version=""1.0"" encoding=""utf-8""?><sec><title>1</title></sec>", Description = @"XmlPresenter.SaveHtml with valid html content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidHtmlContent_ShouldWork(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;

            // Assert
            Assert.AreEqual(expectedResult, result, ServiceMockShouldReturnPassedContentForUpdateMethodMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p>1</p>", Description = @"XmlPresenter.SaveHtml with valid html content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<?xml version=""1.0"" encoding=""utf-8""?><sec><title>1</title></sec>", Description = @"XmlPresenter.SaveHtml with valid html content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidHtmlContent_ShouldInvokeServiceUpdateExactlyOnce(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;

            // Assert
            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, expectedResult), Times.Once, ServiceUpdateShouldBeInvokedExactlyOnceMessage);
        }

        [TestCase(@"<p>1</p>", Description = @"XmlPresenter.SaveHtml with valid html content ""<p>1</p>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<p>&nbsp;</p>", Description = @"XmlPresenter.SaveHtml with valid html content ""<p>&nbsp;</p>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<p> </p>", Description = @"XmlPresenter.SaveHtml with valid html content ""<p> </p>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>", Description = @"XmlPresenter.SaveHtml with valid html content ""<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithIncorrectlyProcessibleValidHtmlContent_ShouldThrowAggregateExceptionWithXmlException(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<XmlException>(
                innerException,
                InnerExceptionShouldBeOfTypeXmlExceptionMessage);
        }

        [TestCase(@"<p>1</p>", Description = @"XmlPresenter.SaveHtml with valid html content ""<p>1</p>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<p>&nbsp;</p>", Description = @"XmlPresenter.SaveHtml with valid html content ""<p>&nbsp;</p>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<p> </p>", Description = @"XmlPresenter.SaveHtml with valid html content ""<p> </p>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>", Description = @"XmlPresenter.SaveHtml with valid html content ""<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithIncorrectlyProcessibleValidHtmlContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            // Assert
            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(@"<p elem-name=""p"">&nbsp;</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p> </p>", Description = @"XmlPresenter.SaveHtml with valid html content with &nbsp; should work", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidHtmlContentWithNbsp_ShouldWork(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;

            // Assert
            Assert.AreEqual(expectedResult, result, ServiceMockShouldReturnPassedContentForUpdateMethodMessage);
        }

        [TestCase(@"<p elem-name=""p"">&nbsp;</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p> </p>", Description = @"XmlPresenter.SaveHtml with valid html content with &nbsp; should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveHtml_WithValidHtmlContentWithNbsp_ShouldInvokeServiceUpdateExactlyOnce(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.SaveHtml(this.userId, this.articleId, this.document, content).Result;

            // Assert
            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, expectedResult), Times.Once, ServiceUpdateShouldBeInvokedExactlyOnceMessage);
        }

        #endregion SaveHtmlTests

        #region SaveXmlTests

        [TestCase("", Description = @"XmlPresenter.SaveXml with empty content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(null, Description = @"XmlPresenter.SaveXml with null content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"                   ", Description = @"XmlPresenter.SaveXml with whitespace content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithEmptyContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveXml(this.userId, this.articleId, this.document, content).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                ContentParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(ContentParamName));
        }

        [TestCase("", Description = @"XmlPresenter.SaveXml with empty content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(null, Description = @"XmlPresenter.SaveXml with null content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"                   ", Description = @"XmlPresenter.SaveXml with whitespace should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithEmptyContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXml(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(InvalidContent, Description = @"XmlPresenter.SaveXml with invalid xml content should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithInvalidXmlContent_ShouldThrowAggregateExceptionWithXmlException(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveXml(this.userId, this.articleId, this.document, content).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<XmlException>(
                innerException,
                InnerExceptionShouldBeOfTypeXmlExceptionMessage);
        }

        [TestCase(InvalidContent, Description = @"XmlPresenter.SaveXml with invalid xml content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithInvalidXmlContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXml(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.SaveXml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveXml(this.userId, null, this.document, ValidContent).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                ArticleIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XmlPresenter.SaveXml with null articleId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullArticleId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXml(this.userId, null, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, null, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.SaveXml with null document should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullDocument_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveXml(this.userId, this.articleId, null, ValidContent).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                DocumentParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(DocumentParamName));
        }

        [Test(Description = @"XmlPresenter.SaveXml with null document should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullDocument_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXml(this.userId, this.articleId, null, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, null, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XmlPresenter.SaveXml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            var exception = Assert.Throws<AggregateException>(() =>
            {
                var result = presenter.SaveXml(null, this.articleId, this.document, ValidContent).Result;
            });

            Assert.AreEqual(
                NumberOfInnerExceptions,
                exception.InnerExceptions.Count(),
                NumberOfInnerExceptionsShouldBeMessage);

            var innerException = exception.InnerExceptions.Single();
            Assert.IsInstanceOf<ArgumentNullException>(
                innerException,
                InnerExceptionShouldBeOfTypeArgumentNullExceptionMessage);

            var argumentNullException = innerException as ArgumentNullException;
            Assert.AreEqual(
                UserIdParamName,
                argumentNullException.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XmlPresenter.SaveXml with null userId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(1000)]
        public void XmlPresenter_SaveXml_WithNullUserId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXml(null, this.articleId, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContent(null, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<p elem-name=""p"">1</p>", Description = @"XmlPresenter.SaveXml with valid xml content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", Description = @"XmlPresenter.SaveXml with valid xml content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveXml_WithValidHtmlContent_ShouldWork(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.SaveXml(this.userId, this.articleId, this.document, content).Result;

            // Assert
            Assert.AreEqual(expectedResult, result, ServiceMockShouldReturnPassedContentForUpdateMethodMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<p elem-name=""p"">1</p>", Description = @"XmlPresenter.SaveXml with valid xml content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", Description = @"XmlPresenter.SaveXml with valid xml content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XmlPresenter))]
        [Timeout(10000)]
        public void XmlPresenter_SaveXml_WithValidHtmlContent_ShouldInvokeServiceUpdateExactlyOnce(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XmlPresenter(this.service);

            // Act
            var result = presenter.SaveXml(this.userId, this.articleId, this.document, content).Result;

            // Assert
            this.serviceMock.Verify(s => s.UpdateContent(this.userId, this.articleId, this.document, expectedResult), Times.Once, ServiceUpdateShouldBeInvokedExactlyOnceMessage);
        }

        #endregion SaveXmlTests

        private string GetParamNameShouldBeMessage(string paramName)
        {
            return $"ParamName should be '{paramName}'.";
        }
    }
}

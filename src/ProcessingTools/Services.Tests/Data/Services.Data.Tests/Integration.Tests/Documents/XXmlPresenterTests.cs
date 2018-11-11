﻿namespace ProcessingTools.Documents.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Documents.Services.Data.Services;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Models.Data.Documents;
    using ProcessingTools.Services.Xml;

    [TestFixture(Category = "Integration tests", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
    public class XXmlPresenterTests
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
        private const string TextContentShouldNotBeNullOrWhitespace = "Text content should not be null or whitespace";
        private const string ServiceGetReaderShouldBeInvokedExactlyOnceMessage = "service.GetReader should be invoked exactly once";
        private const string ServiceUpdateShouldBeInvokedExactlyOnceMessage = "service.Update should be invoked exactly once";
        private const string ServiceGetReaderShouldNotBeInvokedMessage = "service.GetReader should not be invoked";
        private const string ServiceUpdateShouldNotBeInvokedMessage = "service.Update should not be invoked";

        private const string ObjectShouldNotBeNullMessage = "Object should not be null";
        private const string ServiceMockShouldReturnPassedContentForUpdateMethodMessage = @"Service mock should return passed content for update method";
        private const string ServiceShouldBeInitializedMessage = "Service should be initialized";
        private const string ServiceShouldBeSetCorrectlyMessage = "Service should be set correctly";

        private Mock<IXDocumentsDataService> serviceMock;
        private IXDocumentsDataService service;

        private Document document;

        private object userId;
        private object articleId;
        private object documentId;

        private Mock<IDocumentsFormatTransformersFactory> transformerFactoryMock;

        [SetUp]
        public void TestInitialize()
        {
            this.serviceMock = new Mock<IXDocumentsDataService>();
            this.serviceMock.Setup(s => s.UpdateContentAsync(
                It.IsAny<object>(),
                It.IsAny<object>(),
                It.IsAny<Document>(),
                It.IsAny<string>()))
                .Returns((object u, object a, object d, string c) => Task.FromResult<object>(c));

            this.serviceMock.Setup(s => s.GetReaderAsync(
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
                            new XmlReaderSettings { Async = true });

                        return xmlReader;
                    });
                });

            this.service = this.serviceMock.Object;

            var xslCache = new XslTransformCacheFromFile();
            var xmlReadService = new XmlReadService();
            var htmlToXmlTransformer = new XslTransformerFromFile(AppSettings.FormatHtmlToXmlXslFileName, xslCache, xmlReadService);
            var xmlToHtmlTransformer = new XslTransformerFromFile(AppSettings.FormatXmlToHtmlXslFileName, xslCache, xmlReadService);

            this.transformerFactoryMock = new Mock<IDocumentsFormatTransformersFactory>();
            this.transformerFactoryMock
                .Setup(f => f.GetFormatHtmlToXmlTransformer())
                .Returns(htmlToXmlTransformer);
            this.transformerFactoryMock
                .Setup(f => f.GetFormatXmlToHtmlTransformer())
                .Returns(xmlToHtmlTransformer);

            this.userId = Guid.NewGuid();
            this.articleId = Guid.NewGuid();
            this.documentId = Guid.NewGuid();

            this.document = new Document();
        }

        // TODO: test factory
        [Test(Description = @"XXmlPresenter.ctor with null service should throw ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        public void XXmlPresenter_Constructor_WithNullService_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new XXmlPresenter(null, this.transformerFactoryMock.Object);
            });

            Assert.AreEqual(
                ServiceParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ServiceParamName));
        }

        [Test(Description = @"XXmlPresenter.ctor with valid service should not throw", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        public void XXmlPresenter_Constructor_WithValidService_ShouldNotThrow()
        {
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);
            Assert.IsNotNull(presenter, ObjectShouldNotBeNullMessage);
        }

        [Test(Description = @"XXmlPresenter.ctor with valid service should correctly initialize service field", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        public void XXmlPresenter_Constructor_WithValidService_ShouldCorrectlyInitializeServiceField()
        {
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            string fieldName = nameof(this.service);

            var privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(presenter);
            var field = privateObject.GetField(fieldName);

            Assert.IsNotNull(field, ServiceShouldBeInitializedMessage);
            Assert.AreSame(this.service, field, ServiceShouldBeSetCorrectlyMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetHtmlAsync(this.userId, null, this.documentId).ConfigureAwait(false);
            });

            Assert.AreEqual(
                ArticleIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XXmlPresenter.GetHtml with null articleId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithNullArticleId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetHtmlAsync(this.userId, null, this.documentId).ConfigureAwait(false);
            });

            this.serviceMock.Verify(s => s.GetReaderAsync(this.userId, null, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with null documentId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithNullDocumentId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetHtmlAsync(this.userId, this.articleId, null).ConfigureAwait(false);
            });

            Assert.AreEqual(
                DocumentIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(DocumentIdParamName));
        }

        [Test(Description = @"XXmlPresenter.GetHtml with null documentId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithNullDocumentId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetHtmlAsync(this.userId, this.articleId, null).ConfigureAwait(false);
            });

            this.serviceMock.Verify(s => s.GetReaderAsync(this.userId, this.articleId, null), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetHtmlAsync(null, this.articleId, this.documentId).ConfigureAwait(false);
            });

            Assert.AreEqual(
                UserIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XXmlPresenter.GetHtml with null userId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithNullUserId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetHtmlAsync(null, this.articleId, this.documentId).ConfigureAwait(false);
            });

            this.serviceMock.Verify(s => s.GetReaderAsync(null, this.articleId, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with valid parameters should invoke service.GetReader exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public async Task XXmlPresenter_GetHtml_WithValidParameters_ShouldInvokeServiceGetReaderExactlyOnce()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            await presenter.GetHtmlAsync(this.userId, this.articleId, this.documentId).ConfigureAwait(false);

            // Assert
            this.serviceMock.Verify(s => s.GetReaderAsync(this.userId, this.articleId, this.documentId), Times.Once, ServiceGetReaderShouldBeInvokedExactlyOnceMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with valid parameters should return non-empty content", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithValidParameters_ShouldReturnNonEmptyContent()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetHtmlAsync(this.userId, this.articleId, this.documentId).Result;

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with valid parameters should return valid xml content", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithValidParameters_ShouldReturnValidXmlContent()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetHtmlAsync(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(xmlDocument.OuterXml), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with valid parameters should return xml with valid DocumentElement", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithValidDocumentElement()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetHtmlAsync(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.AreEqual(HtmlDocumentElementName, xmlDocument.DocumentElement.Name, HtmlDocumentElementNameShouldBeDivMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with valid parameters should return xml with non-null elem-name attribute", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithNonNullElemNameAttribute()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetHtmlAsync(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            var elemName = xmlDocument.DocumentElement.Attributes[ElemNameAttributeName];

            // Assert
            Assert.IsNotNull(elemName, HtmlXmlElemNameAttributeShouldNotBeNullMessage);
        }

        [Test(Description = @"XXmlPresenter.GetHtml with valid parameters should return xml with elem-name=""article""", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetHtml_WithValidParameters_ShouldReturnXmlWithArticleElemName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetHtmlAsync(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            var elemName = xmlDocument.DocumentElement.Attributes[ElemNameAttributeName];

            // Assert
            Assert.AreEqual(HtmlXmlElemName, elemName.InnerText, HtmlXmlElemNameShouldBeArticleMessage);
        }

        [Test(Description = @"XXmlPresenter.GetXml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetXmlAsync(this.userId, null, this.documentId).ConfigureAwait(false);
            });

            Assert.AreEqual(
                ArticleIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XXmlPresenter.GetXml with null articleId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithNullArticleId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetXmlAsync(this.userId, null, this.documentId).ConfigureAwait(false);
            });

            this.serviceMock.Verify(s => s.GetReaderAsync(this.userId, null, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.GetXml with null documentId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithNullDocumentId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetXmlAsync(this.userId, this.articleId, null).ConfigureAwait(false);
            });

            Assert.AreEqual(
                DocumentIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(DocumentIdParamName));
        }

        [Test(Description = @"XXmlPresenter.GetXml with null documentId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithNullDocumentId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetXmlAsync(this.userId, this.articleId, null).ConfigureAwait(false);
            });

            this.serviceMock.Verify(s => s.GetReaderAsync(this.userId, this.articleId, null), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.GetXml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetXmlAsync(null, this.articleId, this.documentId).ConfigureAwait(false);
            });

            Assert.AreEqual(
                UserIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XXmlPresenter.GetXml with null userId should not invoke service.GetReader", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithNullUserId_ShouldNotInvokeServiceGetReader()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.GetXmlAsync(null, this.articleId, this.documentId).ConfigureAwait(false);
            });

            this.serviceMock.Verify(s => s.GetReaderAsync(null, this.articleId, this.documentId), Times.Never, ServiceGetReaderShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.GetXml with valid parameters should invoke service.GetReader exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public async Task XXmlPresenter_GetXml_WithValidParameters_ShouldInvokeServiceGetReaderExactlyOnce()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            await presenter.GetXmlAsync(this.userId, this.articleId, this.documentId).ConfigureAwait(false);

            // Assert
            this.serviceMock.Verify(s => s.GetReaderAsync(this.userId, this.articleId, this.documentId), Times.Once, ServiceGetReaderShouldBeInvokedExactlyOnceMessage);
        }

        [Test(Description = @"XXmlPresenter.GetXml with valid parameters should return non-empty content", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithValidParameters_ShouldReturnNonEmptyContent()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetXmlAsync(this.userId, this.articleId, this.documentId).Result;

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XXmlPresenter.GetXml with valid parameters should return valid xml content", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithValidParameters_ShouldReturnValidXmlContent()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetXmlAsync(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(xmlDocument.OuterXml), TextContentShouldNotBeNullOrWhitespace);
        }

        [Test(Description = @"XXmlPresenter.GetXml with valid parameters should return xml with valid DocumentElement", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_GetXml_WithValidParameters_ShouldReturnXmlWithValidDocumentElement()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.GetXmlAsync(this.userId, this.articleId, this.documentId).Result;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            // Assert
            Assert.AreEqual(XmlDocumentElementName, xmlDocument.DocumentElement.Name, XmlDocumentElementNameShouldBeDivMessage);
        }

        [TestCase("", Description = @"XXmlPresenter.SaveHtml with empty content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(null, Description = @"XXmlPresenter.SaveHtml with null content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"                   ", Description = @"XXmlPresenter.SaveHtml with whitespace content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithEmptyContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);
            });

            Assert.AreEqual(
                ContentParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ContentParamName));
        }

        [TestCase("", Description = @"XXmlPresenter.SaveHtml with empty content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(null, Description = @"XXmlPresenter.SaveHtml with null content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"                   ", Description = @"XXmlPresenter.SaveHtml with whitespace should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithEmptyContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(InvalidContent, Description = @"XXmlPresenter.SaveHtml with invalid xml content should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithInvalidXmlContent_ShouldThrowAggregateExceptionWithXmlException(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<XmlException>(async () =>
            {
                await presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);
            });
        }

        [TestCase(InvalidContent, Description = @"XXmlPresenter.SaveHtml with invalid xml content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithInvalidXmlContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.SaveHtml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveHtmlAsync(this.userId, null, this.document, ValidContent).ConfigureAwait(false);
            });

            Assert.AreEqual(
                ArticleIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XXmlPresenter.SaveHtml with null articleId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithNullArticleId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtmlAsync(this.userId, null, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, null, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.SaveHtml with null document should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithNullDocument_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveHtmlAsync(this.userId, this.articleId, null, ValidContent).ConfigureAwait(false);
            });

            Assert.AreEqual(
                DocumentParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(DocumentParamName));
        }

        [Test(Description = @"XXmlPresenter.SaveHtml with null document should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithNullDocument_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtmlAsync(this.userId, this.articleId, null, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, null, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.SaveHtml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveHtmlAsync(null, this.articleId, this.document, ValidContent).ConfigureAwait(false);
            });

            Assert.AreEqual(
                UserIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XXmlPresenter.SaveHtml with null userId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveHtml_WithNullUserId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtmlAsync(null, this.articleId, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(null, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p>1</p>", Description = @"XXmlPresenter.SaveHtml with valid html content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<?xml version=""1.0"" encoding=""utf-8""?><sec><title>1</title></sec>", Description = @"XXmlPresenter.SaveHtml with valid html content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public void XXmlPresenter_SaveHtml_WithValidHtmlContent_ShouldWork(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).Result;

            // Assert
            Assert.AreEqual(expectedResult, result, ServiceMockShouldReturnPassedContentForUpdateMethodMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p>1</p>", Description = @"XXmlPresenter.SaveHtml with valid html content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<?xml version=""1.0"" encoding=""utf-8""?><sec><title>1</title></sec>", Description = @"XXmlPresenter.SaveHtml with valid html content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public async Task XXmlPresenter_SaveHtml_WithValidHtmlContent_ShouldInvokeServiceUpdateExactlyOnce(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            await presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);

            // Assert
            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, expectedResult), Times.Once, ServiceUpdateShouldBeInvokedExactlyOnceMessage);
        }

        [TestCase(@"<p>1</p>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<p>1</p>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<p>&nbsp;</p>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<p>&nbsp;</p>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<p> </p>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<p> </p>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>"" should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public void XXmlPresenter_SaveHtml_WithIncorrectlyProcessibleValidHtmlContent_ShouldThrowAggregateExceptionWithXmlException(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<XmlException>(async () =>
            {
                await presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);
            });
        }

        [TestCase(@"<p>1</p>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<p>1</p>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<p>&nbsp;</p>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<p>&nbsp;</p>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<p> </p>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<p> </p>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>", Description = @"XXmlPresenter.SaveHtml with valid html content ""<div><h1 elem-name=""title"">1</h1><p elem-name=""p"">2</p></div>"" should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public void XXmlPresenter_SaveHtml_WithIncorrectlyProcessibleValidHtmlContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            // Assert
            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(@"<p elem-name=""p"">&nbsp;</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p> </p>", Description = @"XXmlPresenter.SaveHtml with valid html content with &nbsp; should work", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public void XXmlPresenter_SaveHtml_WithValidHtmlContentWithNbsp_ShouldWork(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).Result;

            // Assert
            Assert.AreEqual(expectedResult, result, ServiceMockShouldReturnPassedContentForUpdateMethodMessage);
        }

        [TestCase(@"<p elem-name=""p"">&nbsp;</p>", @"<?xml version=""1.0"" encoding=""utf-8""?><p> </p>", Description = @"XXmlPresenter.SaveHtml with valid html content with &nbsp; should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public async Task XXmlPresenter_SaveHtml_WithValidHtmlContentWithNbsp_ShouldInvokeServiceUpdateExactlyOnce(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            await presenter.SaveHtmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);

            // Assert
            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, expectedResult), Times.Once, ServiceUpdateShouldBeInvokedExactlyOnceMessage);
        }

        [TestCase("", Description = @"XXmlPresenter.SaveXml with empty content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(null, Description = @"XXmlPresenter.SaveXml with null content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"                   ", Description = @"XXmlPresenter.SaveXml with whitespace content should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithEmptyContent_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveXmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);
            });

            Assert.AreEqual(
                ContentParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ContentParamName));
        }

        [TestCase("", Description = @"XXmlPresenter.SaveXml with empty content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(null, Description = @"XXmlPresenter.SaveXml with null content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"                   ", Description = @"XXmlPresenter.SaveXml with whitespace should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithEmptyContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXmlAsync(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(InvalidContent, Description = @"XXmlPresenter.SaveXml with invalid xml content should throw AggregateException with inner XmlException", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithInvalidXmlContent_ShouldThrowAggregateExceptionWithXmlException(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            Assert.ThrowsAsync<XmlException>(async () =>
            {
                await presenter.SaveXmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);
            });
        }

        [TestCase(InvalidContent, Description = @"XXmlPresenter.SaveXml with invalid xml content should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithInvalidXmlContent_ShouldNotInvokeServiceUpdate(string content)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXmlAsync(this.userId, this.articleId, this.document, content).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.SaveXml with null articleId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithNullArticleId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveXmlAsync(this.userId, null, this.document, ValidContent).ConfigureAwait(false);
            });

            Assert.AreEqual(
                ArticleIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(ArticleIdParamName));
        }

        [Test(Description = @"XXmlPresenter.SaveXml with null articleId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithNullArticleId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXmlAsync(this.userId, null, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, null, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.SaveXml with null document should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithNullDocument_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveXmlAsync(this.userId, this.articleId, null, ValidContent).ConfigureAwait(false);
            });

            Assert.AreEqual(
                DocumentParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(DocumentParamName));
        }

        [Test(Description = @"XXmlPresenter.SaveXml with null document should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithNullDocument_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXmlAsync(this.userId, this.articleId, null, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, null, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [Test(Description = @"XXmlPresenter.SaveXml with null userId should throw AggregateException with inner ArgumentNullException with correct ParamName", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithNullUserId_ShouldThrowAggregateExceptionWithArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await presenter.SaveXmlAsync(null, this.articleId, this.document, ValidContent).ConfigureAwait(false);
            });

            Assert.AreEqual(
                UserIdParamName,
                exception.ParamName,
                this.GetParamNameShouldBeMessage(UserIdParamName));
        }

        [Test(Description = @"XXmlPresenter.SaveXml with null userId should not invoke service.Update", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(1000)]
        public void XXmlPresenter_SaveXml_WithNullUserId_ShouldNotInvokeServiceUpdate()
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act + Assert
            string result = null;
            Assert.Throws<AggregateException>(() =>
            {
                result = presenter.SaveXmlAsync(null, this.articleId, this.document, ValidContent).Result.ToString();
            });

            this.serviceMock.Verify(s => s.UpdateContentAsync(null, this.articleId, this.document, result), Times.Never, ServiceUpdateShouldNotBeInvokedMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<p elem-name=""p"">1</p>", Description = @"XXmlPresenter.SaveXml with valid xml content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", Description = @"XXmlPresenter.SaveXml with valid xml content should work", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public void XXmlPresenter_SaveXml_WithValidHtmlContent_ShouldWork(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            var result = presenter.SaveXmlAsync(this.userId, this.articleId, this.document, content).Result;

            // Assert
            Assert.AreEqual(expectedResult, result, ServiceMockShouldReturnPassedContentForUpdateMethodMessage);
        }

        [TestCase(@"<p elem-name=""p"">1</p>", @"<p elem-name=""p"">1</p>", Description = @"XXmlPresenter.SaveXml with valid xml content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [TestCase(@"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", @"<div elem-name=""sec""><h1 elem-name=""title"">1</h1></div>", Description = @"XXmlPresenter.SaveXml with valid xml content should invoke service.Update exactly once", Author = "Bozhin Karaivanov", TestOf = typeof(XXmlPresenter))]
        [Timeout(10000)]
        public async Task XXmlPresenter_SaveXml_WithValidHtmlContent_ShouldInvokeServiceUpdateExactlyOnce(string content, string expectedResult)
        {
            // Arrange
            var presenter = new XXmlPresenter(this.service, this.transformerFactoryMock.Object);

            // Act
            await presenter.SaveXmlAsync(this.userId, this.articleId, this.document, content).ConfigureAwait(false);

            // Assert
            this.serviceMock.Verify(s => s.UpdateContentAsync(this.userId, this.articleId, this.document, expectedResult), Times.Once, ServiceUpdateShouldBeInvokedExactlyOnceMessage);
        }

        private string GetParamNameShouldBeMessage(string paramName)
        {
            return $"ParamName should be '{paramName}'.";
        }
    }
}

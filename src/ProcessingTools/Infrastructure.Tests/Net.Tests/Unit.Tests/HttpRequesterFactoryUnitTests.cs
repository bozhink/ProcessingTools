namespace ProcessingTools.Net.Tests.Unit.Tests
{
    using System;
    using NUnit.Framework;
    using ProcessingTools.Contracts;

    [TestFixture]
    public class HttpRequesterFactoryUnitTests
    {
        [Test]
        public void HttpRequesterFactory_Constructor_ShouldReturnValidObject()
        {
            var connectorFactory = new HttpRequesterFactory();
            Assert.IsNotNull(connectorFactory, "ConnectorFactory should be a valid object.");
            Assert.IsInstanceOf<IHttpRequesterFactory>(connectorFactory, "ConnectorFactory should be a instance of {0}.", nameof(IHttpRequesterFactory));
        }

        [Test]
        public void HttpRequesterFactory_ParameterlessCreate_ShouldReturnValidObject()
        {
            var connectorFactory = new HttpRequesterFactory();
            var connector = connectorFactory.Create();
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<IHttpRequester>(connector, "Connector should be a instance of {0}.", nameof(IHttpRequester));
        }

        [TestCase("http://localhost")]
        [TestCase("x:\\")]
        [TestCase("x:\\Some\\Directory")]
        [TestCase("x://Some/Directory")]
        [TestCase("file://x/Some/Directory")]
        [TestCase("urn:some-uri.org")]
        [TestCase("uri:some-uri.org")]
        public void HttpRequesterFactory_CreateWithValidBaseAddress_ShouldReturnValidObject(string baseAddress)
        {
            var connectorFactory = new HttpRequesterFactory();
            var connector = connectorFactory.Create(baseAddress);
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<IHttpRequester>(connector, "Connector should be a instance of {0}.", nameof(IHttpRequester));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        [TestCase(@"

     ")]
        public void HttpRequesterFactory_CreateWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullException(string baseAddress)
        {
            Assert.Catch<ArgumentNullException>(
                () =>
                {
                    var connectorFactory = new HttpRequesterFactory();
                    connectorFactory.Create(baseAddress);
                },
                "Constructor With Null BaseAddress should throw {0}",
                nameof(ArgumentNullException));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        [TestCase(@"

     ")]
        public void HttpRequesterFactory_CreateWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullExceptionWithCorrectParamName(string baseAddress)
        {
            try
            {
                var connectorFactory = new HttpRequesterFactory();
                connectorFactory.Create(baseAddress);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(nameof(baseAddress), e.ParamName, "ParamName should be baseAddress");
            }
        }

        [TestCase("localhost")]
        [TestCase("x:localhost")]
        [TestCase("x::localhost")]
        public void HttpRequesterFactory_CreateWithInvalidBaseAddress_ShouldThrowUriFormatException(string baseAddress)
        {
            Assert.Catch<UriFormatException>(
                () =>
                {
                    var connectorFactory = new HttpRequesterFactory();
                    connectorFactory.Create(baseAddress);
                },
                "Constructor should throw UriFormatException");
        }
    }
}

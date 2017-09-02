namespace ProcessingTools.Net.Tests.Unit.Tests
{
    using System;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Net;
    using ProcessingTools.Net;

    [TestFixture]
    public class NetConnectorUnitTests
    {
        [Test]
        public void NetConnector_ParameterlessConstructor_ShouldReturnValidObject()
        {
            var connector = new NetConnector();
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<INetConnector>(connector, "Connector should be a instance of {0}.", nameof(INetConnector));
        }

        [TestCase("http://localhost")]
        [TestCase("x:\\")]
        [TestCase("x:\\Some\\Directory")]
        [TestCase("x://Some/Directory")]
        [TestCase("file://x/Some/Directory")]
        [TestCase("urn:some-uri.org")]
        [TestCase("uri:some-uri.org")]
        public void NetConnector_ConstructorWithValidBaseAddress_ShouldReturnValidObject(string baseAddress)
        {
            var connector = new NetConnector(baseAddress);
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<INetConnector>(connector, "Connector should be a instance of {0}.", nameof(INetConnector));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        [TestCase(@"

     ")]
        public void NetConnector_ConstructorWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullException(string baseAddress)
        {
            Assert.Catch<ArgumentNullException>(
                () =>
                {
                    new NetConnector(baseAddress);
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
        public void NetConnector_ConstructorWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullExceptionWithCorrectParamName(string baseAddress)
        {
            try
            {
                new NetConnector(baseAddress);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(nameof(baseAddress), e.ParamName, "ParamName should be baseAddress");
            }
        }

        [TestCase("localhost")]
        [TestCase("x:localhost")]
        [TestCase("x::localhost")]
        public void NetConnector_ConstructorWithInvalidBaseAddress_ShouldThrowUriFormatException(string baseAddress)
        {
            Assert.Catch<UriFormatException>(
                () =>
                {
                    new NetConnector(baseAddress);
                },
                "Constructor should throw UriFormatException");
        }
    }
}

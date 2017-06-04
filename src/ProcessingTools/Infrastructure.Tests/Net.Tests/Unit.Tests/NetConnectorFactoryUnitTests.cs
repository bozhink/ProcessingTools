namespace ProcessingTools.Net.Tests.Unit.Tests
{
    using System;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Net;

    [TestFixture]
    public class NetConnectorFactoryUnitTests
    {
        [Test]
        public void NetConnectorFactory_Constructor_ShouldReturnValidObject()
        {
            var connectorFactory = new NetConnectorFactory();
            Assert.IsNotNull(connectorFactory, "ConnectorFactory should be a valid object.");
            Assert.IsInstanceOf<INetConnectorFactory>(connectorFactory, "ConnectorFactory should be a instance of {0}.", nameof(INetConnectorFactory));
        }

        [Test]
        public void NetConnectorFactory_ParameterlessCreate_ShouldReturnValidObject()
        {
            var connectorFactory = new NetConnectorFactory();
            var connector = connectorFactory.Create();
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
        public void NetConnectorFactory_CreateWithValidBaseAddress_ShouldReturnValidObject(string baseAddress)
        {
            var connectorFactory = new NetConnectorFactory();
            var connector = connectorFactory.Create(baseAddress);
            Assert.IsNotNull(connector, "Connector should be a valid object.");
            Assert.IsInstanceOf<INetConnector>(connector, "Connector should be a instance of {0}.", nameof(INetConnector));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        [TestCase(@"

     ")]
        public void NetConnectorFactory_CreateWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullException(string baseAddress)
        {
            Assert.Catch<ArgumentNullException>(
                () =>
                {
                    var connectorFactory = new NetConnectorFactory();
                    var connector = connectorFactory.Create(baseAddress);
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
        public void NetConnectorFactory_CreateWithNullOrWhiteSpaceBaseAddress_ShouldThrowArgumentNullExceptionWithCorrectParamName(string baseAddress)
        {
            try
            {
                var connectorFactory = new NetConnectorFactory();
                var connector = connectorFactory.Create(baseAddress);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("BaseAddress", e.ParamName, "ParamName should be BaseAddress");
            }
        }

        [TestCase("localhost")]
        [TestCase("x:localhost")]
        [TestCase("x::localhost")]
        public void NetConnectorFactory_CreateWithInvalidBaseAddress_ShouldThrowUriFormatException(string baseAddress)
        {
            Assert.Catch<UriFormatException>(
                () =>
                {
                    var connectorFactory = new NetConnectorFactory();
                    var connector = connectorFactory.Create(baseAddress);
                },
                "Constructor should throw UriFormatException");
        }
    }
}

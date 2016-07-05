namespace ProcessingTools.Net.Tests.UnitTests
{
    using System;

    using NUnit.Framework;

    using ProcessingTools.Net;

    [TestFixture]
    public class NetConnectorUnitTests
    {
        [Test]
        public void NetConnector_ParameterlessConstructor_ShouldReturnValidObject()
        {
            var connector = new NetConnector();
            Assert.IsNotNull(connector, "Connector should be a valid object.");
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
                    var connector = new NetConnector(baseAddress);
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
                var connector = new NetConnector(baseAddress);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("BaseAddress", e.ParamName, "ParamName should be BaseAddress");
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
                    var connector = new NetConnector(baseAddress);
                },
                "Constructor should throw UriFormatException");
        }
    }
}

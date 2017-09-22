namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Tests.UnitTests
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Clients;
    using ProcessingTools.Contracts;

    [TestClass]
    public class MaterialCitationsParserUnitTests
    {
        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorInDefaultConstructor_ShouldReturnValidObject()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object);
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullConnectorInDefaultConstructor_ShouldThrowArgumentNullException()
        {
            new MaterialCitationsParser(null);
        }

        [TestMethod]
        public void MaterialCitationsParser_WithNullConnectorInDefaultConstructor_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                new MaterialCitationsParser(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("connectorFactory", e.ParamName, "ParamName should be connector.");
            }
        }

        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorAndValidEncodingInConstructor_ShouldReturnValidObject()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF32);
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullConnectorAndValidEncodingInConstructor_ShouldThrowArgumentNullException()
        {
            new MaterialCitationsParser(null, Encoding.UTF32);
        }

        [TestMethod]
        public void MaterialCitationsParser_WithNullConnectorAndValidEncodingInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                new MaterialCitationsParser(null, Encoding.UTF32);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("connectorFactory", e.ParamName, "ParamName should be connector.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithValidConnectorAndNullEncodingInConstructor_ShouldThrowArgumentNullException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            new MaterialCitationsParser(connectorMock.Object, null);
        }

        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorAndNullEncodingInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var connectorMock = new Mock<INetConnectorFactory>();
                new MaterialCitationsParser(connectorMock.Object, null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("encoding", e.ParamName, "ParamName should be encoding.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithNullContentInInvoke_ShouldThrow()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            parser.Invoke(null).Wait();
        }

        [TestMethod]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithNullContentInInvoke_ShouldThrowWithInnerArgumentNullException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            try
            {
                parser.Invoke(null).Wait();
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count, "Number of inner exceptions should be 1.");

                Assert.AreEqual(
                    typeof(ArgumentNullException).FullName,
                    e.InnerExceptions.First().GetType().FullName,
                    "Inner exception should be of type ArgumentNullException.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithWhitespaceContentInInvoke_ShouldThrow()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            parser.Invoke(@"

").Wait();
        }

        [TestMethod]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithWhitespaceContentInInvoke_ShouldThrowWithInnerArgumentNullException()
        {
            var connectorMock = new Mock<INetConnectorFactory>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF8);

            try
            {
                parser.Invoke("  ").Wait();
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count, "Number of inner exceptions should be 1.");

                Assert.AreEqual(
                    typeof(ArgumentNullException).FullName,
                    e.InnerExceptions.First().GetType().FullName,
                    "Inner exception should be of type ArgumentNullException.");
            }
        }
    }
}

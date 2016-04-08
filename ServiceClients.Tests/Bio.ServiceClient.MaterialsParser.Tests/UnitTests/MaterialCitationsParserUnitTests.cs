namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Tests.UnitTests
{
    using System;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using ProcessingTools.Infrastructure.Net.Contracts;

    [TestClass]
    public class MaterialCitationsParserUnitTests
    {
        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorInDefaultConstructor_ShouldReturnValidObject()
        {
            var connectorMock = new Mock<IConnector>();
            var parser = new MaterialCitationsParser(connectorMock.Object);
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullConnectorInDefaultConstructor_ShouldThrowArgumentNullException()
        {
            var parser = new MaterialCitationsParser(null);
        }

        [TestMethod]
        public void MaterialCitationsParser_WithNullConnectorInDefaultConstructor_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var parser = new MaterialCitationsParser(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("connector", e.ParamName, "ParamName should be connector.");
            }
        }

        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorAndValidEncodingInConstructor_ShouldReturnValidObject()
        {
            var connectorMock = new Mock<IConnector>();
            var parser = new MaterialCitationsParser(connectorMock.Object, Encoding.UTF32);
            Assert.IsNotNull(parser, "Parser should not be null object.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithNullConnectorAndValidEncodingInConstructor_ShouldThrowArgumentNullException()
        {
            var parser = new MaterialCitationsParser(null, Encoding.UTF32);
        }

        [TestMethod]
        public void MaterialCitationsParser_WithNullConnectorAndValidEncodingInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var parser = new MaterialCitationsParser(null, Encoding.UTF32);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("connector", e.ParamName, "ParamName should be connector.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaterialCitationsParser_WithValidConnectorAndNullEncodingInConstructor_ShouldThrowArgumentNullException()
        {
            var connectorMock = new Mock<IConnector>();
            var parser = new MaterialCitationsParser(connectorMock.Object, null);
        }

        [TestMethod]
        public void MaterialCitationsParser_WithValidConnectorAndNullEncodingInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var connectorMock = new Mock<IConnector>();
                var parser = new MaterialCitationsParser(connectorMock.Object, null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("encoding", e.ParamName, "ParamName should be encoding.");
            }
        }

    }
}

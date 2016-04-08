namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Tests.IntegrationTests
{
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ProcessingTools.Infrastructure.Net;

    [TestClass]
    public class MaterialCitationsParserIntegrationTests
    {
        [TestMethod]
        [Timeout(5000)]
        public void MaterialCitationsParser_WithValidZeroTestContent_ShouldReturnValidResponse()
        {
            var connector = new Connector();
            var parser = new MaterialCitationsParser(connector, Encoding.UTF8);

            const string ZeroTestContent = @"<paragraph pn=""1"">Test with <detail>detail</detail></paragraph>";

            string result = parser.Invoke(ZeroTestContent).Result;

            Assert.AreEqual(ZeroTestContent, result, "ZeroTestContent should be unchanged.");
        }
    }
}

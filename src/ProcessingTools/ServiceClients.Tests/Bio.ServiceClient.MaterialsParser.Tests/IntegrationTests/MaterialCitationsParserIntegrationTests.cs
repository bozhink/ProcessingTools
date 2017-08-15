namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Tests.IntegrationTests
{
    using System.Configuration;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Clients;
    using ProcessingTools.Net;

    [TestClass]
    public class MaterialCitationsParserIntegrationTests
    {
        private readonly Regex matchWhitespace = new Regex(@"\s+");

        [TestMethod]
        [Timeout(5000)]
        [Ignore]
        public void MaterialCitationsParser_WithValidZeroTestContent_ShouldReturnValidResponse()
        {
            var connector = new NetConnectorFactory();
            var parser = new MaterialCitationsParser(connector, Encoding.UTF8);

            const string ZeroTestContent = @"<paragraph pn=""1"">Test with <detail>detail</detail></paragraph>";

            var requestXml = new XmlDocument
            {
                PreserveWhitespace = false
            };

            requestXml.LoadXml(ZeroTestContent);

            string result = parser.Invoke(requestXml.OuterXml).Result;

            var responseXml = new XmlDocument
            {
                PreserveWhitespace = false
            };

            responseXml.LoadXml(result);

            Assert.AreEqual(
                requestXml.DocumentElement.LocalName,
                responseXml.DocumentElement.LocalName,
                "Local names of root elements should match.");

            Assert.AreEqual(
                this.matchWhitespace.Replace(requestXml.DocumentElement.InnerText, " ").Trim(),
                this.matchWhitespace.Replace(responseXml.DocumentElement.InnerText, " ").Trim(),
                "ZeroTestContent should be unchanged.");
        }

        [TestMethod]
        [Timeout(5000)]
        [Ignore]
        public void MaterialCitationsParser_WithValidRealTestContentWithCommentInIt_ShouldReturnValidResponse()
        {
            var connector = new NetConnectorFactory();
            var parser = new MaterialCitationsParser(connector, Encoding.UTF8);

            var requestXml = new XmlDocument
            {
                PreserveWhitespace = false
            };

            requestXml.Load(ConfigurationManager.AppSettings["RequestXmlWithTwoMaterialCitations"]);

            string result = parser.Invoke(requestXml.OuterXml).Result;

            var responseXml = new XmlDocument
            {
                PreserveWhitespace = false
            };

            responseXml.LoadXml(result);

            Assert.AreEqual(
                requestXml.DocumentElement.LocalName,
                responseXml.DocumentElement.LocalName,
                "Local names of root elements should match.");

            Assert.AreEqual(
                this.matchWhitespace.Replace(requestXml.DocumentElement.InnerText, " ").Trim(),
                this.matchWhitespace.Replace(responseXml.DocumentElement.InnerText, " ").Trim(),
                "Content should be unchanged.");
        }
    }
}

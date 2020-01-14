// <copyright file="MaterialCitationsParserIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Integration.Tests.Bio.MaterialsParser
{
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Clients.Bio.MaterialsParser;
    using ProcessingTools.Services.Net;

    /// <summary>
    /// <see cref="MaterialCitationsParser"/> integration tests.
    /// </summary>
    [TestClass]
    public class MaterialCitationsParserIntegrationTests
    {
        private readonly Regex matchWhitespace = new Regex(@"\s+");

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with valid zero test content should return valid response.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [Ignore(message: "Net dependent integration test")] // Net dependent integration test
        public void MaterialCitationsParser_WithValidZeroTestContent_ShouldReturnValidResponse()
        {
            var httpRequester = new HttpRequester();
            var parser = new MaterialCitationsParser(httpRequester, Encoding.UTF8);

            const string ZeroTestContent = @"<paragraph pn=""1"">Test with <detail>detail</detail></paragraph>";

            var requestXml = new XmlDocument
            {
                PreserveWhitespace = false,
            };

            requestXml.LoadXml(ZeroTestContent);

            string result = parser.ParseAsync(requestXml.OuterXml).Result;

            var responseXml = new XmlDocument
            {
                PreserveWhitespace = false,
            };

            responseXml.LoadXml(result);

            Assert.AreEqual(
                requestXml.DocumentElement.LocalName,
                responseXml.DocumentElement.LocalName);

            Assert.AreEqual(
                this.matchWhitespace.Replace(requestXml.DocumentElement.InnerText, " ").Trim(),
                this.matchWhitespace.Replace(responseXml.DocumentElement.InnerText, " ").Trim());
        }

        /// <summary>
        /// <see cref="MaterialCitationsParser"/> with valid real test content with comment in it should return valid response.
        /// </summary>
        [TestMethod]
        [Timeout(5000)]
        [Ignore(message: "Net dependent integration test")] // Net dependent integration test
        public void MaterialCitationsParser_WithValidRealTestContentWithCommentInIt_ShouldReturnValidResponse()
        {
            var httpRequester = new HttpRequester();
            var parser = new MaterialCitationsParser(httpRequester, Encoding.UTF8);
            string fileName = @"data-files/materials-parser-request-xml-with-two-material-citations.xml";

            var requestXml = new XmlDocument
            {
                PreserveWhitespace = false,
            };

            requestXml.Load(fileName);

            string result = parser.ParseAsync(requestXml.OuterXml).Result;

            var responseXml = new XmlDocument
            {
                PreserveWhitespace = false,
            };

            responseXml.LoadXml(result);

            Assert.AreEqual(
                requestXml.DocumentElement.LocalName,
                responseXml.DocumentElement.LocalName);

            Assert.AreEqual(
                this.matchWhitespace.Replace(requestXml.DocumentElement.InnerText, " ").Trim(),
                this.matchWhitespace.Replace(responseXml.DocumentElement.InnerText, " ").Trim());
        }
    }
}

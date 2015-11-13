namespace ProcessingTools.DocumentProvider.Tests
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaxPubDocumentTests
    {
        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateEmptyXmlDocument()
        {
            var document = new TaxPubDocument();
            Assert.AreEqual(string.Empty, document.XmlDocument.OuterXml, "XmlDocument should be empty.");
        }

        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateNamespaceManager()
        {
            var document = new TaxPubDocument();
            this.CheckNamespaceManager(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            var document = new TaxPubDocument();
            this.CheckTaxPubNamespaces(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateNameTable()
        {
            var document = new TaxPubDocument();
            this.CheckNameTable(document);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyStringInConstructor_ShouldThrow()
        {
            string content = string.Empty;
            var document = new TaxPubDocument(content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullStringInConstructor_ShouldThrow()
        {
            string content = null;
            var document = new TaxPubDocument(content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithWhitespaceStringInConstructor_ShouldThrow()
        {
            string content = @"
                              ";
            var document = new TaxPubDocument(content);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithInvalidXmlStringInConstructor_ShouldThrow()
        {
            string content = "a";
            var document = new TaxPubDocument(content);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNonEmptyXmlDocument()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            Assert.IsFalse(string.IsNullOrWhiteSpace(document.XmlDocument.OuterXml), "XmlDocument schould not be null or whitespace.");
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNonEmptyXmlDocument()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(content);
            var document = new TaxPubDocument(xmlDocument);
            Assert.IsFalse(string.IsNullOrWhiteSpace(document.XmlDocument.OuterXml), "XmlDocument schould not be null or whitespace.");
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNamespaceManager()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            this.CheckNamespaceManager(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNamespaceManager()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            this.CheckNamespaceManager(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            this.CheckTaxPubNamespaces(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumnetInConstructor_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            this.CheckTaxPubNamespaces(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNameTable()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            this.CheckNameTable(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNameTable()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            this.CheckNameTable(document);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_XmlDocumentDocumentElement_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            var xml = document.XmlDocument;
            this.CheckTaxPubNamespaces(xml);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_XmlDocumentDocumentElement_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            var xml = document.XmlDocument;
            this.CheckTaxPubNamespaces(xml);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_XmlString_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            var xml = document.Xml;
            this.CheckTaxPubNamespaces(xml);
        }

        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_XmlString_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            var xml = document.Xml;
            this.CheckTaxPubNamespaces(xml);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullXmlDocumentInConstructor_ShouldThrow()
        {
            XmlDocument xml = null;
            var document = new TaxPubDocument(xml);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyXmlDocumentInConstructor_ShouldThrow()
        {
            XmlDocument xml = new XmlDocument();
            var document = new TaxPubDocument(xml);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetNullXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetEmptyXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = string.Empty;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetWhitespaceXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = @"  
                            ";
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetInvalidXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = @"a";
        }

        private void CheckNamespaceManager(TaxPubDocument document)
        {
            Assert.IsNotNull(document.NamespaceManager, "NamespaceManager should not be null.");
        }

        private void CheckNameTable(TaxPubDocument document)
        {
            Assert.IsNotNull(document.NameTable, "NameTable should not be null.");
        }

        private void CheckTaxPubNamespaces(TaxPubDocument document)
        {
            var namespaceManager = document.NamespaceManager;
            Assert.IsFalse(string.IsNullOrWhiteSpace(namespaceManager.LookupNamespace("tp")), "tp namespace is null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(namespaceManager.LookupNamespace("xlink")), "xlink namespace is null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(namespaceManager.LookupNamespace("xml")), "xml namespace is null or whitespace.");
            ////Assert.IsFalse(string.IsNullOrWhiteSpace(namespaceManager.LookupNamespace("xsi")), "xsi namespace is null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(namespaceManager.LookupNamespace("mml")), "mml namespace is null or whitespace.");
        }

        private void CheckTaxPubNamespaces(XmlDocument xml)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml.DocumentElement.GetNamespaceOfPrefix("tp")), "tp namespace is null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml.DocumentElement.GetNamespaceOfPrefix("xlink")), "xlink namespace is null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml.DocumentElement.GetNamespaceOfPrefix("xml")), "xml namespace is null or whitespace.");
            ////Assert.IsFalse(string.IsNullOrWhiteSpace(xml.DocumentElement.GetNamespaceOfPrefix("xsi")), "xsi namespace is null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml.DocumentElement.GetNamespaceOfPrefix("mml")), "mml namespace is null or whitespace.");
        }

        private void CheckTaxPubNamespaces(string xml)
        {
            Assert.IsTrue(Regex.IsMatch(xml, @"\bxmlns:tp=""[^<>""]+"""), "Xml string does not contain tp namespace");
            Assert.IsTrue(Regex.IsMatch(xml, @"\bxmlns:xlink=""[^<>""]+"""), "Xml string does not contain xlink namespace");
            Assert.IsTrue(Regex.IsMatch(xml, @"\bxmlns:xml=""[^<>""]+"""), "Xml string does not contain xml namespace");
            ////Assert.IsTrue(Regex.IsMatch(xml, @"\bxmlns:xsi=""[^<>""]+"""), "Xml string does not contain xsi namespace");
            Assert.IsTrue(Regex.IsMatch(xml, @"\bxmlns:mml=""[^<>""]+"""), "Xml string does not contain mml namespace");
        }
    }
}
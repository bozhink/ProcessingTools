// <copyright file="TaxPubDocumentTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Tests.Unit.Tests
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common;
    using ProcessingTools.Contracts;

    /// <summary>
    /// <see cref="TaxPubDocument"/> Tests.
    /// </summary>
    [TestClass]
    public class TaxPubDocumentTests
    {
        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty Constructor Should Create Empty XmlDocument.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateEmptyXmlDocument()
        {
            var document = new TaxPubDocument();
            Assert.AreEqual(string.Empty, document.XmlDocument.OuterXml, "XmlDocument should be empty.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty Constructor Should Create NamespaceManager.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateNamespaceManager()
        {
            var document = new TaxPubDocument();
            this.CheckNamespaceManager(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty Constructor Should Create NamespaceManager With Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            var document = new TaxPubDocument();
            this.CheckTaxPubNamespaces(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty Constructor Should Create NameTable.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateNameTable()
        {
            var document = new TaxPubDocument();
            this.CheckNameTable(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty Constructor Should Create Encoding.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithEmptyConstructor_ShouldCreateEncoding()
        {
            var document = new TaxPubDocument();
            Assert.IsNotNull(document.Encoding, "Encoding should not be null.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Encoding Should Create Empty XmlDocument.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_ConstructorWithEncoding_ShouldCreateEmptyXmlDocument()
        {
            var encoding = new UTF32Encoding();
            var document = new TaxPubDocument(encoding);
            Assert.AreEqual(string.Empty, document.XmlDocument.OuterXml, "XmlDocument should be empty.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Encoding Should Create NamespaceManager.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_ConstructorWithEncoding_ShouldCreateNamespaceManager()
        {
            var encoding = new UTF32Encoding();
            var document = new TaxPubDocument(encoding);
            this.CheckNamespaceManager(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Encoding Should Create NamespaceManager With Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_ConstructorWithEncoding_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            var encoding = new UTF32Encoding();
            var document = new TaxPubDocument(encoding);
            this.CheckTaxPubNamespaces(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Encoding Should Create NameTable.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_ConstructorWithEncoding_ShouldCreateNameTable()
        {
            var encoding = new UTF32Encoding();
            var document = new TaxPubDocument(encoding);
            this.CheckNameTable(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Encoding Should Create Encoding.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_ConstructorWithEncoding_ShouldCreateEncoding()
        {
            var encoding = new UTF32Encoding();
            var document = new TaxPubDocument(encoding);
            Assert.IsNotNull(document.Encoding, "Encoding should not be null.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Encoding Should Set Encoding.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_ConstructorWithEncoding_ShouldSetEncoding()
        {
            var encoding = new UTF32Encoding();
            var type = typeof(UTF32Encoding);
            var document = new TaxPubDocument(encoding);

            Assert.AreEqual(
                type,
                document.Encoding.GetType(),
                $"Encoding should be {type}");

            Assert.AreEqual(
                encoding.GetType(),
                document.Encoding.GetType(),
                "Encoding is not set properly.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_ConstructorWithNullEncoding_ShouldThrow()
        {
            Encoding encoding = null;
            new TaxPubDocument(encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty String In Constructor Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyStringInConstructor_ShouldThrow()
        {
            string content = string.Empty;
            new TaxPubDocument(content);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty String In Constructor With Valid Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyStringInConstructor_WithValidEncoding_ShouldThrow()
        {
            string content = string.Empty;
            Encoding encoding = new UTF32Encoding();
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty String In Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyStringInConstructor_WithNullEncoding_ShouldThrow()
        {
            string content = string.Empty;
            Encoding encoding = null;
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Null String In Constructor Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullStringInConstructor_ShouldThrow()
        {
            string content = null;
            new TaxPubDocument(content);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Null String In Constructor With Valid Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullStringInConstructor_WithValidEncoding_ShouldThrow()
        {
            string content = null;
            Encoding encoding = new UTF32Encoding();
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Null String In Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullStringInConstructor_WithNullEncoding_ShouldThrow()
        {
            string content = null;
            Encoding encoding = null;
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Whitespace String In Constructor Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithWhitespaceStringInConstructor_ShouldThrow()
        {
            string content = @"
                              ";
            new TaxPubDocument(content);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Whitespace String In Constructor With Valid Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithWhitespaceStringInConstructor_WithValidEncoding_ShouldThrow()
        {
            string content = @"
                              ";
            Encoding encoding = new UTF32Encoding();
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Whitespace String In Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithWhitespaceStringInConstructor_WithNullEncoding_ShouldThrow()
        {
            string content = @"
                              ";
            Encoding encoding = null;
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Invalid Xml String In Constructor Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithInvalidXmlStringInConstructor_ShouldThrow()
        {
            string content = "a";
            new TaxPubDocument(content);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Invalid Xml String In Constructor With Valid Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithInvalidXmlStringInConstructor_WithValidEncoding_ShouldThrow()
        {
            string content = "a";
            Encoding encoding = new UTF32Encoding();
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> WithInvalidXmlStringInConstructor WithNullEncoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithInvalidXmlStringInConstructor_WithNullEncoding_ShouldThrow()
        {
            string content = "a";
            Encoding encoding = null;
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor Should Create Non Empty XmlDocument.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNonEmptyXmlDocument()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            Assert.IsFalse(string.IsNullOrWhiteSpace(document.XmlDocument.OuterXml), "XmlDocument should not be null or whitespace.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor With Valid Encoding Should Create Non Empty XmlDocument.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithValidEncoding_ShouldCreateNonEmptyXmlDocument()
        {
            string content = "<a/>";
            Encoding encoding = new UTF32Encoding();
            var document = new TaxPubDocument(content, encoding);
            Assert.IsFalse(string.IsNullOrWhiteSpace(document.XmlDocument.OuterXml), "XmlDocument should not be null or whitespace.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithNullEncoding_ShouldThrow()
        {
            string content = "<a/>";
            Encoding encoding = null;
            new TaxPubDocument(content, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor Should Create NamespaceManager.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNamespaceManager()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            this.CheckNamespaceManager(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor With Valid Encoding Should Create NamespaceManager.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithValidEncoding_ShouldCreateNamespaceManager()
        {
            string content = "<a/>";
            Encoding encoding = new UTF32Encoding();
            var document = new TaxPubDocument(content, encoding);
            this.CheckNamespaceManager(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor Should Create NamespaceManager With Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            this.CheckTaxPubNamespaces(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor With Valid Encoding Should Create NamespaceManager With Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithValidEncoding_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            string content = "<a/>";
            Encoding encoding = new UTF32Encoding();
            var document = new TaxPubDocument(content, encoding);
            this.CheckTaxPubNamespaces(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor Should Create NameTable.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_ShouldCreateNameTable()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            this.CheckNameTable(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor With Valid Encoding Should Create NameTable.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithValidEncoding_ShouldCreateNameTable()
        {
            string content = "<a/>";
            Encoding encoding = new UTF32Encoding();
            var document = new TaxPubDocument(content, encoding);
            this.CheckNameTable(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor XmlDocument DocumentElement Should Contain Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_XmlDocumentDocumentElement_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            var xml = document.XmlDocument;
            this.CheckTaxPubNamespaces(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/>_WithValidXmlStringInConstructor With Valid Encoding XmlDocument DocumentElement Should Contain Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithValidEncoding_XmlDocumentDocumentElement_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            Encoding encoding = new UTF32Encoding();
            var document = new TaxPubDocument(content, encoding);
            var xml = document.XmlDocument;
            this.CheckTaxPubNamespaces(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor XmlString Should Contain Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_XmlString_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var document = new TaxPubDocument(content);
            var xml = document.Xml;
            this.CheckTaxPubNamespaces(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid Xml String In Constructor With Valid Encoding XmlString Should Contain Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlStringInConstructor_WithValidEncoding_XmlString_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            Encoding encoding = new UTF32Encoding();
            var document = new TaxPubDocument(content, encoding);
            var xml = document.Xml;
            this.CheckTaxPubNamespaces(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor Should Create Non Empty XmlDocument.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNonEmptyXmlDocument()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            Assert.IsFalse(string.IsNullOrWhiteSpace(document.XmlDocument.OuterXml), "XmlDocument should not be null or whitespace.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor With Valid Encoding Should Create Non Empty XmlDocument.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithValidEncoding_ShouldCreateNonEmptyXmlDocument()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = new UTF32Encoding();

            var document = new TaxPubDocument(xmlDocument, encoding);
            Assert.IsFalse(string.IsNullOrWhiteSpace(document.XmlDocument.OuterXml), "XmlDocument should not be null or whitespace.");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor WithNullEncoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithNullEncoding_ShouldThrow()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = null;

            new TaxPubDocument(xmlDocument, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor Should Create NamespaceManager.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNamespaceManager()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            this.CheckNamespaceManager(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor With Valid Encoding Should Create NamespaceManager.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithValidEncoding_ShouldCreateNamespaceManager()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = new UTF32Encoding();

            var document = new TaxPubDocument(xmlDocument, encoding);
            this.CheckNamespaceManager(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor Should Create NamespaceManager With Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            this.CheckTaxPubNamespaces(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor With Valid Encoding Should Create NamespaceManager With Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithValidEncoding_ShouldCreateNamespaceManagerWithValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = new UTF32Encoding();

            var document = new TaxPubDocument(xmlDocument, encoding);
            this.CheckTaxPubNamespaces(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor Should Create NameTable.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_ShouldCreateNameTable()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var document = new TaxPubDocument(xmlDocument);
            this.CheckNameTable(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor With Valid Encoding Should Create NameTable.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithValidEncoding_ShouldCreateNameTable()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = new UTF32Encoding();

            var document = new TaxPubDocument(xmlDocument, encoding);
            this.CheckNameTable(document);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor XmlDocument DocumentElement Should Contain Valid TaxPub Namespaces.
        /// </summary>
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

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor With Valid Encoding XmlDocument DocumentElement Should Contain Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithValidEncoding_XmlDocumentDocumentElement_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = new UTF32Encoding();

            var document = new TaxPubDocument(xmlDocument, encoding);
            var xml = document.XmlDocument;
            this.CheckTaxPubNamespaces(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor XmlString Should Contain Valid TaxPub Namespaces.
        /// </summary>
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

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Valid XmlDocument In Constructor With Valid Encoding XmlString Should Contain Valid TaxPub Namespaces.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_WithValidXmlDocumentInConstructor_WithValidEncoding_XmlString_ShouldContainValidTaxPubNamespaces()
        {
            string content = "<a/>";
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            Encoding encoding = new UTF32Encoding();

            var document = new TaxPubDocument(xmlDocument, encoding);
            var xml = document.Xml;
            this.CheckTaxPubNamespaces(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Null XmlDocument In Constructor Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullXmlDocumentInConstructor_ShouldThrow()
        {
            XmlDocument xml = null;
            new TaxPubDocument(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Null XmlDocument In Constructor With Valid Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullXmlDocumentInConstructor_WithValidEncoding_ShouldThrow()
        {
            XmlDocument xml = null;
            Encoding encoding = new UTF32Encoding();
            new TaxPubDocument(xml, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Null XmlDocument In Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithNullXmlDocumentInConstructor_WithNullEncoding_ShouldThrow()
        {
            XmlDocument xml = null;
            Encoding encoding = null;
            new TaxPubDocument(xml, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty XmlDocument In Constructor Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyXmlDocumentInConstructor_ShouldThrow()
        {
            XmlDocument xml = new XmlDocument();
            new TaxPubDocument(xml);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty XmlDocument In Constructor With Valid Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyXmlDocumentInConstructor_WithValidEncoding_ShouldThrow()
        {
            XmlDocument xml = new XmlDocument();
            Encoding encoding = new UTF32Encoding();
            new TaxPubDocument(xml, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> With Empty XmlDocument In Constructor With Null Encoding Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_WithEmptyXmlDocumentInConstructor_WithNullEncoding_ShouldThrow()
        {
            XmlDocument xml = new XmlDocument();
            Encoding encoding = null;
            new TaxPubDocument(xml, encoding);
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Set Null Xml String Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetNullXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = null;
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Set Empty Xml String Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetEmptyXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = string.Empty;
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Set Whitespace Xml String Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetWhitespaceXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = @"
                            ";
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Set Invalid Xml String Should Throw.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(XmlException), AllowDerivedTypes = true)]
        public void TaxPubDocument_SetInvalidXmlString_ShouldThrow()
        {
            var document = new TaxPubDocument();
            document.Xml = @"a";
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Select Nodes With Valid XPath Should Work.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_SelectNodesWithValidXPath_ShouldWork()
        {
            var document = new TaxPubDocument();
            document.Xml = @"<a><b>1</b><b>2</b><b>3</b></a>";

            var queryableNodes = document.SelectNodes("//b");

            Assert.AreEqual(3, queryableNodes.Count(), "Number of nodes should be 3.");

            var nodes = queryableNodes.ToList();

            Assert.AreEqual("1", nodes[0].InnerText, @"InnerText of the first node should be ""1"".");
            Assert.AreEqual("2", nodes[1].InnerText, @"InnerText of the second node should be ""2"".");
            Assert.AreEqual("3", nodes[2].InnerText, @"InnerText of the third node should be ""3"".");
        }

        /// <summary>
        /// <see cref="TaxPubDocument"/> Select Single Node With Valid XPath Should Work.
        /// </summary>
        [TestMethod]
        public void TaxPubDocument_SelectSingleNodeWithValidXPath_ShouldWork()
        {
            var document = new TaxPubDocument();
            document.Xml = @"<a><b>1</b><b>2</b><b>3</b></a>";

            var node = document.SelectSingleNode("//b");

            Assert.IsNotNull(node, "Node should not be null.");
            Assert.AreEqual("1", node.InnerText, @"InnerText of the node should be ""1"".");
        }

        private void CheckNamespaceManager(IDocument document)
        {
            Assert.IsNotNull(document.NamespaceManager, "NamespaceManager should not be null.");
        }

        private void CheckNameTable(IDocument document)
        {
            Assert.IsNotNull(document.NameTable, "NameTable should not be null.");
        }

        private void CheckTaxPubNamespaces(IDocument document)
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

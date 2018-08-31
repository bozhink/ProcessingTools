// <copyright file="TaxPubDocument.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;

    /// <summary>
    /// TaxPub document.
    /// </summary>
    public class TaxPubDocument : ITaxPubDocument
    {
        private const string Xmlns = "xmlns";
        private Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxPubDocument"/> class.
        /// </summary>
        /// <param name="encoding"><see cref="Encoding"/>.</param>
        public TaxPubDocument(Encoding encoding)
        {
            this.NameTable = new NameTable();
            this.NamespaceManager = new XmlNamespaceManager(this.NameTable);
            this.NamespaceManager.AddNamespace(Namespaces.TaxPubNamespacePrefix, Namespaces.TaxPubNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.XlinkNamespacePrefix, Namespaces.XlinkNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.XmlNamespacePrefix, Namespaces.XmlNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.XsiNamespacePrefix, Namespaces.XsiNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.MathMLNamespacePrefix, Namespaces.MathMLNamespaceUri);
            this.NamespaceManager.PushScope();

            this.XmlDocument = new XmlDocument(this.NameTable)
            {
                PreserveWhitespace = true
            };

            this.Encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxPubDocument"/> class.
        /// </summary>
        public TaxPubDocument()
            : this(new UTF8Encoding())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxPubDocument"/> class.
        /// </summary>
        /// <param name="xml">XML as string.</param>
        /// <param name="encoding"><see cref="Encoding"/>.</param>
        public TaxPubDocument(string xml, Encoding encoding)
            : this(encoding)
        {
            this.Xml = xml;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxPubDocument"/> class.
        /// </summary>
        /// <param name="xml">XML as string.</param>
        public TaxPubDocument(string xml)
            : this(xml, new UTF8Encoding())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxPubDocument"/> class.
        /// </summary>
        /// <param name="xml"><see cref="XmlDocument"/>.</param>
        /// <param name="encoding"><see cref="Encoding"/>.</param>
        public TaxPubDocument(XmlDocument xml, Encoding encoding)
            : this(encoding)
        {
            if (xml == null)
            {
                throw new ArgumentNullException(nameof(xml));
            }

            this.Xml = xml.OuterXml;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxPubDocument"/> class.
        /// </summary>
        /// <param name="xml"><see cref="XmlDocument"/>.</param>
        public TaxPubDocument(XmlDocument xml)
            : this(xml, new UTF8Encoding())
        {
        }

        /// <inheritdoc/>
        public Encoding Encoding
        {
            get
            {
                return this.encoding;
            }

            private set
            {
                this.encoding = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <inheritdoc/>
        public NameTable NameTable { get; private set; }

        /// <inheritdoc/>
        public XmlNamespaceManager NamespaceManager { get; private set; }

        /// <inheritdoc/>
        public string Xml
        {
            get
            {
                return this.XmlDocument.OuterXml;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.XmlDocument.LoadXml(value);

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.TaxPubNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.TaxPubNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.XlinkNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.XlinkNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.XmlNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.XmlNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.XsiNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.XsiNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.MathMLNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.MathMLNamespacePrefix));
            }
        }

        /// <inheritdoc/>
        public XmlDocument XmlDocument { get; private set; }

        /// <inheritdoc/>
        public SchemaType SchemaType { get; set; }

        /// <inheritdoc/>
        public IEnumerable<XmlNode> SelectNodes(string xpath)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var query = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager)
                .Cast<XmlNode>()
                .AsQueryable();

            return query;
        }

        /// <inheritdoc/>
        public XmlNode SelectSingleNode(string xpath)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var node = this.XmlDocument.SelectSingleNode(xpath, this.NamespaceManager);

            return node;
        }
    }
}

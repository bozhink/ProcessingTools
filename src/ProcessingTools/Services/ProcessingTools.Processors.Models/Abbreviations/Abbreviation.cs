// <copyright file="Abbreviation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Abbreviations
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions;
    using ProcessingTools.Contracts.Processors.Models.Abbreviations;

    /// <summary>
    /// Abbreviation.
    /// </summary>
    public class Abbreviation : IAbbreviation
    {
        private string content;
        private string definition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Abbreviation"/> class.
        /// </summary>
        /// <param name="content">Value for content.</param>
        /// <param name="contentType">Value for content-type.</param>
        /// <param name="definition">Value for definition.</param>
        public Abbreviation(string content, string contentType, string definition)
        {
            this.Content = content;
            this.ContentType = contentType;
            this.Definition = definition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Abbreviation"/> class.
        /// </summary>
        /// <param name="abbrev">Abbreviation XML node.</param>
        public Abbreviation(XmlNode abbrev)
        {
            this.SetContent(abbrev);
            this.SetContentType(abbrev);
            this.SetDefinition(abbrev);
        }

        /// <inheritdoc/>
        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.content = value;
            }
        }

        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string Definition
        {
            get
            {
                return this.definition;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.definition = value;
            }
        }

        /// <inheritdoc/>
        public string ReplacePattern
        {
            get
            {
                var document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                var replacePatternNode = document.CreateElement(ElementNames.Abbrev);

                if (!string.IsNullOrEmpty(this.ContentType))
                {
                    var contentTypeAttribute = document.CreateAttribute(AttributeNames.ContentType);
                    contentTypeAttribute.InnerText = this.ContentType;
                    replacePatternNode.Attributes.Append(contentTypeAttribute);
                }

                if (!string.IsNullOrEmpty(this.Definition))
                {
                    var titleAttribute = document.CreateAttribute(
                        Namespaces.XlinkNamespacePrefix,
                        AttributeNames.XLinkTitle,
                        Namespaces.XlinkNamespaceUri);

                    titleAttribute.InnerText = this.Definition;
                    replacePatternNode.Attributes.Append(titleAttribute);
                }

                replacePatternNode.InnerXml = "$1";

                return replacePatternNode.OuterXml;
            }
        }

        /// <inheritdoc/>
        public string SearchPattern => $"\\b({Regex.Escape(this.Content)})\\b";

        private void SetContent(XmlNode abbrev)
        {
            var abbrevContent = abbrev.CloneNode(true);
            abbrevContent.SelectNodes(ElementNames.Def).RemoveXmlNodes();
            abbrevContent.SelectNodes("*[name()!='sup'][name()!='sub']").ReplaceXmlNodeByItsInnerXml();

            this.Content = abbrevContent.InnerXml
                .Replace(@"\s+", " ", true)
                .Replace(@"\A[^\w'""’‘\*\?]+|[^\w'""’‘\*\?]+\Z", string.Empty, true);
        }

        private void SetContentType(XmlNode abbrev)
        {
            this.ContentType = abbrev.Attributes[AttributeNames.ContentType]?.InnerText;
        }

        private void SetDefinition(XmlNode abbrev)
        {
            this.Definition = abbrev[ElementNames.Def]?.InnerText
                .Replace(@"\s+", " ", true)
                .Replace(@"\A[=,;:\s–—−-]+|[=,;:\s–—−-]+\Z|\s+(?=\s)", string.Empty, true)
                .Replace(@"\((.+)\)", "$1", true)
                .Replace(@"\[(.+)\]", "$1", true);
        }
    }
}

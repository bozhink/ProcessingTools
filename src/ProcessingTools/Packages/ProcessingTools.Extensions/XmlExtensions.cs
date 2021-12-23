// <copyright file="XmlExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using ProcessingTools.Extensions.Text;

    /// <summary>
    /// XML Extensions.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Validate XML with XSD.
        /// </summary>
        /// <param name="xsdFileName">XSL  file name.</param>
        /// <param name="namespaceName">Namespace name.</param>
        /// <param name="xml">XML as string.</param>
        /// <param name="message">Validation message.</param>
        /// <returns>Validation status.</returns>
        public static bool ValidateWithXsd(string xsdFileName, string namespaceName, string xml, out string message)
        {
            message = string.Empty;
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore,
            };

            try
            {
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml), settings))
                {
                    var document = XDocument.Load(xmlReader);
                    var schemas = new XmlSchemaSet();
                    schemas.Add(namespaceName, xsdFileName);

                    using (var stream = File.OpenRead(xsdFileName))
                    {
                        using (var reader = XmlReader.Create(stream, settings))
                        {
                            schemas.Add(@"http://www.w3.org/2000/09/xmldsig#", reader);
                        }

                        stream.Close();
                    }

                    document.Validate(schemas, null);

                    return true;
                }
            }
            catch (XmlSchemaValidationException ex)
            {
                message = ex.Message;
            }

            return false;
        }

        /// <summary>
        /// Checks the type of a given XmlNode object.
        /// Returns true if the XmlNode is a named XmlNode (*) or a text node.
        /// Returns false if the node is a comment, proceeding instruction, DOCTYPE or CDATA element.
        /// </summary>
        /// <param name="node">XmlNode object to check.</param>
        /// <returns>Returns true if the XmlNode is a named XmlNode (*) or a text node. Returns false if the node is a comment, proceeding instruction, DOCTYPE or CDATA element.</returns>
        public static bool CheckIfIsPossibleToPerformReplaceInXmlNode(this XmlNode node)
        {
            if (node is null)
            {
                return false;
            }

            switch (node.NodeType)
            {
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.Document:
                case XmlNodeType.Text:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets Owner Document of an XML node.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> instance, which owner document is wanted.</param>
        /// <returns>Owner Document of the specified node.</returns>
        public static XmlDocument OwnerDocument(this XmlNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = (node is XmlDocument ? node : node.OwnerDocument) as XmlDocument;

            return document;
        }

        /// <summary>
        /// Removes <see cref="XmlNode"/> objects from the DOM object.
        /// </summary>
        /// <param name="nodeList">List of <see cref="XmlNode"/> objects to be removed.</param>
        public static void RemoveXmlNodes(this XmlNodeList nodeList)
        {
            if (nodeList is null)
            {
                throw new ArgumentNullException(nameof(nodeList));
            }

            foreach (XmlNode node in nodeList)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        /// <summary>
        /// Removes <see cref="XmlNode"/> objects from the DOM object.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object in which will be selected by XPath <see cref="XmlNode"/> objects to be removed.</param>
        /// <param name="xpath">XPath string to select XmlNode objects winch will be removed.</param>
        /// <returns>The input <see cref="XmlNode"/> objects with removed XmlNode objects. This return is needed to enable chaining.</returns>
        public static XmlNode RemoveXmlNodes(this XmlNode node, string xpath)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrEmpty(xpath))
            {
                return node;
            }

            node.SelectNodes(xpath).RemoveXmlNodes();
            return node;
        }

        /// <summary>
        /// Replaces the whole <see cref="XmlNode"/> object by a <see cref="XmlDocumentFragment"/>, generated by Regex.Replace.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object to be replaced.</param>
        /// <param name="regexPattern">Regex pattern string to be executed.</param>
        /// <param name="regexReplacement">Regex replacement string to build the <see cref="XmlDocumentFragment"/> object.</param>
        public static void ReplaceWholeXmlNodeByRegexPattern(this XmlNode node, string regexPattern, string regexReplacement)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrEmpty(regexPattern))
            {
                return;
            }

            XmlDocumentFragment nodeFragment = node.OwnerDocument.CreateDocumentFragment();
            nodeFragment.InnerXml = Regex.Replace(node.OuterXml, regexPattern, regexReplacement);
            node.ParentNode.ReplaceChild(nodeFragment, node);
        }

        /// <summary>
        /// Strip outer XML tags of an <see cref="XmlNode"/> object.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object to be stripped.</param>
        public static void ReplaceXmlNodeByItsInnerXml(this XmlNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            XmlDocumentFragment fragment = node.OwnerDocument.CreateDocumentFragment();
            fragment.InnerXml = node.InnerXml;
            node.ParentNode.ReplaceChild(fragment, node);
        }

        /// <summary>
        /// Strip outer XML tags of an <see cref="XmlNode"/> object.
        /// </summary>
        /// <param name="nodeList">List of <see cref="XmlNode"/> objects to be stripped.</param>
        public static void ReplaceXmlNodeByItsInnerXml(this XmlNodeList nodeList)
        {
            if (nodeList is null)
            {
                throw new ArgumentNullException(nameof(nodeList));
            }

            foreach (XmlNode node in nodeList)
            {
                node.ReplaceXmlNodeByItsInnerXml();
            }
        }

        /// <summary>
        /// Wraps matching by regex content in <see cref="XmlNode"/> 'node' in <see cref="XmlElement"/> with name 'repmacementElementName'
        /// using replacement string 'replacementPattern'.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object which content will be changed.</param>
        /// <param name="regex">Regex object to match content to be wrapped in <see cref="XmlElement"/>.</param>
        /// <param name="patterns">Replacement patterns in the following order: 1 - Replacement pattern to be applied in regex before the <see cref="XmlElement"/>; 2 - Replacement pattern to be applied in regex in the <see cref="XmlElement"/>; 3 - Replacement pattern to be applied in regex after the <see cref="XmlElement"/>.</param>
        /// <param name="replacementElementName">The name of the <see cref="XmlElement"/> which will be inserted in the node.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Non-critical catch of general exception")]
        public static void ReplaceXmlNodeContentByRegex(this XmlNode node, Regex regex, Tuple<string, string, string> patterns, string replacementElementName)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            if (patterns is null)
            {
                throw new ArgumentNullException(nameof(patterns));
            }

            if (string.IsNullOrWhiteSpace(replacementElementName))
            {
                throw new ArgumentNullException(nameof(replacementElementName));
            }

            var content = node.InnerXml;
            if (regex.IsMatch(content))
            {
                var replacementElement = node.OwnerDocument.CreateElement(replacementElementName);
                replacementElement.InnerText = patterns.Item2 ?? string.Empty;

                try
                {
                    node.InnerXml = regex.Replace(content, patterns.Item1 ?? string.Empty + replacementElement.OuterXml + patterns.Item3 ?? string.Empty);
                }
                catch
                {
                    node.InnerXml = content;
                }
            }
        }

        /// <summary>
        /// Wraps matching by regex content in <see cref="XmlNode"/> 'node' in <see cref="XmlElement"/> with name 'repmacementElementName'
        /// using replacement string 'replacementPattern'.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object which content will be changed.</param>
        /// <param name="regex">Regex object to match content to be wrapped in <see cref="XmlElement"/>.</param>
        /// <param name="patterns">Replacement patterns in the following order: 1 - Replacement pattern to be applied in regex before the <see cref="XmlElement"/>; 2 - Replacement pattern to be applied in regex in the <see cref="XmlElement"/>; 3 - Replacement pattern to be applied in regex after the <see cref="XmlElement"/>.</param>
        /// <param name="replacementElementName">The name of the <see cref="XmlElement"/> which will be inserted in the node.</param>
        /// <param name="repmacementElementNamePrefix">Prefix for the replacement <see cref="XmlElement"/>.</param>
        /// <param name="namespaceUri">Namespace URI for the replacement <see cref="XmlElement"/>.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Non-critical catch of general exception")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "XML namespace URI")]
        public static void ReplaceXmlNodeContentByRegex(this XmlNode node, Regex regex, Tuple<string, string, string> patterns, string replacementElementName, string repmacementElementNamePrefix, string namespaceUri)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            if (patterns is null)
            {
                throw new ArgumentNullException(nameof(patterns));
            }

            if (string.IsNullOrWhiteSpace(replacementElementName))
            {
                throw new ArgumentNullException(nameof(replacementElementName));
            }

            var content = node.InnerXml;
            if (regex.IsMatch(content))
            {
                var replacementElement = node.OwnerDocument.CreateElement(repmacementElementNamePrefix, replacementElementName, namespaceUri);
                replacementElement.InnerText = patterns.Item2 ?? string.Empty;

                try
                {
                    node.InnerXml = regex.Replace(content, patterns.Item1 ?? string.Empty + replacementElement.OuterXml + patterns.Item3 ?? string.Empty);
                }
                catch
                {
                    node.InnerXml = content;
                }
            }
        }

        /// <summary>
        /// Replaces safely the InnerXml of a given <see cref="XmlNode"/>. If the replace string is not a valid XML fragment, replacement will not be done.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> which content would be replaced.</param>
        /// <param name="replace">Replacement string.</param>
        /// <returns>Status value: is the replacement performed or not.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Non-critical catch of general exception")]
        public static bool SafeReplaceInnerXml(this XmlNode node, string replace)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            string nodeInnerXml = node.InnerXml;
            try
            {
                node.InnerXml = replace;
                return true;
            }
            catch
            {
                node.InnerXml = nodeInnerXml;
            }

            return false;
        }

        /// <summary>
        /// Safely sets the InnerText value of a <see cref="XmlNode"/>'s attributes. Creates it if it does not exist.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object to be set attribute's InnerText value.</param>
        /// <param name="attributeName">The name of the attribute which InnerText will be set.</param>
        /// <param name="attributeInnerText">The value of the InnerText of the attribute.</param>
        /// <returns>Updated node.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Non-critical catch of general exception")]
        public static XmlNode SafeSetAttributeValue(this XmlNode node, string attributeName, string attributeInnerText)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrEmpty(attributeName))
            {
                return node;
            }

            try
            {
                if (node.Attributes[attributeName] is null)
                {
                    XmlAttribute atribute = node.OwnerDocument.CreateAttribute(attributeName);
                    node.Attributes.Append(atribute);
                }

                if (node.Attributes[attributeName].InnerText.Length < 1)
                {
                    node.Attributes[attributeName].InnerText = attributeInnerText;
                }
            }
            catch
            {
                // Skip
            }

            return node;
        }

        /// <summary>
        /// Sets or updates attribute with given value of an <see cref="XmlNode"/> object.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object to be changed.</param>
        /// <param name="attributeName">Name of the attribute to be created of updated.</param>
        /// <param name="attributeValue">Value of the attribute.</param>
        /// <returns>The same <see cref="XmlNode"/> object. Used for chaining.</returns>
        public static XmlNode SetOrUpdateAttribute(this XmlNode node, string attributeName, string attributeValue)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrEmpty(attributeName))
            {
                return node;
            }

            var attribute = node.Attributes[attributeName];
            if (attribute is null)
            {
                var a = node.OwnerDocument.CreateAttribute(attributeName);
                node.Attributes.Append(a);
                attribute = a;
            }

            attribute.InnerText = attributeValue;

            return node;
        }

        /// <summary>
        /// Wraps XML string in <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="document">XML string to be wrapped.</param>
        /// <returns>Wrapper <see cref="XmlDocument"/> object.</returns>
        public static XmlDocument ToXmlDocument(this string document)
        {
            XmlDocument result = new XmlDocument
            {
                PreserveWhitespace = true,
            };

            result.LoadXml(document);
            return result;
        }

        /// <summary>
        /// Creates <see cref="XmlReader"/> object from a text content.
        /// </summary>
        /// <param name="text">Valid XML node as text.</param>
        /// <returns>XmlReader object.</returns>
        /// <exception cref="EncoderFallbackException">Input document string should be UFT8 encoded.</exception>
        public static XmlReader ToXmlReader(this string text)
        {
            var settings = new XmlReaderSettings
            {
                Async = true,
                CloseInput = true,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false,
            };

            XmlReader xmlReader;
            try
            {
                byte[] bytesContent = Defaults.Encoding.GetBytes(text);
                xmlReader = XmlReader.Create(new MemoryStream(bytesContent), settings);
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException($"Input document string should be {Defaults.Encoding.EncodingName} encoded.", e);
            }

            return xmlReader;
        }
    }
}

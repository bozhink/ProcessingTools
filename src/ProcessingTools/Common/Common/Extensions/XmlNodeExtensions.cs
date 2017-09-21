﻿namespace ProcessingTools.Common.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;

    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Checks the type of a given XmlNode object.
        /// Returns true if the XmlNode is a named XmlNode (*) or a text node.
        /// Returns false if the node is a comment, proceeding instruction, DOCTYPE or CDATA element.
        /// </summary>
        /// <param name="node">XmlNode object to check.</param>
        /// <returns>Returns true if the XmlNode is a named XmlNode (*) or a text node. Returns false if the node is a comment, proceeding instruction, DOCTYPE or CDATA element.</returns>
        public static bool CheckIfIsPossibleToPerformReplaceInXmlNode(this XmlNode node)
        {
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

        public static XmlDocument OwnerDocument(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = (node is XmlDocument ? node : node.OwnerDocument) as XmlDocument;

            return document;
        }

        /// <summary>
        /// Removes XmlNode objects from the DOM object.
        /// </summary>
        /// <param name="nodeList">List of XmlNode objects to be removed.</param>
        public static void RemoveXmlNodes(this XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        /// <summary>
        /// Removes XmlNode objects from the DOM object.
        /// </summary>
        /// <param name="node">XmlNode object in which will be selected by XPath XmlNode objects to be removed.</param>
        /// <param name="xpath">XPath string to select XmlNode objects winch will be removed.</param>
        /// <returns>The input XmlNode objects with removed XmlNode objects. This return is needed to enable chaining.</returns>
        public static XmlNode RemoveXmlNodes(this XmlNode node, string xpath)
        {
            node.SelectNodes(xpath).RemoveXmlNodes();
            return node;
        }

        /// <summary>
        /// Replaces the whole XmlNode object by a XmlDocumentFragment, generated by Regex.Replace.
        /// </summary>
        /// <param name="node">XmlNode object to be replaced.</param>
        /// <param name="regexPattern">Regex pattern string to be executed.</param>
        /// <param name="regexReplacement">Regex replacement string to build the XmlDocumentFragment object.</param>
        public static void ReplaceWholeXmlNodeByRegexPattern(this XmlNode node, string regexPattern, string regexReplacement)
        {
            XmlDocumentFragment nodeFragment = node.OwnerDocument.CreateDocumentFragment();
            nodeFragment.InnerXml = Regex.Replace(node.OuterXml, regexPattern, regexReplacement);
            node.ParentNode.ReplaceChild(nodeFragment, node);
        }

        /// <summary>
        /// Strip outer XML tags of an XmlNode object.
        /// </summary>
        /// <param name="node">XmlNode object to be stripped.</param>
        public static void ReplaceXmlNodeByItsInnerXml(this XmlNode node)
        {
            XmlDocumentFragment fragment = node.OwnerDocument.CreateDocumentFragment();
            fragment.InnerXml = node.InnerXml;
            node.ParentNode.ReplaceChild(fragment, node);
        }

        /// <summary>
        /// Strip outer XML tags of an XmlNode object.
        /// </summary>
        /// <param name="nodeList">XmlNodeList of XmlNode objects to be stripped.</param>
        public static void ReplaceXmlNodeByItsInnerXml(this XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                node.ReplaceXmlNodeByItsInnerXml();
            }
        }

        /// <summary>
        /// Wraps matching by regex content in XmlNode 'node' in XmlElement with name 'repmacementElementName'
        /// using replacement string 'replacementPattern'.
        /// </summary>
        /// <param name="node">XmlNode object which content will be changed.</param>
        /// <param name="re">Regex object to match content to be wrapped in XmlElement.</param>
        /// <param name="patterns">Replacement patterns in the following order: 1 - Replacement pattern to be applied in regex before the XmlElement; 2 - Replacement pattern to be applied in regex in the XmlElement; 3 - Replacement pattern to be applied in regex after the XmlElement.</param>
        /// <param name="replacementElementName">The name of the XmlElement which will be inserted in the node.</param>
        public static void ReplaceXmlNodeContentByRegex(this XmlNode node, Regex re, Tuple<string, string, string> patterns, string replacementElementName)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (re == null)
            {
                throw new ArgumentNullException(nameof(re));
            }

            if (patterns == null)
            {
                throw new ArgumentNullException(nameof(patterns));
            }

            if (string.IsNullOrWhiteSpace(replacementElementName))
            {
                throw new ArgumentNullException(nameof(replacementElementName));
            }

            var content = node.InnerXml;
            if (re.IsMatch(content))
            {
                var replacementElement = node.OwnerDocument.CreateElement(replacementElementName);
                replacementElement.InnerText = patterns.Item2 ?? string.Empty;

                try
                {
                    node.InnerXml = re.Replace(content, patterns.Item1 ?? string.Empty + replacementElement.OuterXml + patterns.Item3 ?? string.Empty);
                }
                catch
                {
                    node.InnerXml = content;
                }
            }
        }

        /// <summary>
        /// Wraps matching by regex content in XmlNode 'node' in XmlElement with name 'repmacementElementName'
        /// using replacement string 'replacementPattern'.
        /// </summary>
        /// <param name="node">XmlNode object which content will be changed.</param>
        /// <param name="re">Regex object to match content to be wrapped in XmlElement.</param>
        /// <param name="patterns">Replacement patterns in the following order: 1 - Replacement pattern to be applied in regex before the XmlElement; 2 - Replacement pattern to be applied in regex in the XmlElement; 3 - Replacement pattern to be applied in regex after the XmlElement.</param>
        /// <param name="replacementElementName">The name of the XmlElement which will be inserted in the node.</param>
        /// <param name="repmacementElementNamePrefix">Prefix for the replacement XmlElement.</param>
        /// <param name="namespaceUri">Namespace uri for the replacement XmlElement.</param>
        public static void ReplaceXmlNodeContentByRegex(this XmlNode node, Regex re, Tuple<string, string, string> patterns, string replacementElementName, string repmacementElementNamePrefix, string namespaceUri)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (re == null)
            {
                throw new ArgumentNullException(nameof(re));
            }

            if (patterns == null)
            {
                throw new ArgumentNullException(nameof(patterns));
            }

            if (string.IsNullOrWhiteSpace(replacementElementName))
            {
                throw new ArgumentNullException(nameof(replacementElementName));
            }

            var content = node.InnerXml;
            if (re.IsMatch(content))
            {
                var replacementElement = node.OwnerDocument.CreateElement(repmacementElementNamePrefix, replacementElementName, namespaceUri);
                replacementElement.InnerText = patterns.Item2 ?? string.Empty;

                try
                {
                    node.InnerXml = re.Replace(content, patterns.Item1 ?? string.Empty + replacementElement.OuterXml + patterns.Item3 ?? string.Empty);
                }
                catch
                {
                    node.InnerXml = content;
                }
            }
        }

        /// <summary>
        /// Replaces safely the InnerXml of a given XmlNode. If the replace string is not a valid Xml fragment, replacement will not be done.
        /// </summary>
        /// <param name="node">XmlNode which content would be replaced.</param>
        /// <param name="replace">Replacement string.</param>
        /// <param name="logger">ILogger object to log exceptions.</param>
        /// <returns>Status value: is the replacement performed or not.</returns>
        public static async Task<bool> SafeReplaceInnerXml(this XmlNode node, string replace, ILogger logger)
        {
            string nodeInnerXml = node.InnerXml;
            try
            {
                node.InnerXml = replace;
                return true;
            }
            catch (Exception e)
            {
                logger?.Log(e, "\nInvalid replacement string:\n{0}\n\n", replace.Substring(0, Math.Min(replace.Length, 300)));
                node.InnerXml = nodeInnerXml;
            }

            return await Task.FromResult(false).ConfigureAwait(false);
        }

        /// <summary>
        /// Safely sets the InnerText value of a XmlNode's attributes. Creates it if it does not exist.
        /// </summary>
        /// <param name="node">XmlNode object to be set attribute's InnerText value.</param>
        /// <param name="attributeName">The name of the attribute which InnerText will be set.</param>
        /// <param name="attributeInnerText">The value of the InnerText of the attribute.</param>
        public static void SafeSetAttributeValue(this XmlNode node, string attributeName, string attributeInnerText)
        {
            try
            {
                if (node.Attributes[attributeName] == null)
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
        }

        /// <summary>
        /// Sets or updates attribute with given value of an XmlNode object.
        /// </summary>
        /// <param name="node">XmlNode object to be changed.</param>
        /// <param name="attributeName">Name of the attribute to be created of updated.</param>
        /// <param name="attributeValue">Value of the attribute.</param>
        /// <returns>The same XmlNode object. Used for chaining.</returns>
        public static XmlNode SetOrUpdateAttribute(this XmlNode node, string attributeName, string attributeValue)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var attribute = node.Attributes[attributeName];
            if (attribute == null)
            {
                var a = node.OwnerDocument.CreateAttribute(attributeName);
                node.Attributes.Append(a);
                attribute = a;
            }

            attribute.InnerText = attributeValue;

            return node;
        }

        public static XmlDocument ToXmlDocument(this string document)
        {
            XmlDocument result = new XmlDocument
            {
                PreserveWhitespace = true
            };

            result.LoadXml(document);
            return result;
        }

        /// <summary>
        /// Creates XmlReader object from a text content.
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
                IgnoreWhitespace = false
            };

            XmlReader xmlReader = null;
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

        public static XmlNamespaceManager GetTaxPubXmlNamespaceManager(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = (node is XmlDocument ? node : node.OwnerDocument) as XmlDocument;
            var nameTable = document.NameTable;
            var namespaceManager = new XmlNamespaceManager(nameTable);
            namespaceManager.AddNamespace(Namespaces.TaxPubNamespacePrefix, Namespaces.TaxPubNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.XlinkNamespacePrefix, Namespaces.XlinkNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.XmlNamespacePrefix, Namespaces.XmlNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.XsiNamespacePrefix, Namespaces.XsiNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.MathMLNamespacePrefix, Namespaces.MathMLNamespaceUri);
            namespaceManager.PushScope();

            return namespaceManager;
        }

        public static XmlNodeList SelectNodesWithTaxPubXmlNamespaceManager(this XmlNode node, string xpath)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var namespaceManager = node.GetTaxPubXmlNamespaceManager();
            var nodeList = node.SelectNodes(xpath, namespaceManager);

            return nodeList;
        }
    }
}

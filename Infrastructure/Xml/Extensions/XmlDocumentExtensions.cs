namespace ProcessingTools.Xml.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    using ProcessingTools.Common;

    public static class XmlDocumentExtensions
    {
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
                byte[] bytesContent = Defaults.DefaultEncoding.GetBytes(text);
                xmlReader = XmlReader.Create(new MemoryStream(bytesContent), settings);
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException($"Input document string should be {Defaults.DefaultEncoding.EncodingName} encoded.", e);
            }

            return xmlReader;
        }

        /// <summary>
        /// Removes XmlNode object from the DOM object.
        /// </summary>
        /// <param name="node">XmlNode object to be removed.</param>
        public static void RemoveXmlNodes(this XmlNode node)
        {
            node.ParentNode.RemoveChild(node);
        }

        /// <summary>
        /// Removes XmlNode objects from the DOM object.
        /// </summary>
        /// <param name="nodeList">List of XmlNode objects to be removed.</param>
        public static void RemoveXmlNodes(this XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                node.RemoveXmlNodes();
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
        /// Removes XmlNode objects from the DOM object.
        /// </summary>
        /// <param name="node">XmlNode object in which will be selected by XPath XmlNode objects to be removed.</param>
        /// <param name="xpath">XPath string to select XmlNode objects winch will be removed.</param>
        /// <param name="namespaceManager">XmlNamespaceManager object required in SelectNodes method.</param>
        /// <returns>The input XmlNode objects with removed XmlNode objects. This return is needed to enable chaining.</returns>
        public static XmlNode RemoveXmlNodes(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
        {
            node.SelectNodes(xpath, namespaceManager).RemoveXmlNodes();
            return node;
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
            }
        }

        /// <summary>
        /// Wraps matching by regex content in XmlNode 'node' in XmlElement with name 'repmacementElementName'
        /// using replacement string 'replacementPattern'.
        /// </summary>
        /// <param name="node">XmlNode object which content will be changed.</param>
        /// <param name="re">Regex object to match content to be wrapped in XmlElement.</param>
        /// <param name="preReplacementPattern">Replacement pattern to be applied in regex before the XmlElement.</param>
        /// <param name="replacementPattern">Replacement pattern to be applied in regex in the XmlElement.</param>
        /// <param name="postReplacementPattern">Replacement pattern to be applied in regex after the XmlElement.</param>
        /// <param name="replacementElementName">The name of the XmlElement which will be inserted in the node.</param>
        public static void ReplaceXmlNodeContentByRegex(this XmlNode node, Regex re, string preReplacementPattern, string replacementPattern, string postReplacementPattern, string replacementElementName)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (re == null)
            {
                throw new ArgumentNullException(nameof(re));
            }

            if (string.IsNullOrWhiteSpace(replacementElementName))
            {
                throw new ArgumentNullException(nameof(replacementElementName));
            }

            var content = node.InnerXml;
            if (re.IsMatch(content))
            {
                var replacementElement = node.OwnerDocument.CreateElement(replacementElementName);
                replacementElement.InnerText = replacementPattern;

                try
                {
                    node.InnerXml = re.Replace(content, preReplacementPattern + replacementElement.OuterXml + postReplacementPattern);
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
        /// <param name="preReplacementPattern">Replacement pattern to be applied in regex before the XmlElement.</param>
        /// <param name="replacementPattern">Replacement pattern to be applied in regex in the XmlElement.</param>
        /// <param name="postReplacementPattern">Replacement pattern to be applied in regex after the XmlElement.</param>
        /// <param name="replacementElementName">The name of the XmlElement which will be inserted in the node.</param>
        /// <param name="repmacementElementNamePrefix">Prefix for the replacement XmlElement.</param>
        /// <param name="namespaceUri">Namespace uri for the replacement XmlElement.</param>
        public static void ReplaceXmlNodeContentByRegex(this XmlNode node, Regex re, string preReplacementPattern, string replacementPattern, string postReplacementPattern, string replacementElementName, string repmacementElementNamePrefix, string namespaceUri)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (re == null)
            {
                throw new ArgumentNullException(nameof(re));
            }

            if (string.IsNullOrWhiteSpace(replacementElementName))
            {
                throw new ArgumentNullException(nameof(replacementElementName));
            }

            var content = node.InnerXml;
            if (re.IsMatch(content))
            {
                var replacementElement = node.OwnerDocument.CreateElement(repmacementElementNamePrefix, replacementElementName, namespaceUri);
                replacementElement.InnerText = replacementPattern;

                try
                {
                    node.InnerXml = re.Replace(content, preReplacementPattern + replacementElement.OuterXml + postReplacementPattern);
                }
                catch
                {
                    node.InnerXml = content;
                }
            }
        }
    }
}

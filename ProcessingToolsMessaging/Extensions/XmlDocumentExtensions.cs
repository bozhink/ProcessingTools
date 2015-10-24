namespace ProcessingTools.Globals.Extensions
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    public static class XmlDocumentExtensions
    {
        /// <summary>
        /// Converts XmlDocument to XDocument.
        /// Original source: <see cref="http://stackoverflow.com/questions/1508572/converting-xdocument-to-xmldocument-and-vice-versa"/>
        /// </summary>
        /// <param name="xmlDocument">XmlDocument instance to be converted.</param>
        /// <returns></returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (XmlNodeReader nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        /// <summary>
        /// Converts XDocument to XmlDocument.
        /// Original source: <see cref="http://stackoverflow.com/questions/1508572/converting-xdocument-to-xmldocument-and-vice-versa"/>
        /// </summary>
        /// <param name="document">XDocument instance to be converted.</param>
        /// <returns></returns>
        public static XmlDocument ToXmlDocument(this XDocument document)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (XmlReader xmlReader = document.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }

        /// <summary>
        /// Creates XmlReader object from a text content.
        /// </summary>
        /// <param name="text">Valid XML node as text.</param>
        /// <returns>XmlReader object.</returns>
        /// <exception cref="System.Text.EncoderFallbackException">Input document string schould be UFT8 encoded.</exception>
        public static XmlReader ToXmlReader(this string text)
        {
            XmlReader xmlReader = null;
            try
            {
                byte[] bytesContent = Encoding.UTF8.GetBytes(text);
                xmlReader = XmlReader.Create(new MemoryStream(bytesContent));
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException("Input document string schould be UFT8 encoded.", e);
            }
            catch
            {
                throw;
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
        /// <param name="xpath">XPath string to select XmlNode objects wich will be removed.</param>
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
        /// <param name="xpath">XPath string to select XmlNode objects wich will be removed.</param>
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
        /// <param name="node">XmlNode object to be set attribte's InnerText value.</param>
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
    }
}

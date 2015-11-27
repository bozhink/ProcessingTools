namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Contracts.Log;

    public static class DocumentExtensions
    {
        /// <summary>
        /// Checks the type of a given XmlNode object.
        /// Returns true if the XmlNode is a named XmlNode (*) or a text node.
        /// Returns false if the node is a comment, procedding instruction, DOCTYPE or CDATA element.
        /// </summary>
        /// <param name="node">XmlNode object to check.</param>
        /// <returns>Returns true if the XmlNode is a named XmlNode (*) or a text node. Returns false if the node is a comment, procedding instruction, DOCTYPE or CDATA element.</returns>
        public static bool CheckIfIsPossibleToPerformReplaceInXmlNode(this XmlNode node)
        {
            bool performReplace = false;
            switch (node.NodeType)
            {
                case XmlNodeType.None:
                    performReplace = false;
                    break;

                case XmlNodeType.Element:
                    performReplace = false;
                    break;

                case XmlNodeType.Attribute:
                    performReplace = false;
                    break;

                case XmlNodeType.Text:
                    performReplace = true;
                    break;

                case XmlNodeType.CDATA:
                    performReplace = false;
                    break;

                case XmlNodeType.EntityReference:
                    performReplace = false;
                    break;

                case XmlNodeType.Entity:
                    performReplace = false;
                    break;

                case XmlNodeType.ProcessingInstruction:
                    performReplace = false;
                    break;

                case XmlNodeType.Comment:
                    performReplace = false;
                    break;

                case XmlNodeType.Document:
                    performReplace = true;
                    break;

                case XmlNodeType.DocumentType:
                    performReplace = false;
                    break;

                case XmlNodeType.DocumentFragment:
                    performReplace = true;
                    break;

                case XmlNodeType.Notation:
                    performReplace = false;
                    break;

                case XmlNodeType.Whitespace:
                    performReplace = false;
                    break;

                case XmlNodeType.SignificantWhitespace:
                    performReplace = false;
                    break;

                case XmlNodeType.EndElement:
                    performReplace = false;
                    break;

                case XmlNodeType.EndEntity:
                    performReplace = false;
                    break;

                case XmlNodeType.XmlDeclaration:
                    performReplace = false;
                    break;

                default:
                    performReplace = false;
                    break;
            }

            return performReplace;
        }

        public static IEnumerable<string> ExtractWordsFromString(this string text)
        {
            Regex matchWords = new Regex(@"[^\W\d]+");
            var result = new HashSet<string>();
            for (Match word = matchWords.Match(text); word.Success; word = word.NextMatch())
            {
                result.Add(word.Value);
            }

            return result;
        }

        /// <summary>
        /// Gets list of first words of a given list of strings.
        /// </summary>
        /// <param name="list">IEnumerable&lt;string&gt; object from which to extract first words.</param>
        /// <returns>IEnumerable&lt;string&gt; object containing every first word in the input list.</returns>
        public static IEnumerable<string> GetFirstWord(this IEnumerable<string> list)
        {
            return new HashSet<string>(list.Select(phrase => phrase.GetFirstWord()));
        }

        /// <summary>
        /// Gets the first word of a given string.
        /// </summary>
        /// <param name="phrase">String frm which to extract the first word.</param>
        /// <returns>String of the first word.</returns>
        public static string GetFirstWord(this string phrase)
        {
            Regex matchWord = new Regex(@"\A(?:[^\W\d]{1,4}\.|[^\W\d]{2,})");
            return matchWord.Match(phrase).Value;
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this XmlNode xml, string xpath)
        {
            try
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath);
                var result = nodeList.GetStringListOfUniqueXmlNodeContent();
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager)
        {
            try
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath, namespaceManager);
                var result = nodeList.GetStringListOfUniqueXmlNodeContent();
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this IEnumerable xmlNodeList)
        {
            try
            {
                var result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerText);
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this XmlNode xml, string xpath)
        {
            try
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath);
                var result = nodeList.GetStringListOfUniqueXmlNodes();
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager)
        {
            try
            {
                XmlNodeList nodeList = xml.SelectNodes(xpath, namespaceManager);
                var result = nodeList.GetStringListOfUniqueXmlNodes();
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this IEnumerable xmlNodeList)
        {
            try
            {
                var result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerXml);
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Raplaces the whole XmlNode object by a XmlDocumentFragment, generated by Regex.Replace.
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
        /// Raplaces the whole XmlNode object by a XmlDocumentFragment, generated by Regex.Replace.
        /// </summary>
        /// <param name="node">XmlNode object to be replaced.</param>
        /// <param name="regex">Regex object to be executed.</param>
        /// <param name="regexReplacement">Regex replacement string to build the XmlDocumentFragment object.</param>
        public static void ReplaceWholeXmlNodeByRegexPattern(this XmlNode node, Regex regex, string regexReplacement)
        {
            XmlDocumentFragment nodeFragment = node.OwnerDocument.CreateDocumentFragment();
            nodeFragment.InnerXml = regex.Replace(node.OuterXml, regexReplacement);
            node.ParentNode.ReplaceChild(nodeFragment, node);
        }

        /// <summary>
        /// Replaces safely the InnerXml of a given XmlNode. If the replace string is not a valid Xml fragment, replacement will not be done.
        /// </summary>
        /// <param name="node">XmlNode which content would be replaced.</param>
        /// <param name="replace">Replacement string.</param>
        public static void SafeReplaceInnerXml(this XmlNode node, string replace, ILogger logger)
        {
            string nodeInnerXml = node.InnerXml;
            bool reset = false;
            try
            {
                reset = true;
                node.InnerXml = replace;
                reset = false;
            }
            catch (Exception e)
            {
                logger?.Log(e, "\nInvalid replacement string:\n{0}\n\n", replace);
            }
            finally
            {
                if (reset)
                {
                    node.InnerXml = nodeInnerXml;
                }
            }
        }
    }
}
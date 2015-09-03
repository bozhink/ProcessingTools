namespace ProcessingTools.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

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
            if (node.InnerXml.Length < 1)
            {
                string outerXml = node.OuterXml;
                if (outerXml.IndexOf("<!--") == 0)
                {
                    // This node is a comment. Do not replace matches here.
                    performReplace = false;
                }
                else if (outerXml.IndexOf("<?") == 0)
                {
                    // This node is a processing instruction. Do not replace matches here.
                    performReplace = false;
                }
                else if (outerXml.IndexOf("<!DOCTYPE") == 0)
                {
                    // This node is a DOCTYPE node. Do not replace matches here.
                    performReplace = false;
                }
                else if (outerXml.IndexOf("<![CDATA[") == 0)
                {
                    // This node is a CDATA node. Do nothing?
                    performReplace = false;
                }
                else
                {
                    // This node is a text node. Tag this text and replace in InnerXml
                    performReplace = true;
                }
            }
            else
            {
                // This is a named node
                performReplace = true;
            }

            return performReplace;
        }

        public static void ClearTagsInWrongPositions(this XmlDocument xml)
        {
            foreach (XmlNode node in xml.SelectNodes("//date//institutional_code | //quantity//quantity"))
            {
                node.ReplaceXmlNodeByItsInnerXml();
            }

            foreach (XmlNode node in xml.SelectNodes("//abbrev[normalize-space(@content-type)!='institution']//institutional_code[name(..)!='p']"))
            {
                node.ReplaceXmlNodeByItsInnerXml();
            }
        }

        /// <summary>
        /// Gets all strings which does not contain any string of a comparision list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be distincted.</param>
        /// <param name="compareList">IEnumerable object of patterns which must not be contained in the wordList.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <returns>IEnumerable object of all non-matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> DistinctWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false)
        {
            IEnumerable<string> result = null;
            try
            {
                HashSet<string> list = new HashSet<string>(compareList);
                result = from word in wordList
                         where word.MatchWithStringList(list, treatAsRegex, caseSensitive).Count() == 0
                         select word;
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> ExtractWordsFromString(this string text, bool distinctWords = true)
        {
            List<string> result = new List<string>();

            Regex matchWords = new Regex(@"[^\W\d]+");
            ////result.AddRange(matchWords.Matches(text).Cast<Match>().Select(m => m.Value).ToList<string>());
            for (Match word = matchWords.Match(text); word.Success; word = word.NextMatch())
            {
                result.Add(word.Value);
            }

            if (distinctWords)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> ExtractWordsFromXml(this XmlDocument xml)
        {
            return ExtractWordsFromString(xml.InnerText);
        }

        /// <summary>
        /// Gets list of first words of a given list of strings.
        /// </summary>
        /// <param name="list">IEnumerable&lt;string&gt; object from which to extract first words.</param>
        /// <returns>IEnumerable&lt;string&gt; object containing every first word in the input list.</returns>
        public static IEnumerable<string> GetFirstWord(this IEnumerable<string> list)
        {
            Regex matchWord = new Regex(@"\A[^\W\d]{2,}");
            return new HashSet<string>(list.Select(word => matchWord.Match(word).Value).Distinct());
        }

        public static IEnumerable<string> GetMatchesInText(this string text, Regex search, bool clearList = false)
        {
            List<string> result = new List<string>();

            for (Match m = search.Match(text); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            if (clearList)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetMatchesInXmlText(this XmlNodeList nodeList, Regex search, bool clearList = true)
        {
            List<string> result = new List<string>();

            foreach (XmlNode node in nodeList)
            {
                result.AddRange(node.GetMatchesInXmlText(search, false));
            }

            if (clearList)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetMatchesInXmlText(this XmlNode node, Regex search, bool clearList = true)
        {
            return node.InnerText.GetMatchesInText(search, clearList);
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            IEnumerable<string> result = null;
            try
            {
                XmlNodeList nodeList = null;
                if (namespaceManager == null)
                {
                    nodeList = xml.SelectNodes(xpath);
                }
                else
                {
                    nodeList = xml.SelectNodes(xpath, namespaceManager);
                }

                result = nodeList.GetStringListOfUniqueXmlNodeContent();
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this IEnumerable xmlNodeList)
        {
            IEnumerable<string> result = null;
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerText).Distinct();
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            IEnumerable<string> result = null;
            try
            {
                XmlNodeList nodeList = null;
                if (namespaceManager == null)
                {
                    nodeList = xml.SelectNodes(xpath);
                }
                else
                {
                    nodeList = xml.SelectNodes(xpath, namespaceManager);
                }

                result = nodeList.GetStringListOfUniqueXmlNodes();
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this IEnumerable xmlNodeList)
        {
            IEnumerable<string> result = null;
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct();
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        /// <summary>
        /// Gets all items of an IEnumerable object which are contained in the given string.
        /// </summary>
        /// <param name="word">The string which should contain some items of interest.</param>
        /// <param name="compareList">IEnumerable object which items provides the output items.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <returns>IEnumerable object of all matching string items of compareList in word.</returns>
        /// <remarks>If match-whole-word-search is needed treatAsRegex should be set to true, and values in compareList should be Regex.Escape-d if needed.</remarks>
        public static IEnumerable<string> MatchWithStringList(
            this string word,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false)
        {
            IEnumerable<string> result = null;
            try
            {
                if (treatAsRegex)
                {
                    if (caseSensitive)
                    {
                        result = from comparePattern in compareList
                                 where Regex.Match(word, @"\b" + comparePattern + @"\b").Success
                                 select comparePattern;
                    }
                    else
                    {
                        result = from comparePattern in compareList
                                 where Regex.Match(word, @"\b(?i)" + comparePattern + @"\b").Success
                                 select comparePattern;
                    }
                }
                else
                {
                    if (caseSensitive)
                    {
                        result = from stringToCompare in compareList
                                 where word.Contains(stringToCompare)
                                 select stringToCompare;
                    }
                    else
                    {
                        string wordLowerCase = word.ToLower();
                        result = from stringToCompare in compareList
                                 where wordLowerCase.Contains(stringToCompare.ToLower())
                                 select stringToCompare;
                    }
                }
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
        }

        /// <summary>
        /// Gets all strings which contains any string of a comparision list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be matches.</param>
        /// <param name="compareList">IEnumerable object of patterns which must be contained in the wordList.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <returns>IEnumerable object of all matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> MatchWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false)
        {
            IEnumerable<string> result = null;
            try
            {
                HashSet<string> list = new HashSet<string>(compareList);
                result = from word in wordList
                         where word.MatchWithStringList(list, treatAsRegex, caseSensitive).Count() > 0
                         select word;
            }
            catch
            {
                throw;
            }

            return new HashSet<string>(result);
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
        /// Replaces safely the InnerXml of a given XmlNode. If the replace string is not a valid Xml fragment, replacement will not be done.
        /// </summary>
        /// <param name="node">XmlNode which content would be replaced.</param>
        /// <param name="replace">Replacement string.</param>
        public static void SafeReplaceInnerXml(this XmlNode node, string replace)
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
                Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");
                Alert.RaiseExceptionForMethod(e, 0);
            }
            finally
            {
                if (reset)
                {
                    node.InnerXml = nodeInnerXml;
                }
            }
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

        public static void RemoveXmlNodes(this XmlNode node)
        {
            node.ParentNode.RemoveChild(node);
        }

        public static void RemoveXmlNodes(this XmlNodeList nodeList)
        {
            foreach (XmlNode node in nodeList)
            {
                node.RemoveXmlNodes();
            }
        }

        public static XmlNode RemoveXmlNodes(this XmlNode node, string xpath)
        {
            node.SelectNodes(xpath).RemoveXmlNodes();
            return node;
        }

        public static XmlNode RemoveXmlNodes(this XmlNode node, string xpath, XmlNamespaceManager namespaceManager)
        {
            node.SelectNodes(xpath, namespaceManager).RemoveXmlNodes();
            return node;
        }
    }
}
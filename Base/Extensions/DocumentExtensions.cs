namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Globals.Extensions;
    using Globals.Loggers;

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
            try
            {
                var list = new HashSet<string>(wordList);
                var result = from word in list
                             where word.MatchWithStringList(compareList, treatAsRegex, caseSensitive).Count() == 0
                             select word;

                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> ExtractWordsFromString(this string text)
        {
            List<string> result = new List<string>();

            Regex matchWords = new Regex(@"[^\W\d]+");
            ////result.AddRange(matchWords.Matches(text).Cast<Match>().Select(m => m.Value).ToList<string>());
            for (Match word = matchWords.Match(text); word.Success; word = word.NextMatch())
            {
                result.Add(word.Value);
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

        public static IEnumerable<string> GetMatchesInText(this string text, Regex search)
        {
            List<string> result = new List<string>();

            for (Match m = search.Match(text); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetMatchesInXmlText(this XmlNodeList nodeList, Regex search)
        {
            List<string> result = new List<string>();

            foreach (XmlNode node in nodeList)
            {
                result.AddRange(node.GetMatchesInXmlText(search, false));
            }

            return new HashSet<string>(result);
        }

        public static IEnumerable<string> GetMatchesInXmlText(this XmlNode node, Regex search, bool clearList = true)
        {
            return node.InnerText.GetMatchesInText(search);
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
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
                var result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerText).Distinct();
                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
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
                var list = new HashSet<string>(compareList);
                if (treatAsRegex)
                {
                    if (caseSensitive)
                    {
                        result = from comparePattern in list
                                 where Regex.IsMatch(word, @"\b" + comparePattern + @"\b")
                                 select comparePattern;
                    }
                    else
                    {
                        result = from comparePattern in list
                                 where Regex.IsMatch(word, @"\b(?i)" + comparePattern + @"\b")
                                 select comparePattern;
                    }
                }
                else
                {
                    if (caseSensitive)
                    {
                        result = from stringToCompare in list
                                 where word.Contains(stringToCompare)
                                 select stringToCompare;
                    }
                    else
                    {
                        string wordLowerCase = word.ToLower();
                        result = from stringToCompare in list
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
            try
            {
                var list = new HashSet<string>(compareList);
                var result = from word in wordList
                             where word.MatchWithStringList(list, treatAsRegex, caseSensitive).Count() > 0
                             select word;

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
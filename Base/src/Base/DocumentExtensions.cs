using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ProcessingTools.Base
{
    public static class DocumentExtensions
    {
        /// <summary>
        /// Converts XDocument to XmlDocument.
        /// Original source: <see cref="http://stackoverflow.com/questions/1508572/converting-xdocument-to-xmldocument-and-vice-versa"/>
        /// </summary>
        /// <param name="xDocument">XDocument instance to be converted.</param>
        /// <returns></returns>
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (XmlReader xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }

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

        public static List<string> GetMatchesInXmlText(this XmlNodeList nodeList, Regex search, bool clearList = true)
        {
            List<string> result = new List<string>();

            foreach (XmlNode node in nodeList)
            {
                result.AddRange(GetMatchesInXmlText(node, search, false));
            }

            if (clearList)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return result;
        }

        public static List<string> GetMatchesInXmlText(this XmlNode node, Regex search, bool clearList = true)
        {
            List<string> result = new List<string>();

            string text = node.InnerText;
            for (Match m = search.Match(text); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            if (clearList)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return result;
        }

        public static HashSet<string> ExtractWordsFromString(this string text, bool distinctWords = true)
        {
            List<string> result = new List<string>();

            Regex matchWords = new Regex(@"\b\w+\b");
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

        public static HashSet<string> ExtractWordsFromXml(this XmlDocument xml)
        {
            return ExtractWordsFromString(xml.InnerText);
        }

        public static List<string> GetStringListOfUniqueXmlNodes(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            List<string> result = new List<string>();
            try
            {
                XmlNodeList nodeList;
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static List<string> GetStringListOfUniqueXmlNodes(this IEnumerable xmlNodeList)
        {
            List<string> result = new List<string>();
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static List<string> GetStringListOfUniqueXmlNodeContent(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            List<string> result = new List<string>();
            try
            {
                XmlNodeList nodeList;
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
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static List<string> GetStringListOfUniqueXmlNodeContent(this IEnumerable xmlNodeList)
        {
            List<string> result = new List<string>();
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerText).Distinct().ToList();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        /// <summary>
        /// Given an input list, returns the sublist of not-included-in-xdoc words
        /// </summary>
        /// <param name="wordList">Input list to be parsed.</param>
        /// <param name="xdoc">XDocument to parse with.</param>
        /// <returns>Xdoc-clean sublist of the wordList.</returns>
        public static IEnumerable<string> ClearListWithXDocument(this IEnumerable<string> wordList, XElement xdoc)
        {
            IEnumerable<string> result = null;

            try
            {
                result = from word in wordList
                         where (from item in xdoc.Elements()
                                where Regex.Match(word, "\\A(?i)" + item.Value + "\\Z").Success
                                select item).Count() == 0
                         select word;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        /// <summary>
        /// Given an input list, returns the sublist of included-in-xdoc words
        /// </summary>
        /// <param name="wordList">Input list to be parsed.</param>
        /// <param name="xdoc">XDocument to parse with.</param>
        /// <returns>Xdoc-selected sublist of the wordList.</returns>
        public static IEnumerable<string> SelectListWithXDocument(this IEnumerable<string> wordList, XElement xdoc)
        {
            IEnumerable<string> result = null;

            try
            {
                result = from word in wordList
                         where (from item in xdoc.Elements()
                                where Regex.Match(word, "\\A(?i)" + item.Value + "\\Z").Success
                                select item).Count() > 0
                         select word;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

    }
}
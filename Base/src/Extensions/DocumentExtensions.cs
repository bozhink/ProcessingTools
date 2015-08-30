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
    using System.Xml.Linq;
    using System.Xml.Xsl;

    public static class DocumentExtensions
    {
        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml object and returns the result as a string.
        /// </summary>
        /// <param name="xmlDocument">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument xmlDocument, string xslFileName)
        {
            return xmlDocument.OuterXml.ApplyXslTransform(xslFileName);
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml object and returns the result as a string.
        /// </summary>
        /// <param name="xmlDocument">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument xmlDocument, XslCompiledTransform xslTransform)
        {
            return xmlDocument.OuterXml.ApplyXslTransform(xslTransform);
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the string object and returns the result as a string.
        /// </summary>
        /// <param name="xml">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this string xml, string xslFileName)
        {
            string result = string.Empty;
            try
            {
                using (XmlReader xmlReader = xml.ToXmlReader())
                {
                    result = xmlReader.ApplyXslTransform(xslFileName);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the string object and returns the result as a string.
        /// </summary>
        /// <param name="xml">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this string xml, XslCompiledTransform xslTransform)
        {
            string result = string.Empty;
            try
            {
                byte[] bytesContent = Encoding.UTF8.GetBytes(xml);
                using (XmlReader xmlReader = XmlReader.Create(new MemoryStream(bytesContent)))
                {
                    result = xmlReader.ApplyXslTransform(xslTransform);
                }
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException("Input document string must be UFT8 encoded.", e);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="xmlReader">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlReader xmlReader, string xslFileName)
        {
            string result = string.Empty;
            if (xslFileName == null || xslFileName.Length < 1)
            {
                throw new ArgumentNullException("XSL file name is invalid.");
            }

            try
            {
                XslCompiledTransform xslTransform = new XslCompiledTransform();
                xslTransform.Load(xslFileName);
                result = xmlReader.ApplyXslTransform(xslTransform);
            }
            catch (IOException e)
            {
                throw new IOException("Cannot read XSL file.", e);
            }
            catch (XsltException e)
            {
                throw new XsltException("Invalid XSL file.", e);
            }
            catch (XmlException e)
            {
                throw new System.Xml.XmlException("Invalid XML file.", e);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="xmlReader">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlReader xmlReader, XslCompiledTransform xslTransform)
        {
            string result = string.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                StreamReader streamReader = null;
                try
                {
                    xslTransform.Transform(xmlReader, null, memoryStream);
                    memoryStream.Position = 0;
                    streamReader = new StreamReader(memoryStream);
                }
                catch (XsltException e)
                {
                    throw new XsltException("Invalid XSL file.", e);
                }
                catch (Exception e)
                {
                    throw new System.Exception("General exception.", e);
                }
                finally
                {
                    if (streamReader != null)
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }

            return result;
        }

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

        /// <summary>
        /// Gets all strings which contains any string of a comparision list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be matches.</param>
        /// <param name="compareList">IEnumerable object of patterns which must be contained in the wordList.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <returns>IEnumerable object of all matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> MatchWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false)
        {
            IEnumerable<string> result = null;
            try
            {
                if (treatAsRegex)
                {
                    result = from word in wordList
                             where (from comparePattern in compareList
                                    where Regex.Match(word, "\\A(?i)" + comparePattern + "\\Z").Success
                                    select comparePattern).Count() > 0
                             select word;
                }
                else
                {
                    result = from word in wordList
                             where (from stringToCompare in compareList
                                    where word.Contains(stringToCompare)
                                    select stringToCompare).Count() > 0
                             select word;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Gets all strings which does not contain any string of a comparision list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be distincted.</param>
        /// <param name="compareList">IEnumerable object of patterns which must not be contained in the wordList.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <returns>IEnumerable object of all non-matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> DistinctWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false)
        {
            IEnumerable<string> result = null;
            try
            {
                if (treatAsRegex)
                {
                    result = from word in wordList
                             where (from comparePattern in compareList
                                    where Regex.Match(word, "\\A(?i)" + comparePattern + "\\Z").Success
                                    select comparePattern).Count() == 0
                             select word;
                }
                else
                {
                    result = from word in wordList
                             where (from stringToCompare in compareList
                                    where word.Contains(stringToCompare)
                                    select stringToCompare).Count() == 0
                             select word;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
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

        public static IEnumerable<string> ExtractWordsFromString(this string text, bool distinctWords = true)
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

            return result;
        }

        public static IEnumerable<string> ExtractWordsFromXml(this XmlDocument xml)
        {
            return ExtractWordsFromString(xml.InnerText);
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

            return result;
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

            return result;
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

        public static IEnumerable<string> GetStringListOfUniqueXmlNodeContent(this IEnumerable xmlNodeList)
        {
            IEnumerable<string> result = null;
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerText).Distinct();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            IEnumerable<string> result = null;
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

        public static IEnumerable<string> GetStringListOfUniqueXmlNodes(this IEnumerable xmlNodeList)
        {
            IEnumerable<string> result = null;
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
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
    }
}
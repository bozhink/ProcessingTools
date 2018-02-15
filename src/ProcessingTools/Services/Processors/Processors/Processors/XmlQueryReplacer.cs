namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Contracts;

    public class XmlQueryReplacer : IXmlQueryReplacer
    {
        public Task<string> ReplaceAsync(string content, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(content))
                {
                    return content;
                }

                var queryDocument = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                if (Regex.IsMatch(query, @"<[^>]*>"))
                {
                    // Query is XML
                    queryDocument.LoadXml(query);
                }
                else
                {
                    // Query is not XML
                    queryDocument.Load(query);
                }

                var namespaceManager = new XmlNamespaceManager(queryDocument.NameTable);
                namespaceManager.AddNamespace("query", "urn:processing-tools-query:query-replacer");
                namespaceManager.PushScope();

                string result = content;

                var replaceNodeList = queryDocument.SelectNodes("//query:replace", namespaceManager);
                foreach (XmlNode replaceNode in replaceNodeList)
                {
                    string pattern = replaceNode.SelectSingleNode("query:pattern", namespaceManager).InnerXml;
                    string replacement = replaceNode.SelectSingleNode("query:replacement", namespaceManager).InnerXml;
                    if (replaceNode.Attributes.Count > 0)
                    {
                        result = Regex.Replace(result, pattern: pattern, replacement: replacement);
                    }
                    else
                    {
                        result = Regex.Replace(result, pattern: Regex.Escape(pattern), replacement: replacement);
                    }
                }

                return result;
            });
        }
    }
}

namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Contracts;

    public class QueryReplacer : IQueryReplacer
    {
        public Task<string> Replace(string content, string queryFilePath) => Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(queryFilePath))
            {
                throw new ArgumentNullException(nameof(queryFilePath));
            }

            if (string.IsNullOrEmpty(content))
            {
                return content;
            }

            var queryDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            queryDocument.Load(queryFilePath);

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

namespace ProcessingTools.Processors.Processors
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Contracts;

    public class QueryReplacer : IQueryReplacer
    {
        public Task<string> ReplaceAsync(string content, string queryFileName)
        {
            if (string.IsNullOrWhiteSpace(queryFileName))
            {
                throw new ArgumentNullException(nameof(queryFileName));
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

                queryDocument.Load(queryFileName);

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

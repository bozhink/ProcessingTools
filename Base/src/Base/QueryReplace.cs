namespace ProcessingTools.BaseLibrary
{
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;

    public static class QueryReplace
    {
        /// <summary>
        /// Do multiple replaces using a valid xml query.
        /// </summary>
        /// <param name="textContent">Input string.</param>
        /// <param name="queryFileName">Valid Xml file containing [multiple ]replace instructions.</param>
        /// <returns>Output string after replaces.</returns>
        public static string Replace(Config config, string textContent, string queryFileName)
        {
            string text = textContent;
            FileProcessor queryFileProcessor = new FileProcessor(config, queryFileName);
            queryFileProcessor.Read();
            XmlDocument queryXml = queryFileProcessor.XmlDocument;

            try
            {
                XmlNodeList replaceNodeList = queryXml.SelectNodes("//replace");
                foreach (XmlNode replaceNode in replaceNodeList)
                {
                    string a = replaceNode["A"].InnerXml;
                    string b = replaceNode["B"].InnerXml;
                    if (replaceNode.Attributes.Count > 0)
                    {
                        text = Regex.Replace(text, a, b);
                    }
                    else
                    {
                        text = Regex.Replace(text, Regex.Escape(a), b);
                    }
                }
            }
            catch
            {
                throw;
            }

            return text;
        }
    }
}

namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Globals;

    public class Envo : TaggerBase, IBaseTagger
    {
        private ILogger logger;

        public Envo(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public Envo(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public Envo(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void Tag()
        {
            this.Tag(new XPathProvider(this.Config));
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            XmlDocument envoTermsTagSet = new XmlDocument();
            {
                XmlDocument envoTermsResponse = Net.UseGreekTagger(this.TextContent);

                envoTermsResponse.SelectNodes("//count").RemoveXmlNodes();

                try
                {
                    envoTermsResponse.Save(this.Config.EnvoResponseOutputXmlFileName);
                }
                catch (Exception e)
                {
                    string message = string.Format(
                            "Cannot write envoTermsResponse XML document to file '{0}'",
                            this.Config.EnvoResponseOutputXmlFileName);

                    this.logger?.LogException(e, message);
                }

                string envoTermsResponseString = Regex.Replace(envoTermsResponse.OuterXml, @"\sxmlns=""[^<>""]*""", string.Empty);

                string tagSetString = envoTermsResponseString.ApplyXslTransform(this.Config.EnvoTermsWebServiceTransformXslPath);

                envoTermsTagSet.LoadXml(tagSetString);
            }

            {
                ////string xpath = string.Format(xpathProvider.SelectContentNodesXPathTemplate, "normalize-space(.)!=''");
                //////string xpath = xpathProvider.SelectContentNodesXPath;
                ////XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);

                ////foreach(XmlNode envo in envoTermsTagSet.SelectNodes("//envo"))
                ////{
                ////    string xml = this.Xml;
                ////    bool isValid = true;
                ////    try
                ////    {
                ////        isValid = false;
                ////        this.Xml = Regex.Replace(this.Xml, @"(?<!<[^>]+)\b" + envo.InnerXml + @"\b(?![^<>]*>)", envo.OuterXml);
                ////        isValid = true;
                ////    }
                ////    catch (Exception e)
                ////    {
                ////        Alert.RaiseExceptionForMethod(e, 0);
                ////    }

                ////    if (!isValid)
                ////    {
                ////        this.Xml = xml;
                ////    }
                ////}

                XmlNodeList nodeList = this.XmlDocument.SelectNodes("/*", this.NamespaceManager);
                envoTermsTagSet.DocumentElement.ChildNodes.TagContentInDocument(nodeList, false, true, this.logger);
            }
        }
    }
}

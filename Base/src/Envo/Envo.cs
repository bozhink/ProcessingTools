namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class Envo : TaggerBase
    {
        public Envo(string xml)
            : base(xml)
        {
        }

        public Envo(Config config, string xml)
            : base(config, xml)
        {
        }

        public Envo(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            XmlDocument envoTermsTagSet = new XmlDocument();
            {
                XmlDocument envoTermsResponse = Net.UseGreekTagger(this.TextContent);

                try
                {
                    envoTermsResponse.Save(this.Config.EnvoResponseOutputXmlFileName);
                }
                catch (Exception e)
                {
                    string message = string.Format(
                            "Cannot write envoTermsResponse XML document to file '{0}'",
                            this.Config.EnvoResponseOutputXmlFileName);

                    Alert.RaiseExceptionForMethod(
                        e,
                        this.GetType().Name,
                        0,
                        message);
                }

                string envoTermsResponseString = Regex.Replace(
                    envoTermsResponse.OuterXml,
                    @"\sxmlns=""[^<>""]*""",
                    string.Empty);

                string tagSetString = envoTermsResponseString.ApplyXslTransform(this.Config.envoTermsWebServiceTransformXslPath);

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

                this.TagTextInXmlDocument(envoTermsTagSet, nodeList, true, true);
            }
        }
    }
}

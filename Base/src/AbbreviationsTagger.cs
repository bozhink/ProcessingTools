using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class AbbreviationsTagger : Base
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = "//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),'{0}')][count(.//node()[contains(string(.),'{0}')])=0]";

        private const string AbbreviationReplaceTagName = "abbreviationReplaceTagName";

        public AbbreviationsTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public AbbreviationsTagger(Base baseObject)
            : base(baseObject)
        {
        }

        public void TagAbbreviationsInText()
        {
            this.ParseXmlStringToXmlDocument();

            // Do not change this sequence
            this.TagAbbreviationsInSpecificNode("//graphic|//media|//disp-formula-group");
            this.TagAbbreviationsInSpecificNode("//chem-struct-wrap|//fig|//supplementary-material|//table-wrap");
            this.TagAbbreviationsInSpecificNode("//fig-group|//table-wrap-group");
            this.TagAbbreviationsInSpecificNode("//boxed-text");
            this.TagAbbreviationsInSpecificNode("/");

            this.xmlDocument.InnerXml = Regex.Replace(this.xmlDocument.InnerXml, "</?" + AbbreviationReplaceTagName + "[^>]*>", string.Empty);
            this.xml = this.xmlDocument.OuterXml;
        }

        private void TagAbbreviationsInSpecificNode(string selectSpecificNodeXPath)
        {
            XmlNodeList specificNodes = xmlDocument.SelectNodes(selectSpecificNodeXPath, NamespaceManager);
            foreach (XmlNode specificNode in specificNodes)
            {
                List<Abbreviation> abbreviationsList = specificNode.SelectNodes(".//abbrev", NamespaceManager)
                    .Cast<XmlNode>().Select(a => this.ConvertAbbrevXmlNodeToAbbreviation(a)).ToList();

                foreach (Abbreviation abbreviation in abbreviationsList)
                {
                    string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                    foreach (XmlNode nodeInspecificNode in specificNode.SelectNodes(xpath, this.NamespaceManager))
                    {
                        bool doReplace = false;
                        if (nodeInspecificNode.InnerXml == string.Empty)
                        {
                            if (nodeInspecificNode.OuterXml.IndexOf("<!--") == 0)
                            {
                                // This node is a comment. Do not replace matches here.
                                doReplace = false;
                            }
                            else if (nodeInspecificNode.OuterXml.IndexOf("<?") == 0)
                            {
                                // This node is a processing instruction. Do not replace matches here.
                                doReplace = false;
                            }
                            else if (nodeInspecificNode.OuterXml.IndexOf("<!DOCTYPE") == 0)
                            {
                                // This node is a DOCTYPE node. Do not replace matches here.
                                doReplace = false;
                            }
                            else if (nodeInspecificNode.OuterXml.IndexOf("<![CDATA[") == 0)
                            {
                                // This node is a CDATA node. Do nothing?
                                doReplace = false;
                            }
                            else
                            {
                                // This node is a text node. Tag this tex¾t and replace in InnerXml
                                doReplace = true;
                            }
                        }
                        else
                        {
                            // This is a named node
                            doReplace = true;
                        }

                        if (doReplace)
                        {
                            XmlElement newNode = xmlDocument.CreateElement("abbreviationReplaceTagName");
                            newNode.InnerXml = Regex.Replace(nodeInspecificNode.OuterXml, abbreviation.SearchPattern, abbreviation.ReplacePattern);
                            nodeInspecificNode.ParentNode.ReplaceChild(newNode, nodeInspecificNode);
                        }
                    }
                }
            }
        }

        private Abbreviation ConvertAbbrevXmlNodeToAbbreviation(XmlNode abbrev)
        {
            Abbreviation abbreviation = new Abbreviation();

            abbreviation.Content = Regex.Replace(
                        Regex.Replace(
                            Regex.Replace(
                                abbrev.InnerXml,
                                @"<def.+</def>",
                                string.Empty),
                            @"<def[*>]</def>|</?b[^>]*>",
                            string.Empty),
                        @"\A\W+|\W+\Z",
                        string.Empty);

            if (abbrev.Attributes["content-type"] != null)
            {
                abbreviation.ContentType = abbrev.Attributes["content-type"].InnerText;
            }

            if (abbrev["def"] != null)
            {
                abbreviation.Definition = Regex.Replace(
                    Regex.Replace(
                        abbrev["def"].InnerXml,
                        "<[^>]*>",
                        string.Empty),
                    @"\A[=,;:\s–—−-]|[=,;:\s–—−-]\Z|\s+(?=\s)",
                    string.Empty);
            }

            return abbreviation;
        }

        private struct Abbreviation
        {
            public string Content;

            public string ContentType;

            public string Definition;

            public string SearchPattern
            {
                get
                {
                    return "\\b(" + this.Content + ")\\b";
                }
            }

            public string ReplacePattern
            {
                get
                {
                    return "<abbrev" +
                        ((this.ContentType == null || this.ContentType == string.Empty) ? string.Empty : @" content-type=""" + this.ContentType + @"""") +
                        ((this.Definition == null || this.Definition == string.Empty) ? string.Empty : @" xlink:title=""" + this.Definition + @"""") +
                        @" xmlns:xlink=""http://www.w3.org/1999/xlink""" +
                        ">$1</abbrev>";
                }
            }
        }
    }
}

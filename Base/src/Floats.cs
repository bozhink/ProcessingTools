using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public enum ReferenceType
    {
        Affiliation, ////"aff"
        Appendix, ////"app"
        AuthorNotes, ////"author-notes"
        BibliographiReference, ////"bibr"
        ChemicalStructure, ////"chem"
        Contributor, ////"contrib"
        CorrespondingAuthor, ////"corresp"
        DisplayFormula, ////"disp-formula"
        Figure, ////"fig"
        Footnote, ////"fn"
        Keyword, ////"kwd"
        List, ////"list"
        Other, ////"other"
        Plate, ////"plate"
        Scheme, ////"scheme"
        Section, ////"sec"
        Statement, ////"statement"
        SupplementaryMaterial, ////"supplementary-material"
        Table, ////"table"
        TableFootnote, ////"table-fn"
        Textbox ////"boxed-text"
    }

    public class Floats : Base
    {
        private const int MaxNumberOfSequentalFloats = 30;
        private const int MaxNumberOfPunctuationSigns = 10;
        private const string SubfloatsPattern = "\\s*(([A-Za-z]\\d?|[ivx]+)[,\\s]*([,;–−-]|and|\\&amp;)\\s*)*([A-Za-z]\\d?|[ivx]+)";

        private string[] floatNumericLabel;

        private Hashtable floatIdByLabel = null;
        private Hashtable floatLabelById = null;
        private IEnumerable floatIdByLabelKeys = null;
        private IEnumerable floatIdByLabelValues = null;

        public Floats(Config config, string xml)
            : base(config, xml)
        {
            this.InitFloats();
        }

        public Floats(Base baseObject)
            : base(baseObject)
        {
            this.InitFloats();
        }

        /// <summary>
        /// Gets the number of floating objects of a given type and populates label-and-id-related hash tables.
        /// This method generates the "dictionary" to correctly post-process xref/@rid references.
        /// </summary>
        /// <param name="refType">"Physical" type of the floating object: &lt;fig /&gt;, &lt;table-wrap /&gt;, &lt;boxed-text /&gt;, etc.</param>
        /// <param name="floatType">"Logical" type of the floating object: This string is supposed to be contained in the &lt;label /&gt; of the object.</param>
        /// <returns>Number of floating objects of type refType with label containing "floatType"</returns>
        public int GetFloats(ReferenceType refType = ReferenceType.Figure, string floatType = "Figure")
        {
            int numberOfFloatsOfType = 0;
            this.ParseXmlStringToXmlDocument();

            string xpath = string.Empty;
            switch (refType)
            {
                case ReferenceType.Figure:
                    xpath = "//fig[contains(string(label),'" + floatType + "')]";
                    break;
                case ReferenceType.Table:
                    xpath = "//table-wrap[contains(string(label),'" + floatType + "')]";
                    break;
                case ReferenceType.Textbox:
                    xpath = "//box[contains(string(title),'" + floatType + "')]|//boxed-text[contains(string(label),'" + floatType + "')]";
                    break;
                case ReferenceType.SupplementaryMaterial:
                    xpath = "//supplementary-material[contains(string(label),'" + floatType + "')]";
                    break;
                default:
                    xpath = string.Empty;
                    break;
            }

            this.floatIdByLabel = new Hashtable();
            this.floatLabelById = new Hashtable();

            try
            {
                XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);
                numberOfFloatsOfType = nodeList.Count;
                this.floatNumericLabel = new string[numberOfFloatsOfType + 1];
                for (int i = 0; i < numberOfFloatsOfType + 1; i++)
                {
                    this.floatNumericLabel[i] = string.Empty;
                }

                int currentFloat = 0;
                foreach (XmlNode node in nodeList)
                {
                    currentFloat++;
                    string idAttribute = string.Empty;
                    if (node.Attributes["id"] != null)
                    {
                        idAttribute = node.Attributes["id"].InnerText;
                    }
                    else
                    {
                        switch (refType)
                        {
                            case ReferenceType.Table:
                                try
                                {
                                    idAttribute = node["table"].Attributes["id"].InnerText;
                                }
                                catch (Exception e)
                                {
                                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0,
                                        "There is no 'table-wrap/@id' or 'table-wrap/table' or 'table-wrap/table/@id'");
                                }

                                break;
                        }
                    }

                    // Get the text of the current float
                    string labelText = string.Empty;
                    if (node["label"] != null)
                    {
                        labelText = node["label"].InnerXml;
                    }
                    else if (node["title"] != null)
                    {
                        labelText = node["title"].InnerXml;
                    }

                    if (Regex.Match(labelText, @"\A\w+\s+([A-Za-z]?\d+\W*)+\Z").Success)
                    {
                        this.floatNumericLabel[currentFloat] = Regex.Replace(labelText, @"\A\w+\s+(([A-Za-z]?\d+\W*?)+)[\.;,:–—−-]*\s*\Z", "$1");
                        this.floatLabelById.Add(idAttribute, this.floatNumericLabel[currentFloat]);
                    }

                    for (Match m = Regex.Match(labelText, @"[A-Z]?\d+([–—−-](?=[A-Z]?\d+))?"); m.Success; m = m.NextMatch())
                    {
                        string curr = Regex.Replace(m.Value, "[–—−-]", string.Empty);
                        string next = m.NextMatch().Success ? Regex.Replace(m.NextMatch().Value, "[–—−-]", string.Empty) : string.Empty;

                        this.floatIdByLabel.Add(curr, idAttribute);

                        Match dash = Regex.Match(m.Value, "[–—−-]");
                        if (dash.Success)
                        {
                            try
                            {
                                int icurr = int.Parse(Regex.Replace(curr, @"\D", string.Empty));
                                int inext = int.Parse(Regex.Replace(next, @"\D", string.Empty));
                                string prefix = Regex.Replace(curr, @"([A-Z]?)\d+", "$1");
                                if (icurr < inext)
                                {
                                    for (int i = icurr + 1; i < inext; i++)
                                    {
                                        this.floatIdByLabel.Add(prefix + i, idAttribute);
                                    }
                                }
                                else
                                {
                                    throw new Exception("Error in multiple-float's label '" + labelText + "': Label numbers must be strictly increasing.");
                                }
                            }
                            catch (Exception e)
                            {
                                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            this.floatIdByLabelKeys = this.floatIdByLabel.Keys;
            this.floatIdByLabelValues = this.floatIdByLabel.Values;

            Console.WriteLine();
            foreach (string s in this.floatIdByLabelKeys.Cast<string>().ToArray().OrderBy(s => s))
            {
                Console.WriteLine("{2}\t#{0}\tis in float\t#{1}", s, this.floatIdByLabel[s], refType.ToString());
            }

            return numberOfFloatsOfType;
        }

        public void TagAllFloats()
        {
            // Force Fig. and Figs
            this.xml = Regex.Replace(this.xml, @"(Fig)\s", "$1. ");
            this.xml = Regex.Replace(this.xml, @"(Figs)\.", "$1");

            {
                /*
                 * Tag Figures
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Figure, "Figure");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("fig", "Fig\\.|Figs|Figures?");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "fig");
                    this.FormatXrefGroup("fig");
                }
            }

            {
                /*
                 * Tag Maps
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Figure, "Map");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("map", "Maps?");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "map");
                    this.FormatXrefGroup("map");
                }
            }

            {
                /*
                 * Tag Plates
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Figure, "Plate");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("plate", "Plates?");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "plate");
                    this.FormatXrefGroup("plate");
                }
            }

            {
                /*
                 * Tag Habitus
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Figure, "Habitus");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("habitus", "Habitus");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "habitus");
                    this.FormatXrefGroup("habitus");
                }
            }

            {
                /*
                 * Tag Tables
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Table, "Table");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("table", "Tab\\.|Tabs|Tables?");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "table");
                    this.FormatXrefGroup("table");
                }
            }

            {
                /*
                 * Tag Boxes of type table
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Table, "Box");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("table", "Box|Boxes");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "table");
                    this.FormatXrefGroup("table");
                }
            }

            {
                /*
                 * Tag Boxes of type boxed-text
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.Textbox, "Box");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("boxed-text", "Box|Boxes");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "boxed-text");
                    this.FormatXrefGroup("boxed-text");
                }
            }

            {
                /*
                 * Tag Supplementary materials
                 */
                this.InitFloats();
                int numberOfFloatsOfType = this.GetFloats(ReferenceType.SupplementaryMaterial, "Supplementary material");
                if (numberOfFloatsOfType > 0)
                {
                    this.TagFloatsOfType("supplementary-material", @"Suppl\.\s*materials?");
                    this.FormatXref();
                    this.ProcessFloatsRid(numberOfFloatsOfType, "supplementary-material");
                    this.FormatXrefGroup("supplementary-material");
                }
            }

            this.RemoveXrefInTitles();

            this.xml = Regex.Replace(this.xml, "\\s+ref-type=\"(map|plate|habitus)\"", " ref-type=\"fig\"");
        }

        public void TagTableFootNotes()
        {
            this.ParseXmlStringToXmlDocument();

            // Get list of table-wrap with correctly formatted foot-notes
            XmlNodeList tableWrapList = this.xmlDocument.SelectNodes("//table-wrap[table-wrap-foot[fn[label][@id]]]", this.NamespaceManager);
            if (tableWrapList.Count < 1)
            {
                Alert.Log("There is no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");
            }
            else
            {
                Console.WriteLine("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);
                foreach (XmlNode tableWrap in tableWrapList)
                {
                    Hashtable tableFootNotes = new Hashtable();

                    // Get foot-note's label and corresponding @id-s
                    foreach (XmlNode fn in tableWrap.SelectNodes("//fn[label][@id]", this.NamespaceManager))
                    {
                        tableFootNotes.Add(fn["label"].InnerText.Trim(), fn.Attributes["id"].Value.Trim());
                    }

                    foreach (string x in tableFootNotes.Keys)
                    {
                        foreach (XmlNode footnoteSup in tableWrap.SelectNodes("//table//sup[normalize-space()='" + x + "']", this.NamespaceManager))
                        {
                            // <xref ref-type="table-fn" rid="TN1"></xref>
                            XmlNode xrefTableFootNote = xmlDocument.CreateElement("xref");

                            XmlAttribute refType = xmlDocument.CreateAttribute("ref-type");
                            refType.InnerXml = "table-fn";
                            xrefTableFootNote.Attributes.Append(refType);

                            XmlAttribute rid = xmlDocument.CreateAttribute("rid");
                            rid.InnerXml = tableFootNotes[x].ToString();
                            xrefTableFootNote.Attributes.Append(rid);

                            xrefTableFootNote.InnerXml = footnoteSup.OuterXml;

                            footnoteSup.InnerXml = xrefTableFootNote.OuterXml;
                        }
                    }
                }
            }

            this.xml = this.xmlDocument.OuterXml;
            this.xml = Regex.Replace(this.xml, @"<sup>(<xref ref-type=""table-fn"" [^>]*><sup>[^<>]*?</sup></xref>)</sup>", "$1");
        }

        private void InitFloats()
        {
            if (this.floatIdByLabel != null)
            {
                this.floatIdByLabel.Clear();
                this.floatIdByLabel = null;
            }

            if (this.floatLabelById != null)
            {
                this.floatLabelById.Clear();
            }

            this.floatIdByLabelKeys = null;
            this.floatIdByLabelValues = null;
        }

        private string FloatsFirstOccurencePattern(string labelPattern)
        {
            return "\\b(" + labelPattern + ")\\s*(([A-Z]?\\d+)(" + SubfloatsPattern + ")?)(?=\\W)";
        }

        private string FloatsFirstOccurenceReplace(string floatType)
        {
            return "$1 <xref ref-type=\"" + floatType + "\" rid=\"$3\">$2</xref>";
        }

        private string FloatsNextOccurencePattern(string floatType)
        {
            return "(<xref ref-type=\"" + floatType + "\" [^>]*>[^<]*</xref>[,\\s]*([,;–−-]|and|\\&amp;)\\s*)(([A-Z]?\\d+)(" + SubfloatsPattern + ")?)(?=\\W)";
        }

        private string FloatsNextOccurenceReplace(string floatType)
        {
            return "$1<xref ref-type=\"" + floatType + "\" rid=\"$4\">$3</xref>";
        }

        /// <summary>
        /// Find and put in xref citations of a floating object of given type.
        /// </summary>
        /// <param name="floatType">Logical type of the floating object. This string will be put as current value of the attribute xref/@ref-type.</param>
        /// <param name="labelPattern">Regex pattern to find citations of floating objects of the given type.</param>
        private void TagFloatsOfType(string floatType, string labelPattern)
        {
            string pattern = this.FloatsFirstOccurencePattern(labelPattern);
            string replace = this.FloatsFirstOccurenceReplace(floatType);
            this.xml = Regex.Replace(this.xml, pattern, replace);

            pattern = this.FloatsNextOccurencePattern(floatType);
            replace = this.FloatsNextOccurenceReplace(floatType);
            for (int i = 0; i < MaxNumberOfSequentalFloats; i++)
            {
                this.xml = Regex.Replace(this.xml, pattern, replace);
            }
        }

        private void ProcessFloatsRid(int floatsNumber, string refType)
        {
            string pattern = string.Empty, replace = string.Empty;

            foreach (string s in this.floatIdByLabelKeys)
            {
                this.xml = Regex.Replace(this.xml, "<xref ref-type=\"" + refType + "\" rid=\"" + s + "\">", "<xref ref-type=\"" + refType + "\" rid=\"" + this.floatIdByLabel[s] + "\">");
            }

            foreach (string s in this.floatIdByLabelValues.Cast<string>().Select(c => c).Distinct().ToList())
            {
                for (int j = 0; j < MaxNumberOfSequentalFloats; j++)
                {
                    this.xml = Regex.Replace(this.xml, "((<xref ref-type=\"" + refType + "\" rid=\"" + s + "\">)[^<>]*)</xref>\\s*[–—−-]\\s*\\2", "$1–");
                }
            }
        }

        private void RemoveXrefInTitles()
        {
            this.ParseXmlStringToXmlDocument();
            string xpath = "//fig//label[xref]|//fig//title[xref]|//table-wrap//label[xref]|//table-wrap//title[xref]";
            try
            {
                foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "<xref [^>]*>|</?xref>", string.Empty);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            this.xml = this.xmlDocument.OuterXml;
        }

        private void FormatXref()
        {
            // Format content between </xref> and <xref
            this.xml = Regex.Replace(this.xml, @"(?<=</xref>)\s*[–—−-]\s*(?=<xref)", "–");
            this.xml = Regex.Replace(this.xml, @"(?<=</xref>)\s*([,;])\s*(?=<xref)", "$1 ");
            this.xml = Regex.Replace(this.xml, @"(?<=</xref>)\s*(and|\\&amp;)\s*(?=<xref)", " $1 ");

            this.xml = Regex.Replace(this.xml, @"(<xref [^>]*>)\s*[–—−-]\s*(?=[A-Za-z0-9][^<>]*</xref>)", "–$1");

            // Remove xref from attributes
            for (int i = 0; i < 2 * MaxNumberOfSequentalFloats; i++)
            {
                this.xml = Regex.Replace(this.xml, "(<[^<>]+=\"[^<>\"]*)<[^<>]*>", "$1");
            }
        }

        private void FormatXrefGroup(string refType)
        {
            StringBuilder sb = new StringBuilder();
            this.ParseXmlStringToXmlDocument();

            try
            {
                foreach (XmlNode node in this.xmlDocument.SelectNodes("//xref-group[xref[@ref-type='" + refType + "']]", this.NamespaceManager))
                {
                    // Format content in xref-group
                    string xref_group = node.InnerXml;

                    // <xref-group>Figures 109–112
                    for (Match dashed = Regex.Match(xref_group, "<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>[–—−-]<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>"); dashed.Success; dashed = dashed.NextMatch())
                    {
                        string xref_replace = dashed.Value;

                        string idPrefix = Regex.Replace(xref_replace, @"<xref .*?rid=\W(.*?)\d+.*?>.*", "$1");
                        string first_id = Regex.Replace(xref_replace, @"\A<xref .*?(\d+).*?>.*", "$1");
                        string last_id = Regex.Replace(xref_replace, @".*[–—−-]<xref .*?(\d+).*?>.*", "$1");

                        int first = int.Parse(first_id);
                        int last = int.Parse(last_id);

                        // Parse the dash
                        // Convert the dash to a sequence of xref
                        {
                            sb.Clear();
                            for (int i = first + 1; i < last; i++)
                            {
                                string rid = idPrefix + i;
                                sb.Append(", <xref ref-type=\"" + refType + "\" rid=\"" + rid + "\">" + this.floatLabelById[rid] + "</xref>");
                            }

                            xref_replace = Regex.Replace(xref_replace, "(</xref>)[–—−-](<xref [^>]*>)", "$1" + sb.ToString() + ", $2");
                        }

                        // <xref-group>Figs <xref ref-type="fig" rid="F1">1</xref>, <xref ref-type="F2">5–11</xref>, <xref ref-type="F3">12–18</xref>
                        // , <xref ref-type="F4">19–22</xref>, <xref ref-type="F5">23–31</xref>, <xref ref-type="F6">32–37</xref>, <xref ref-type="fig" rid="F7">40</xref>

                        // Parse left xref
                        {
                            Match matchLeftXref = Regex.Match(xref_replace, @"\A<xref [^>]*>[^<>]+</xref>, <xref [^>]*>[A-Z]?\d+");
                            string leftXref = matchLeftXref.Value;
                            if (matchLeftXref.Success)
                            {
                                string thisXrefLastItem = Regex.Replace(matchLeftXref.Value, @"\A<xref [^>]*>[^<>]*?([A-Z]?\d+)</xref>, <xref [^>]*>[A-Z]?\d+", "$1");
                                string thisXrefRid = Regex.Replace(matchLeftXref.Value, @"\A<xref [^>]*rid=""([^<>""]+)""[^>]*>[^<>]+</xref>, <xref [^>]*>[A-Z]?\d+", "$1");
                                string thisLabelLastItem = Regex.Replace(this.floatLabelById[thisXrefRid].ToString(), @"\A.*?([A-Z]?\d+)\W*?\Z", "$1");

                                if (string.Compare(thisXrefLastItem, thisLabelLastItem) != 0)
                                {
                                    leftXref = Regex.Replace(matchLeftXref.Value, @"(?=</xref>, )", "–" + thisLabelLastItem);
                                }
                            }

                            xref_replace = Regex.Replace(xref_replace, Regex.Escape(matchLeftXref.Value), leftXref);
                        }

                        // Parse the right xref
                        {
                            Match matchRightXref = Regex.Match(xref_replace, @"[A-Z]?\d+</xref>, <xref [^>]*>[^<>]+</xref>\Z");
                            string rightXref = matchRightXref.Value;
                            if (matchRightXref.Success)
                            {
                                string thisXrefFirstItem = Regex.Replace(matchRightXref.Value, @"[A-Z]?\d+</xref>, <xref [^>]*>([A-Z]?\d+)[^<>]*?</xref>\Z", "$1");
                                string thisXrefRid = Regex.Replace(matchRightXref.Value, @"[A-Z]?\d+</xref>, <xref [^>]*rid=""([^<>""]+)""[^>]*>[^<>]+</xref>\Z", "$1");
                                string thisLabelFirstItem = Regex.Replace(this.floatLabelById[thisXrefRid].ToString(), @"\A([A-Z]?\d+).*?\Z", "$1");

                                if (string.Compare(thisXrefFirstItem, thisLabelFirstItem) != 0)
                                {
                                    rightXref = Regex.Replace(matchRightXref.Value, @"(?<=, <xref [^>]*>)", thisLabelFirstItem + "–");
                                }
                            }

                            xref_replace = Regex.Replace(xref_replace, Regex.Escape(matchRightXref.Value), rightXref);
                        }

                        xref_group = Regex.Replace(xref_group, Regex.Escape(dashed.Value), xref_replace);
                    }

                    node.InnerXml = xref_group;
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            this.xml = this.xmlDocument.OuterXml;
        }
    }
}

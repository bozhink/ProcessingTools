namespace ProcessingTools.Base
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

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

    public class Floats : TaggerBase
    {
        private const int MaxNumberOfPunctuationSigns = 10;
        private const int MaxNumberOfSequentalFloats = 30;
        private const string SubfloatsPattern = "\\s*(([A-Za-z]\\d?|[ivx]+)[,\\s]*([,;–−-]|and|\\&amp;)\\s*)*([A-Za-z]\\d?|[ivx]+)";

        private readonly Regex selectDash = new Regex("[–—−-]");
        private readonly Regex selectNaNChar = new Regex(@"\D");

        private Hashtable floatIdByLabel = null;
        private IEnumerable floatIdByLabelKeys = null;
        private IEnumerable floatIdByLabelValues = null;
        private Hashtable floatLabelById = null;

        public Floats(Config config, string xml)
            : base(config, xml)
        {
            this.InitFloats();
        }

        public Floats(IBase baseObject)
            : base(baseObject)
        {
            this.InitFloats();
        }

        public void TagAllFloats()
        {
            // Force Fig. and Figs
            this.Xml = Regex.Replace(
                Regex.Replace(this.Xml, @"(Fig)\s", "$1. "),
                @"(Figs)\.",
                "$1");

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

            this.Xml = Regex.Replace(this.Xml, "\\s+ref-type=\"(map|plate|habitus)\"", " ref-type=\"fig\"");
        }

        public void TagTableFootNotes()
        {
            // Get list of table-wrap with correctly formatted foot-notes
            XmlNodeList tableWrapList = this.XmlDocument.SelectNodes("//table-wrap[table-wrap-foot[fn[label][@id]]]", this.NamespaceManager);
            if (tableWrapList.Count < 1)
            {
                Alert.Log("There is no table-wrap nodes with correctly formatted footnotes: table-wrap-foot/fn[@id][label]");
            }
            else
            {
                Console.WriteLine("Number of correctly formatted table-wrap-s: {0}", tableWrapList.Count);
                foreach (XmlNode tableWrap in tableWrapList)
                {
                    Hashtable tableFootnotes = new Hashtable();

                    // Get foot-note's label and corresponding @id-s
                    foreach (XmlNode fn in tableWrap.SelectNodes("//fn[label][@id]", this.NamespaceManager))
                    {
                        tableFootnotes.Add(fn["label"].InnerText.Trim(), fn.Attributes["id"].Value.Trim());
                    }

                    foreach (string tableFootnoteKey in tableFootnotes.Keys)
                    {
                        foreach (XmlNode footnoteSup in tableWrap.SelectNodes("//table//sup[normalize-space()='" + tableFootnoteKey + "']", this.NamespaceManager))
                        {
                            // <xref ref-type="table-fn" rid="TN1"></xref>
                            XmlNode xrefTableFootNote = this.XmlDocument.CreateElement("xref");

                            XmlAttribute refType = this.XmlDocument.CreateAttribute("ref-type");
                            refType.InnerXml = "table-fn";
                            xrefTableFootNote.Attributes.Append(refType);

                            XmlAttribute rid = this.XmlDocument.CreateAttribute("rid");
                            rid.InnerXml = tableFootnotes[tableFootnoteKey].ToString();
                            xrefTableFootNote.Attributes.Append(rid);

                            xrefTableFootNote.InnerXml = footnoteSup.OuterXml;

                            footnoteSup.InnerXml = xrefTableFootNote.OuterXml;
                        }
                    }
                }
            }

            this.Xml = Regex.Replace(this.Xml, @"<sup>(<xref ref-type=""table-fn"" [^>]*><sup>[^<>]*?</sup></xref>)</sup>", "$1");
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

        private void FormatXref()
        {
            string xml = this.Xml;

            // Format content between </xref> and <xref
            xml = Regex.Replace(xml, @"(?<=</xref>)\s*[–—−-]\s*(?=<xref)", "–");
            xml = Regex.Replace(xml, @"(?<=</xref>)\s*([,;])\s*(?=<xref)", "$1 ");
            xml = Regex.Replace(xml, @"(?<=</xref>)\s*(and|\\&amp;)\s*(?=<xref)", " $1 ");

            xml = Regex.Replace(xml, @"(<xref [^>]*>)\s*[–—−-]\s*(?=[A-Za-z0-9][^<>]*</xref>)", "–$1");

            // Remove xref from attributes
            for (int i = 0; i < 2 * MaxNumberOfSequentalFloats; i++)
            {
                xml = Regex.Replace(xml, "(<[^<>]+=\"[^<>\"]*)<[^<>]*>", "$1");
            }

            this.Xml = xml;
        }

        private void FormatXrefGroup(string refType)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (XmlNode xrefGroupNode in this.XmlDocument.SelectNodes("//xref-group[xref[@ref-type='" + refType + "']]", this.NamespaceManager))
                {
                    // Format content in xref-group
                    string xrefGroup = xrefGroupNode.InnerXml;

                    // <xref-group>Figures 109–112
                    for (Match dashed = Regex.Match(xrefGroup, "<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>[–—−-]<xref ref-type=\"" + refType + "\" [^>]*>[^<>]*?</xref>"); dashed.Success; dashed = dashed.NextMatch())
                    {
                        string xref_replace = dashed.Value;

                        string prefixId = Regex.Replace(xref_replace, @"<xref .*?rid=\W(.*?)\d+.*?>.*", "$1");
                        string firstId = Regex.Replace(xref_replace, @"\A<xref .*?(\d+).*?>.*", "$1");
                        string lastId = Regex.Replace(xref_replace, @".*[–—−-]<xref .*?(\d+).*?>.*", "$1");

                        int first = int.Parse(firstId);
                        int last = int.Parse(lastId);

                        // Parse the dash
                        // Convert the dash to a sequence of xref
                        {
                            sb.Clear();
                            for (int i = first + 1; i < last; i++)
                            {
                                string rid = prefixId + i;
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

                        xrefGroup = Regex.Replace(xrefGroup, Regex.Escape(dashed.Value), xref_replace);
                    }

                    xrefGroupNode.InnerXml = xrefGroup;
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        private string GetFloatId(ReferenceType refType, XmlNode node)
        {
            string id = string.Empty;
            if (node.Attributes["id"] != null)
            {
                id = node.Attributes["id"].InnerText;
            }
            else
            {
                switch (refType)
                {
                    case ReferenceType.Table:
                        try
                        {
                            id = node["table"].Attributes["id"].InnerText;
                        }
                        catch (Exception e)
                        {
                            Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0,
                                "There is no 'table-wrap/@id' or 'table-wrap/table' or 'table-wrap/table/@id'");
                        }

                        break;
                }
            }

            return id;
        }

        private string GetFloatLabelText(XmlNode node)
        {
            string labelText = string.Empty;
            if (node["label"] != null)
            {
                labelText = node["label"].InnerXml;
            }
            else if (node["title"] != null)
            {
                labelText = node["title"].InnerXml;
            }

            return labelText;
        }

        /// <summary>
        /// Gets the number of floating objects of a given type and populates label-and-id-related hash tables.
        /// This method generates the "dictionary" to correctly post-process xref/@rid references.
        /// </summary>
        /// <param name="refType">"Physical" type of the floating object: &lt;fig /&gt;, &lt;table-wrap /&gt;, &lt;boxed-text /&gt;, etc.</param>
        /// <param name="floatType">"Logical" type of the floating object: This string is supposed to be contained in the &lt;label /&gt; of the object.</param>
        /// <returns>Number of floating objects of type refType with label containing "floatType"</returns>
        private int GetFloats(ReferenceType refType = ReferenceType.Figure, string floatType = "Figure")
        {
            this.floatIdByLabel = new Hashtable();
            this.floatLabelById = new Hashtable();

            int numberOfFloatsOfType = 0;
            try
            {
                string xpath = this.GetFloatsXPath(refType, floatType);
                XmlNodeList floatsOfTypeNodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);
                foreach (XmlNode floatOfTypeNode in floatsOfTypeNodeList)
                {
                    string id = this.GetFloatId(refType, floatOfTypeNode);
                    string labelText = this.GetFloatLabelText(floatOfTypeNode);

                    this.UpdateFloatLabelByIdList(id, labelText);
                    this.UpdateFloatIdByLabelList(id, labelText);
                }

                numberOfFloatsOfType = floatsOfTypeNodeList.Count;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }

            this.floatIdByLabelKeys = this.floatIdByLabel.Keys;
            this.floatIdByLabelValues = this.floatIdByLabel.Values;

            this.PrintFloatsDistributionById(refType);

            return numberOfFloatsOfType;
        }

        private string GetFloatsXPath(ReferenceType refType, string floatType)
        {
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

            return xpath;
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

        private void ParseFloatIndexRangesInLabel(string id, string labelText, Match floatIndexInLabel, string currentFloatIndex)
        {
            Match dash = this.selectDash.Match(floatIndexInLabel.Value);
            if (dash.Success)
            {
                try
                {
                    int currentFloatIndexNumber = int.Parse(this.selectNaNChar.Replace(currentFloatIndex, string.Empty));

                    string nextFloatIndex = floatIndexInLabel.NextMatch().Success ? this.selectDash.Replace(floatIndexInLabel.NextMatch().Value, string.Empty) : string.Empty;
                    int nextFloatIndexNumber = int.Parse(this.selectNaNChar.Replace(nextFloatIndex, string.Empty));

                    if (currentFloatIndexNumber < nextFloatIndexNumber)
                    {
                        string prefix = Regex.Replace(currentFloatIndex, @"([A-Z]?)\d+", "$1");
                        for (int i = currentFloatIndexNumber + 1; i < nextFloatIndexNumber; ++i)
                        {
                            this.floatIdByLabel.Add(prefix + i, id);
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

        private void PrintFloatsDistributionById(ReferenceType refType)
        {
            Console.WriteLine();
            foreach (string floatId in this.floatIdByLabelKeys.Cast<string>().ToArray().OrderBy(s => s))
            {
                Console.WriteLine("{2}\t#{0}\tis in float\t#{1}", floatId, this.floatIdByLabel[floatId], refType.ToString());
            }
        }

        private void ProcessFloatsRid(int floatsNumber, string refType)
        {
            string xml = this.Xml;

            foreach (string s in this.floatIdByLabelKeys)
            {
                xml = Regex.Replace(xml, "<xref ref-type=\"" + refType + "\" rid=\"" + s + "\">", "<xref ref-type=\"" + refType + "\" rid=\"" + this.floatIdByLabel[s] + "\">");
            }

            foreach (string s in this.floatIdByLabelValues.Cast<string>().Select(c => c).Distinct().ToList())
            {
                for (int j = 0; j < MaxNumberOfSequentalFloats; j++)
                {
                    xml = Regex.Replace(xml, "((<xref ref-type=\"" + refType + "\" rid=\"" + s + "\">)[^<>]*)</xref>\\s*[–—−-]\\s*\\2", "$1–");
                }
            }

            this.Xml = xml;
        }

        private void RemoveXrefInTitles()
        {
            string xpath = "//fig//label[xref]|//fig//title[xref]|//table-wrap//label[xref]|//table-wrap//title[xref]";
            try
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "<xref [^>]*>|</?xref>", string.Empty);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
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

            string xml = this.Xml;
            xml = Regex.Replace(xml, pattern, replace);

            pattern = this.FloatsNextOccurencePattern(floatType);
            replace = this.FloatsNextOccurenceReplace(floatType);
            for (int i = 0; i < MaxNumberOfSequentalFloats; i++)
            {
                xml = Regex.Replace(xml, pattern, replace);
            }

            this.Xml = xml;
        }

        private void UpdateFloatIdByLabelList(string id, string labelText)
        {
            for (Match floatIndexInLabel = Regex.Match(labelText, @"[A-Z]?\d+([–—−-](?=[A-Z]?\d+))?"); floatIndexInLabel.Success; floatIndexInLabel = floatIndexInLabel.NextMatch())
            {
                string currentFloatIndex = this.selectDash.Replace(floatIndexInLabel.Value, string.Empty);
                this.floatIdByLabel.Add(currentFloatIndex, id);

                this.ParseFloatIndexRangesInLabel(id, labelText, floatIndexInLabel, currentFloatIndex);
            }
        }

        private void UpdateFloatLabelByIdList(string id, string labelText)
        {
            if (Regex.Match(labelText, @"\A\w+\s+([A-Za-z]?\d+\W*)+\Z").Success)
            {
                this.floatLabelById.Add(id, Regex.Replace(labelText, @"\A\w+\s+(([A-Za-z]?\d+\W*?)+)[\.;,:–—−-]*\s*\Z", "$1"));
            }
        }
    }
}

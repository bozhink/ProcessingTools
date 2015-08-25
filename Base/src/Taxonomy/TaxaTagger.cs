namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;

    public class TaxaTagger : TaggerBase
    {
        public const string HigherTaxaMatchPattern = "\\b([A-Z](?i)[a-z]*(morphae?|mida|toda|ideae|oida|genea|formes|ales|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\\b";
        private const string HigherTaxaXPathTemplate = "//p[{0}]|//td[{0}]|//th[{0}]|//li[{0}]|//article-title[{0}]|//title[{0}]|//label[{0}]|//ref[{0}]|//kwd[{0}]|//tp:nomenclature-citation[{0}]|//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48'][{0}]";
        private const string HigherTaxaReplacePattern = "<tn type=\"higher\">$1</tn>";
        private const string LowerRaxaReplacePattern = "<tn type=\"lower\">$1</tn>";
        private const string SelectTreatmentGeneraXPathString = "//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tn/tn-part[@type='genus']";

        public TaxaTagger(string xml)
            : base(xml)
        {
        }

        public TaxaTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public TaxaTagger(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public static string TagItalics(string nodeXml)
        {
            string result = nodeXml;

            // Genus (Subgenus)? species subspecies?
            result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]*)(?=</i>)", LowerRaxaReplacePattern);
            result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)", LowerRaxaReplacePattern);
            result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]*)(?=</i>)", LowerRaxaReplacePattern);
            result = Regex.Replace(result, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)", LowerRaxaReplacePattern);
            result = Regex.Replace(result, @"(?<=<i>)([A-Z\.-]{3,30})(?=</i>)", LowerRaxaReplacePattern);

            result = Regex.Replace(result, @"‘<i>(<tn type=""lower"">)([A-Z][a-z\.×]+)(</tn>)</i>’\s*<i>([a-z\.×-]+)</i>", "$1‘$2’ $4$3");


            result = TagInfraspecificTaxa(result);

            return result;
        }

        public static string TagInfraspecificTaxa(string nodeXml)
        {
            string replace = nodeXml;
            string infraspecificPattern;

            // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
            infraspecificPattern = @"<i><tn type=""lower"">([^<>]*?)</tn></i>\s*((?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?)\s*<i>([a-z\s-]+)</i>";
            replace = Regex.Replace(
                replace,
                infraspecificPattern,
                @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");

            // Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
            {
                const string Subpattern = @"(?![,\.])(?!\s+and\b)(?!\s+as\b)(?!\s+to\b)\s*([^<>\(\)\[\]]{0,40}?)\s*(\(\s*)?((?i)\b(?:subgen(?:us)?|subg|ser|(?:sub)?sect(?:ion)?)\b\.?)\s*(?:<i>)?(?:<tn type=""lower"">)?([A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

                infraspecificPattern = @"<i><tn type=""lower"">([A-Za-z\.-]+)</tn></i>" + Subpattern;
                for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                {
                    replace = Regex.Replace(
                        replace,
                        infraspecificPattern,
                        @"<tn type=""lower""><genus>$1</genus> <genus-authority>$2</genus-authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                }

                replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside

                infraspecificPattern = @"(?<=</infraspecific>\s*\)?)</tn>" + Subpattern;
                for (int i = 0; i < 3; i++)
                {
                    for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                    {
                        replace = Regex.Replace(
                            replace,
                            infraspecificPattern,
                            " <authority>$1</authority> $2<infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>$5");
                    }
                }

                replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
            }

            // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
            // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
            {
                infraspecificPattern = @"<i><tn type=""lower"">([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]]{0,3}?\([^<>\(\)\[\]]{0,30}?\)[^<>\(\)\[\]]{0,30}?|[^<>\(\)\[\]]{0,30}?)?)\s*((?i)(?:\b(?:ab?|sp|var|subvar|subvar|subsp|subspecies|ssp|f|forma?|st|r|sf|cf|nr|near|sp\. near|aff|prope|(?:sub)?sect)\b\.?)|×|\?)\s*<i>([a-z-]+)</i>";
                for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                {
                    replace = Regex.Replace(
                        replace,
                        infraspecificPattern,
                        @"<tn type=""lower""><basionym>$1</basionym> <basionym-authority>$2</basionym-authority> <infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>");
                }

                replace = Regex.Replace(
                    replace,
                    @"(?<=</infraspecific>\s*\)?)</tn>\s*<i>([A-Za-z\.\s-]+)</i>",
                    " <species>$1</species></tn>");

                infraspecificPattern = @"(?<=(?:</infraspecific>|</species>)\s*\)?)</tn>\s*([^<>]{0,100}?)\s*((?i)(?:\b(?:ab?|n?\.?\s*sp|var|subvar|subsp|subspecies|ssp|subspec|f|fo|forma?|st|r|sf|cf|nr|near|aff|prope|(?:sub)?sect)\b\.?)|×|\?)\s*<i>([a-z-]+)</i>";
                for (int i = 0; i < 4; i++)
                {
                    for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
                    {
                        replace = Regex.Replace(
                            replace,
                            infraspecificPattern,
                            " <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$3</infraspecific></tn>");
                    }
                }
            }

            // Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
            replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z\.-]+)\s+([a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

            replace = Regex.Replace(replace, " <([a-z-]+)?authority></([a-z-]+)?authority>", string.Empty);

            return replace;
        }

        public void TagLowerTaxa(bool tagBasionym = false)
        {
            string xpath = string.Empty;
            if (this.Config.NlmStyle)
            {
                if (this.Config.TagWholeDocument)
                {
                    xpath = "//*[i]";
                }
                else
                {
                    xpath = "//p[.//i]|//ref[.//i]|//kwd[.//i]|//article-title[.//i]|//li[.//i]|//th[.//i]|//td[.//i]|//title[.//i]|//label[.//i]|//tp:nomenclature-citation[.//i]";
                }
            }
            else
            {
                if (this.Config.TagWholeDocument)
                {
                    xpath = "//*[i]";
                }
                else
                {
                    xpath = "//p[.//i]|//li[.//i]|//td[.//i]|//th[.//i]";
                }
            }

            /*
             * The following piece of code will be executed twice: once for lower-level-content-holding tags, and next for all value tags (System)
             */
            try
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = TaxaTagger.TagItalics(node.InnerXml);
                }

                if (!this.Config.NlmStyle && !this.Config.TagWholeDocument)
                {
                    foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//i]", this.NamespaceManager))
                    {
                        node.InnerXml = TaxaTagger.TagItalics(node.InnerXml);
                    }
                }

                this.RemoveTaxaInWrongPlaces();

                if (tagBasionym)
                {
                    foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                    {
                        node.InnerXml = TaxaTagger.TagInfraspecificTaxa(node.InnerXml);
                    }

                    if (!this.Config.NlmStyle && !this.Config.TagWholeDocument)
                    {
                        foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//tn[@type='lower']]", this.NamespaceManager))
                        {
                            node.InnerXml = TaxaTagger.TagInfraspecificTaxa(node.InnerXml);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag taxa.");
            }
        }

        public void TagHigherTaxa()
        {
            try
            {
                Regex matchHigherTaxa = new Regex(HigherTaxaMatchPattern);

                IEnumerable<string> taxaNames = GetNonTaggedTaxa(matchHigherTaxa);

                //Alert.Log(taxaNames.Count());

                taxaNames = taxaNames.ClearListWithXDocument(this.GetBlackList());

                //Alert.Log(taxaNames.Count());

                ////IEnumerable<string> whiteListedTaxa = this.TextWords.SelectListWithXDocument(this.GetWhiteList());
                ////foreach (string taxon in whiteListedTaxa)
                ////{
                ////    Alert.Log(taxon);
                ////}

                // TODO: Clear taxaNames by black List

                TagContent tag = new TagContent("tn", @" type=""higher""");
                TagTextInXmlDocument(taxaNames, tag, HigherTaxaXPathTemplate, false, true);

                // TODO: Refactor
                foreach (XmlNode node in this.XmlDocument.SelectNodes(".//tn[.//tn]", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "<tn [^>]*>|</tn>", string.Empty);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tagging higher taxa.");
            }

            this.ApplyWhiteList();
            this.RemoveTaxaInWrongPlaces();
        }

        private IEnumerable<string> GetNonTaggedTaxa(Regex matchTaxa)
        {
            return from item in this.XmlDocument.GetMatchesInXmlText(matchTaxa, true)
                   where this.XmlDocument.SelectNodes("//tn[contains(string(.),'" + item + "')]", this.NamespaceManager).Count == 0
                   select item;
        }

        public void UntagTaxa()
        {
            try
            {
                this.RemoveFalseTaxaOfPersonNames();

                this.ApplyBlackList();

                this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, @"<tn type=""higher"">([a-z]+)</tn>", "$1");
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        public void FormatTreatments()
        {
            try
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//tp:nomenclature", this.NamespaceManager))
                {
                    if (node["title"] != null)
                    {
                        node["title"].InnerXml = Regex.Replace(node["title"].InnerXml, "</?i>", string.Empty);
                        node["title"].InnerXml = Regex.Replace(node["title"].InnerXml, "\\s+", " ");
                    }

                    node.InnerXml = Regex.Replace(node.InnerXml, @"(?<=<title [^>]*>)\s+|\s+(?=</title>)", string.Empty);

                    string replace = node.InnerXml;

                    /*
                     * Extract label preceding lower taxa and Authority and Status tags
                     */
                    replace = Regex.Replace(
                        replace,
                        @"(\s*)<title[^>]*>([^<>]+?)\s*(<tn [^>]*>.*?</tn>)\s*([^<>]*)</title>",
                        "$1<label>$2</label>$1$3$1<tp:taxon-authority>$4</tp:taxon-authority>");
                    /*
                     * Extract Authority and Status tags if there is no label
                     */
                    replace = Regex.Replace(
                        replace,
                        @"(\s*)<title[^>]*>(<tn [^>]*>.*?</tn>)\s*([^<>]*)</title>",
                        "$1$2$1<tp:taxon-authority>$3</tp:taxon-authority>");

                    replace = Regex.Replace(replace, @"\s*<tp:taxon-authority>\s*</tp:taxon-authority>", string.Empty);
                    /*
                     * Format nomenclature
                     */
                    replace = Regex.Replace(
                        replace,
                        @"(?<=</label>)(\s*)(<tn [^>]*>)(<tn-part[\s\S]*?)(</tn>)",
                        "$1$2$1    $3$1$4");
                    replace = Regex.Replace(
                        replace,
                        @"^(\s*)(<tn [^>]*>)(<tn-part[\s\S]*?)(</tn>)",
                        "$1$2$1    $3$1$4");
                    for (int i = 0; i < 8; i++)
                    {
                        replace = Regex.Replace(
                            replace,
                            @"(\n\s*)(\(?<tn-part [^>]*>.*?</tn-part>\)?) (\(?<tn-part [^>]*>.*?</tn-part>\)?)",
                            "$1$2$1$3");
                    }

                    /*
                     * Split authority and status
                     */
                    replace = Regex.Replace(
                        replace,
                        @"<tp:taxon-authority>((([a-z]+\.(\s*)(n|nov))|(n\.\s*[a-z]+)|(([a-z]+\.)?(\s*)spp))(\.)?|new record)</tp:taxon-authority>",
                        "<tp:taxon-status>$1</tp:taxon-status>");
                    replace = Regex.Replace(
                        replace,
                        @"(\s*)<tp:taxon-authority>([\w\-\,\;\.\(\)\&\s-]+)(\s*\W\s*)([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|[a-z]+\.\s*(n|nov|r|rev)(\.)?|new record)</tp:taxon-authority>",
                        "$1<tp:taxon-authority>$2</tp:taxon-authority>$1<tp:taxon-status>$4</tp:taxon-status>");
                    replace = Regex.Replace(replace, @"(<tp:taxon-authority>.*)((?<!&[a-z]+)[\s,;:]+)(</tp:taxon-authority>)", "$1$3");
                    replace = Regex.Replace(replace, @"(<tp:taxon-authority>.*?</tp:taxon-authority>\S*)\s+?(\n?)", "$1\n");
                    replace = Regex.Replace(replace, @"(?<=<tp:taxon-authority>)\s+|\s+(?=</tp:taxon-authority>)", string.Empty);

                    node.InnerXml = replace;

                    if (node["object-id"] != null)
                    {
                        if (node["tn"] != null)
                        {
                            foreach (XmlNode objectId in node.SelectNodes("./object-id", this.NamespaceManager))
                            {
                                node["tn"].AppendChild(objectId);
                            }

                            node["tn"].InnerXml = Regex.Replace(node["tn"].InnerXml, "(?=<object-id)", "    ");
                        }

                        node.InnerXml = Regex.Replace(node.InnerXml, @"\n\s*\n", "\n");
                    }
                }

                this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, @"(\s*)(    <object-id .*?</object-id>)(</tn>)", "$1$2$1$3");
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        public void ParseTreatmentMetaWithAphia()
        {
            try
            {
                List<string> genusList = this.XmlDocument.GetStringListOfUniqueXmlNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager);

                bool delay = false;
                foreach (string genus in genusList)
                {
                    if (delay)
                    {
                        System.Threading.Thread.Sleep(15000);
                    }
                    else
                    {
                        delay = true;
                    }

                    Console.WriteLine("\n{0}\n", genus);

                    XmlDocument response = Net.SearchAphia(genus);

                    List<string> responseFamily = response.GetStringListOfUniqueXmlNodes("//return/item[string(genus)='" + genus + "']/family", NamespaceManager);
                    List<string> responseOrder = response.GetStringListOfUniqueXmlNodes("//return/item[string(genus)='" + genus + "']/order", NamespaceManager);
                    List<string> responseKingdom = response.GetStringListOfUniqueXmlNodes("//return/item[string(genus)='" + genus + "']/kingdom", NamespaceManager);

                    this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");
                    this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");
                    this.ReplaceTreatmentMetaClassificationItem(responseFamily, genus, "family");
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        public void ParseTreatmentMetaWithGbif()
        {
            try
            {
                List<string> genusList = this.XmlDocument.GetStringListOfUniqueXmlNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager);

                bool delay = false;
                foreach (string genus in genusList)
                {
                    if (delay)
                    {
                        System.Threading.Thread.Sleep(15000);
                    }
                    else
                    {
                        delay = true;
                    }

                    Console.WriteLine("\n{0}\n", genus);

                    Json.Gbif.GbifResult obj = Net.SearchGbif(genus);
                    if (obj != null)
                    {
                        Console.WriteLine("{0} .... {1} .... {2}", genus, obj.scientificName, obj.canonicalName);

                        if (obj.canonicalName != null || obj.scientificName != null)
                        {
                            if (!obj.canonicalName.Equals(genus) && !obj.scientificName.Contains(genus))
                            {
                                Alert.Log("No match.");
                            }
                            else
                            {
                                Alert.Log("Kingdom: " + obj.kingdom);
                                Alert.Log("Order: " + obj.order);
                                Alert.Log("Family: " + obj.family);
                                Alert.Log();

                                List<string> responseKingdom = new List<string>();
                                List<string> responseOrder = new List<string>();
                                List<string> responseFamily = new List<string>();

                                responseKingdom.Add(obj.kingdom);
                                responseOrder.Add(obj.order);
                                responseFamily.Add(obj.family);

                                if (obj.alternatives != null)
                                {
                                    foreach (var alternative in obj.alternatives)
                                    {
                                        if (alternative.canonicalName.Equals(genus) || alternative.scientificName.Contains(genus))
                                        {
                                            responseKingdom.Add(alternative.kingdom);
                                            responseOrder.Add(alternative.order);
                                            responseFamily.Add(alternative.family);
                                        }
                                    }
                                }

                                this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");
                                this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");
                                this.ReplaceTreatmentMetaClassificationItem(responseFamily, genus, "family");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        public void ParseTreatmentMetaWithCoL()
        {
            try
            {
                List<string> genusList = this.XmlDocument.GetStringListOfUniqueXmlNodes(SelectTreatmentGeneraXPathString, this.NamespaceManager);

                bool delay = false;
                foreach (string genus in genusList)
                {
                    if (delay)
                    {
                        System.Threading.Thread.Sleep(15000);
                    }
                    else
                    {
                        delay = true;
                    }

                    Console.WriteLine("\n{0}\n", genus);

                    XmlDocument response = Net.SearchCatalogueOfLife(genus);

                    if (response != null)
                    {
                        List<string> responseFamily = response.GetStringListOfUniqueXmlNodes("/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Family']/name", NamespaceManager);
                        List<string> responseOrder = response.GetStringListOfUniqueXmlNodes("/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Order']/name", NamespaceManager);
                        List<string> responseKingdom = response.GetStringListOfUniqueXmlNodes("/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Kingdom']/name", NamespaceManager);

                        this.ReplaceTreatmentMetaClassificationItem(responseKingdom, genus, "kingdom");
                        this.ReplaceTreatmentMetaClassificationItem(responseOrder, genus, "order");
                        this.ReplaceTreatmentMetaClassificationItem(responseFamily, genus, "family");
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        // Flora-like tagging methods
        public void PerformFloraReplace(string xmlTemplate)
        {
            XmlDocument template = new XmlDocument();
            template.LoadXml(xmlTemplate);

            XmlNode root = template.DocumentElement;
            Alert.Log(root.ChildNodes.Count);

            for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
            {
                XmlNode taxon = root.ChildNodes.Item(i);
                XmlNode find = taxon.FirstChild;
                XmlNode replace = taxon.LastChild;

                Alert.Log(find.InnerXml);

                string pattern = find.InnerXml;
                pattern = Regex.Replace(pattern, @"\.", "\\.");
                pattern = Regex.Replace(pattern, @"(?<=\w)\s+(?=\w)", @"\b\s*\b");
                pattern = Regex.Replace(pattern, @"(?<=\W)\s+(?=\w)", @"?\s*\b");
                pattern = Regex.Replace(pattern, @"(?<=\W)\s+", @"?\s*");
                pattern = Regex.Replace(pattern, @"\bvar\b", "(var|v)");

                pattern = "(?i)" + pattern;

                this.Xml = Regex.Replace(
                    this.Xml,
                    "(?<![a-z-])(?<!<[^>]+=\"[^>]*)(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
                    "<tn>$1</tn>");
            }

            ////string infraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
            ////string lowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

            ////xml = Regex.Replace(xml, infraspecificPattern + "\\s*<tn>", "<tn>$1 ");
            ////xml = Regex.Replace(xml, "(?<!<tn>)(" + infraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>");

            ////xml = Regex.Replace(xml, @"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>");
            ////xml = Regex.Replace(xml, "(<tn>)" + infraspecificPattern + "</tn>\\s+<tn>", "$1$2 ");

            ////xml = Regex.Replace(xml, "</tn>\\s*<tn>" + infraspecificPattern, " $1");

            ////// TODO: Here we must remove tn/tn
            ////{
            ////    ParseXmlStringToXmlDocument();
            ////    XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
            ////    foreach (XmlNode node in nodeList)
            ////    {
            ////        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
            ////    }
            ////    xml = xmlDocument.OuterXml;
            ////}

            ////// Guess new taxa:
            ////for (int i = 0; i < 10; i++)
            ////{
            ////    xml = Regex.Replace(xml,
            ////        "(</tn>,?(\\s+and)?\\s+)(" + infraspecificPattern + "?" + lowerPattern + ")",
            ////        "$1<tn>$3</tn>");
            ////}

            //// Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
            ////xml = Regex.Replace(xml, @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)", "$1<tn>$2</tn>");

            ////xml = Regex.Replace(xml, "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + infraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)", "<tn>$1</tn>");

            ////xml = Regex.Replace(xml,
            ////    "(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + infraspecificPattern + ")?" + lowerPattern + ")",
            ////    "<tn>$1</tn>");

            ////// TODO: Here we must remove tn/tn
            ////{
            ////    ParseXmlStringToXmlDocument();
            ////    XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
            ////    foreach (XmlNode node in nodeList)
            ////    {
            ////        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
            ////    }
            ////    xml = xmlDocument.OuterXml;
            ////}

            ////// Remove taxa in toTaxon
            ////{
            ////    ParseXmlStringToXmlDocument();
            ////    XmlNodeList nodeList = xmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
            ////    foreach (XmlNode node in nodeList)
            ////    {
            ////        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
            ////    }
            ////    xml = xmlDocument.OuterXml;
            ////}
        }

        private void RemoveFalseTaxaOfPersonNames()
        {
            try
            {
                List<string> firstWordTaxaList = GetFirstWordOfTaxaNames();

                char[] charsToSplit = new char[] { ' ', ',', ';' };
                List<string> personNameParts = this.XmlDocument.SelectNodes("//surname[string-length(normalize-space(.)) > 2]|//given-names[string-length(normalize-space(.)) > 2]")
                    .Cast<XmlNode>().Select(s => s.InnerText).Distinct().ToList();

                foreach (string taxon in firstWordTaxaList)
                {
                    if (taxon.IndexOf('.') < 0)
                    {
                        Regex matchTaxonInName = new Regex("(?i)\\b" + Regex.Escape(taxon) + "\\b");
                        IEnumerable<string> queryResult = from item in personNameParts
                                                          where matchTaxonInName.Match(item).Success
                                                          select matchTaxonInName.Match(item).Value;

                        foreach (string item in queryResult)
                        {
                            if (item.IndexOf('.') < 0)
                            {
                                this.XmlDocument.InnerXml = Regex.Replace(this.XmlDocument.InnerXml, "<tn [^>]*>((?i)" + item + "(\\s+.*?)?(\\.?))</tn>", "$1");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
            }
        }

        private void RemoveTaxaInWrongPlaces()
        {
            string xpath = "tn[.//tn] | a[.//tn] | ext-link[.//tn] | xref[.//tn] | article/front/notes/sec[.//tn] | tp:treatment-meta/kwd-group/kwd/named-content[.//tn] | *[@object_id='82'][.//tn] | *[@id='41'][.//tn] | *[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value[.//tn]";
            Regex matchTaxonTag = new Regex(@"</?tn[^>]*>");
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                node.InnerXml = matchTaxonTag.Replace(node.InnerXml, string.Empty);
            }
        }

        private XElement GetWhiteList()
        {
            return XElement.Load(this.Config.whiteListXmlFilePath);
        }

        private XElement GetBlackList()
        {
            return XElement.Load(this.Config.blackListXmlFilePath);
        }

        private void ApplyBlackList()
        {
            try
            {
                List<string> firstWordTaxaList = GetFirstWordOfTaxaNames();

                XElement blackList = GetBlackList();

                string xml = this.XmlDocument.InnerXml;
                foreach (string taxon in firstWordTaxaList)
                {
                    IEnumerable<string> queryResult = from item in blackList.Elements()
                                                      where Regex.Match(taxon, "(?i)" + item.Value).Success
                                                      select item.Value;
                    foreach (string item in queryResult)
                    {
                        xml = Regex.Replace(xml, "<tn [^>]*>((?i)" + item + "(\\s+.*?)?(\\.?))</tn>", "$1");
                    }
                }

                this.XmlDocument.InnerXml = xml;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Apply black list.");
            }
        }

        private List<string> GetFirstWordOfTaxaNames()
        {
            List<string> firstWordTaxaList = this.XmlDocument.GetStringListOfUniqueXmlNodes("//tn", this.NamespaceManager)
                .Cast<string>().Select(c => Regex.Match(c, @"\w+\.|\w+\b").Value).Distinct().ToList();
            return firstWordTaxaList;
        }

        private void ApplyWhiteList()
        {
            try
            {
                XElement whiteList = XElement.Load(this.Config.whiteListXmlFilePath);
                IEnumerable<string> whiteListItems = from item in whiteList.Elements()
                                                     select item.Value;

                foreach (string item in whiteListItems)
                {
                    this.XmlDocument.InnerXml = Regex.Replace(
                        this.XmlDocument.InnerXml,
                        "(?<!<tn [^>]*>)(?<!name [^>]*>)(?<!<[^>]+=\"[^>]*)(?i)\\b(" + item + ")\\b(?!\"\\s?>)(?!</tn)(?!</tp:)(?!</named-content></kwd>)",
                        HigherTaxaReplacePattern);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Applying white list.");
            }
        }

        const string TreatmentMetaReplaceXPathTemplate = "//tp:taxon-treatment[string(tp:nomenclature/tn/tn-part[@type='genus'])='{0}']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='{1}']";

        private void ReplaceTreatmentMetaClassificationItem(List<string> higherTaxaOfType, string genus, string type)
        {
            if (higherTaxaOfType.Count == 1)
            {
                string taxonName = higherTaxaOfType[0];

                Alert.Log("{0}: {1}\t--\t{2}", genus, type, taxonName);

                string xpath = string.Format(TreatmentMetaReplaceXPathTemplate, genus, type);
                foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
                {
                    node.InnerXml = taxonName;
                }
            }
            else
            {
                Alert.Log("WARNING: Multiple or zero matches of type {0}:", type);
                foreach (string taxonName in higherTaxaOfType)
                {
                    Alert.Log("{0}: {1}\t--\t{2}", genus, type, taxonName);
                }

                Alert.Log();
            }
        }
    }
}

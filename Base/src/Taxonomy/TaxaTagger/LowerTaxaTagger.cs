namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class LowerTaxaTagger : TaxaTagger
    {
        public LowerTaxaTagger(string xml, IStringDataList whiteList, IStringDataList blackList)
            : base(xml, whiteList, blackList)
        {
        }

        public LowerTaxaTagger(Config config, string xml, IStringDataList whiteList, IStringDataList blackList)
            : base(config, xml, whiteList, blackList)
        {
        }

        public LowerTaxaTagger(IBase baseObject, IStringDataList whiteList, IStringDataList blackList)
            : base(baseObject, whiteList, blackList)
        {
        }

        public override void Tag()
        {
            string xpath = this.SetLowerTaxaMatchXPath();

            try
            {
                this.TagLowerTaxa(xpath);

                // TODO: Must be corrected.
                this.ClearLowerTaxa();

                this.AdvancedTagLowerTaxa(xpath);
            }
            catch
            {
                throw;
            }
        }

        public override void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }

        private string TagInfraspecificTaxa(string nodeXml)
        {
            string replace = nodeXml;

            {
                // Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
                const string InfraspecificPattern = @"<i><tn type=""lower"">([^<>]*?)</tn></i>\s*((?:\(\s*)?(?i)(?:\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)(?:\s*\))?)\s*<i>([a-z\s-]+)</i>";
                Regex re = new Regex(InfraspecificPattern);

                replace = re.Replace(
                    replace,
                    @"<tn type=""lower""><basionym>$1</basionym> <sensu>$2</sensu> <specific>$3</specific></tn>");
            }

            // Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
            {
                const string Subpattern = @"(?![,\.])(?!\s+and\b)(?!\s+as\b)(?!\s+to\b)\s*([^<>\(\)\[\]]{0,40}?)\s*(\(\s*)?((?i)\b(?:subgen(?:us)?|subg|ser|trib|(?:sub)?sect(?:ion)?)\b\.?)\s*(?:<i>)?(?:<tn type=""lower"">)?([A-Za-z\.-]+(?:\s+[a-z\s\.-]+){0,3})(?:</tn>)?(?:</i>)?(\s*\))?";

                {
                    const string InfraspecificPattern = @"<i><tn type=""lower"">([A-Za-z\.-]+)</tn></i>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                    {
                        replace = re.Replace(
                            replace,
                            @"<tn type=""lower""><genus>$1</genus> <genus-authority>$2</genus-authority> $3<infraspecific-rank>$4</infraspecific-rank> <infraspecific>$5</infraspecific></tn>$6");
                    }

                    replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }

                {
                    const string InfraspecificPattern = @"(?<=</infraspecific>\s*\)?)</tn>" + Subpattern;
                    Regex re = new Regex(InfraspecificPattern);

                    for (int i = 0; i < 3; i++)
                    {
                        for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                        {
                            replace = re.Replace(
                                replace,
                                " <authority>$1</authority> $2<infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>$5");
                        }
                    }

                    replace = Regex.Replace(replace, @"(?<=\(\s*<infraspecific[^\)]*?)(</tn>)(\s*\))", "$2$1"); // Move closing bracket in tn if it is outside
                }
            }

            // <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
            // <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
            {
                {
                    const string InfraspecificPattern = @"<i><tn type=""lower"">([^<>]*?)</tn></i>(?![,\.])\s*((?:[^<>\(\)\[\]]{0,3}?\([^<>\(\)\[\]]{0,30}?\)[^<>\(\)\[\]]{0,30}?|[^<>\(\)\[\]]{0,30}?)?)\s*((?i)(?:\b(?:ab?|sp|var|subvar|subvar|subsp|subspecies|ssp|f|forma?|st|r|sf|cf|gr|nr|near|sp\. near|aff|prope|(?:sub)?sect)\b\.?)|×|\?)\s*<i>([a-z-]+)</i>";
                    Regex re = new Regex(InfraspecificPattern);

                    for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                    {
                        replace = re.Replace(
                            replace,
                            @"<tn type=""lower""><basionym>$1</basionym> <basionym-authority>$2</basionym-authority> <infraspecific-rank>$3</infraspecific-rank> <infraspecific>$4</infraspecific></tn>");
                    }

                    replace = Regex.Replace(
                        replace,
                        @"(?<=</infraspecific>\s*\)?)</tn>\s*<i>([A-Za-z\.\s-]+)</i>",
                        " <species>$1</species></tn>");
                }

                {
                    const string InfraspecificPattern = @"(?<=(?:</infraspecific>|</species>)\s*\)?)</tn>\s*([^<>]{0,100}?)\s*((?i)(?:\b(?:ab?|n?\.?\s*sp|var|subvar|subsp|subspecies|ssp|subspec|f|fo|forma?|st|r|sf|cf|gr|nr|near|aff|prope|(?:sub)?sect)\b\.?)|×|\?)\s*<i>([a-z-]+)</i>";
                    Regex re = new Regex(InfraspecificPattern);

                    for (int i = 0; i < 4; i++)
                    {
                        for (Match m = re.Match(replace); m.Success; m = m.NextMatch())
                        {
                            replace = re.Replace(
                                replace,
                                " <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$3</infraspecific></tn>");
                        }
                    }
                }
            }

            // Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
            replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z\.-]+)\s+([a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

            replace = Regex.Replace(replace, " <([a-z-]+)?authority></([a-z-]+)?authority>", string.Empty);

            return replace;
        }

        private string TagItalics(string nodeXml)
        {
            string result = nodeXml;

            // Genus (Subgenus)? species subspecies?
            result = Regex.Replace(
                result,
                @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]*)(?=</i>)",
                TaxaTagger.LowerRaxaReplacePattern);

            result = Regex.Replace(
                result,
                @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)",
                TaxaTagger.LowerRaxaReplacePattern);

            result = Regex.Replace(
                result,
                @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]*)(?=</i>)",
                TaxaTagger.LowerRaxaReplacePattern);

            result = Regex.Replace(
                result,
                @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)",
                TaxaTagger.LowerRaxaReplacePattern);

            result = Regex.Replace(
                result,
                @"(?<=<i>)([A-Z\.-]{3,30})(?=</i>)",
                TaxaTagger.LowerRaxaReplacePattern);

            result = Regex.Replace(
                result,
                @"‘<i>(<tn type=""lower"">)([A-Z][a-z\.×]+)(</tn>)</i>’\s*<i>([a-z\.×-]+)</i>",
                "$1‘$2’ $4$3");

            result = this.TagInfraspecificTaxa(result);

            return result;
        }

        private void AdvancedTagLowerTaxa(string xpath)
        {
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                node.InnerXml = this.TagInfraspecificTaxa(node.InnerXml);
            }

            if ((this.Config.ArticleSchemaType == SchemaType.System) && !this.Config.TagWholeDocument)
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//tn[@type='lower']]", this.NamespaceManager))
                {
                    node.InnerXml = this.TagInfraspecificTaxa(node.InnerXml);
                }
            }
        }

        private void ClearLowerTaxa()
        {
            IEnumerable<string> lowerTaxaNames = this.XmlDocument.ExtractTaxa(true, TaxaType.Lower);
            IEnumerable<string> taxaNames = this.ClearFakeTaxaNames(lowerTaxaNames);
            IEnumerable<string> fakeTaxaNames = lowerTaxaNames.DistinctWithStringList(taxaNames, true, true);

            foreach (string fakeTaxon in fakeTaxaNames)
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes(".//tn[@type='lower'][contains(string(.), '" + fakeTaxon + "')]", this.NamespaceManager))
                {
                    node.ReplaceXmlNodeByItsInnerXml();
                }
            }

            this.XmlDocument.RemoveTaxaInWrongPlaces();
        }

        private string SetLowerTaxaMatchXPath()
        {
            string xpath = string.Empty;

            switch (this.Config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    if (this.Config.TagWholeDocument)
                    {
                        xpath = "//*[i]";
                    }
                    else
                    {
                        xpath = "//p[.//i]|//ref[.//i]|//kwd[.//i]|//article-title[.//i]|//li[.//i]|//th[.//i]|//td[.//i]|//title[.//i]|//label[.//i]|//tp:nomenclature-citation[.//i]";
                    }

                    break;

                default:
                    if (this.Config.TagWholeDocument)
                    {
                        xpath = "//*[i]";
                    }
                    else
                    {
                        xpath = "//p[.//i]|//li[.//i]|//td[.//i]|//th[.//i]";
                    }

                    break;
            }

            return xpath;
        }

        private void TagLowerTaxa(string xpath)
        {
            /*
             * The following piece of code will be executed twice: once for lower-level-content-holding tags, and next for all value tags (System)
             */
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                node.InnerXml = this.TagItalics(node.InnerXml);
            }

            if ((this.Config.ArticleSchemaType == SchemaType.System) && !this.Config.TagWholeDocument)
            {
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//value[.//i]", this.NamespaceManager))
                {
                    node.InnerXml = this.TagItalics(node.InnerXml);
                }
            }
        }
    }
}
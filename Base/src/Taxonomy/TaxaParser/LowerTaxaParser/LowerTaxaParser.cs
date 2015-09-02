namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class LowerTaxaParser : Base, IParser
    {
        public LowerTaxaParser(string xml)
            : base(xml)
        {
        }

        public LowerTaxaParser(Config config, string xml)
            : base(config, xml)
        {
        }

        public LowerTaxaParser(IBase baseObject)
            : base(baseObject)
        {
        }

        public static string ParseRank(string infraSpecificRank)
        {
            string rank = string.Empty;
            switch (infraSpecificRank)
            {
                case "subgen":
                case "Subgen":
                case "subgenus":
                case "Subgenus":
                case "subg":
                case "Subg":
                    rank = "subgenus";
                    break;
                case "sect":
                case "Sect":
                case "section":
                case "Section":
                    rank = "section";
                    break;
                case "Ser":
                case "ser":
                    rank = "series";
                    break;
                case "Trib":
                case "trib":
                    rank = "tribe";
                    break;
                case "subsect":
                case "Subsect":
                case "subsection":
                case "Subsection":
                    rank = "subsection";
                    break;
                case "subvar":
                case "Subvar":
                    rank = "subvariety";
                    break;
                case "var":
                case "Var":
                    rank = "variety";
                    break;
                case "subsp":
                case "Subsp":
                case "ssp":
                case "Ssp":
                case "subspec":
                case "Subspec":
                case "Subspecies":
                case "subspecies":
                    rank = "subspecies";
                    break;
                case "st":
                case "St":
                    rank = "stage";
                    break;
                case "r":
                    rank = "rank";
                    break;
                case "f":
                case "fo":
                case "form":
                case "Form":
                case "forma":
                case "Forma":
                    rank = "form";
                    break;
                case "sf":
                case "Sf":
                    rank = "subform";
                    break;
                case "a":
                case "A":
                case "ab":
                case "Ab":
                    rank = "aberration";
                    break;
                case "aff":
                case "Aff":
                case "prope":
                case "Prope":
                case "cf":
                case "Cf":
                case "sp":
                case "Sp":
                case "nr":
                case "Nr":
                case "Near":
                case "near":
                case "sp. near":
                case "×":
                case "?":
                    rank = "species";
                    break;
                default:
                    rank = "species";
                    break;
            }

            return rank;
        }

        public static string ParseLower(string str)
        {
            string replace = str;

            // Parse different parts of the taxonomic name
            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubvar)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subvariety\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Vv]ar|v)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"variety\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ff]orma?|f)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"form\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b(sf|[Ss]ubforma?)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subform\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Aa]b)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"aberration\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubsp|[Ss]sp)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subspecies\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Aa]ff|[Cc]f|[Nn]r|[Ss]p|[Ss]p\.?\s*ne?a?r|[Ss]p\s+ne?a?r)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"uncertainty-rank\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubgen|[Ss]ubgenus|[Ss]g)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subgenus\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ect|[Ss]ection)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"section\">$3</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\b([Ss]ubsect|[Ss]ubsection)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subsection\">$3</tn-part>");

            // Parse taxa from beginnig

            // Genus species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>$4<tn-part type=\"subspecies\">$5</tn-part>");

            // Genus species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");
            replace = Regex.Replace(
                replace,
                @"\A‘([A-Z][a-z\.]+)’([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "‘<tn-part type=\"genus\">$1</tn-part>’$2<tn-part type=\"species\">$3</tn-part>");

            // Genus (Subgenus) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (superspecies) species subspecies
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêî\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");

            // Genus (Subgenus) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (superspecies) species
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([a-z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]*)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");

            // Genus (Subgenus)
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]*)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)",
                "<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)");

            // Genus
            replace = Regex.Replace(
                replace,
                @"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)",
                "<tn-part type=\"genus\">$1</tn-part>");

            // species
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)",
                @"<tn-part type=""species"">$1</tn-part>");

            // Try to parse whole string
            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]sp|[Ss]ubsp)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subspecies\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Vv]ar|[Vv])(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"variety\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Aa]b)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"aberration\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ff]|[Ff]orma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"form\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]f|[Ss]ubforma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subform\">$4</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-zçäöüëïâěôûêî-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([a-zçäöüëïâěôûêîæœ\.-]+)\Z",
                "<tn-part type=\"species\">$1</tn-part>");

            replace = Regex.Replace(
                replace,
                @"\A([A-Z\.-]+)\Z",
                "<tn-part type=\"genus\">$1</tn-part>");

            // Parse question mark
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s*\?", "<tn-part type=\"uncertainty-rank\">?</tn-part>");

            // Parse hybrid sign
            replace = Regex.Replace(replace, @"(?<=</tn-part>\s*)×(?=\s*<tn-part)", "<tn-part type=\"hybrid-sign\">×</tn-part>");

            // Here we must return the dot after tn-part[@type='infraspecific-rank'] back into the tag
            replace = Regex.Replace(replace, @"</tn-part>\.", ".</tn-part>");

            // Clear multiple white spaces between ‘tn-part’-s
            replace = Regex.Replace(replace, @"(?<=</tn-part>)\s{2,}(?=<tn-part)", " ");

            return replace;
        }

        public void Parse()
        {
            try
            {
                foreach (XmlNode lowerTaxon in this.XmlDocument.SelectNodes("//tn[@type='lower'][not(*)]", this.NamespaceManager))
                {
                    lowerTaxon.InnerXml = ParseLower(lowerTaxon.InnerXml);
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Parse lower taxa without basionym.");
            }

            try
            {
                foreach (XmlNode lowerTaxon in this.XmlDocument.SelectNodes("//tn[@type='lower'][count(*) != count(tn-part)]", this.NamespaceManager))
                {
                    string replace = Regex.Replace(lowerTaxon.InnerXml, "</?i>", string.Empty);
                    string parseBasionym = Regex.Replace(replace, "^.*?<basionym>(.*?)</basionym>.*$", "$1");
                    parseBasionym = ParseLower(parseBasionym);
                    string parseSpecific = Regex.Replace(replace, "^.*?<specific>(.*?)</specific>.*$", "$1");
                    parseSpecific = ParseLower(parseSpecific);

                    replace = Regex.Replace(replace, "<genus>(.+?)</genus>", "<tn-part type=\"genus\">$1</tn-part>");
                    replace = Regex.Replace(replace, "<genus-authority>(.*?)</genus-authority>", "<tn-part type=\"authority\">$1</tn-part>");
                    replace = Regex.Replace(replace, "<basionym>(.*?)</basionym>", parseBasionym);
                    replace = Regex.Replace(replace, "<specific>(.*?)</specific>", parseSpecific);
                    replace = Regex.Replace(replace, @"<basionym-authority>(\s*)(\(.*?\))(\s*)(.*?)</basionym-authority>", "$1<tn-part type=\"basionym-authority\">$2</tn-part>$3<tn-part type=\"authority\">$4</tn-part>");
                    replace = Regex.Replace(replace, "<basionym-authority>(.*?)</basionym-authority>", "<tn-part type=\"authority\">$1</tn-part>");

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>\s*\)?\s*<species>[^<>]*</species>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;
                        string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", string.Empty);
                        string rank = ParseRank(infraSpecificRank);
                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-z\s\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");
                        rank = replace1.Contains("type=\"subspecies\"") ? "subspecies" : "species";
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
                    {
                        string replace1 = m.Value;
                        string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", string.Empty);
                        string rank = ParseRank(infraSpecificRank);
                        replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
                        ////replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"" + rank + "-authority\">$1</tn-part>");
                        replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
                        replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
                    }

                    replace = Regex.Replace(replace, @"<sensu>(.*?)</sensu>", @"<tn-part type=""sensu"">$1</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">×</tn-part>", @"<tn-part type=""hybrid-sign"">×</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">\?</tn-part>", @"<tn-part type=""uncertainty-rank"">?</tn-part>");
                    replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">((?i)(aff|prope|cf|nr|near|sp\. near)\.?)</tn-part>", @"<tn-part type=""uncertainty-rank"">$1</tn-part>");

                    lowerTaxon.InnerXml = replace;
                }

                this.AddFullNameAttribute();

                this.AddMissingEmptyTagsInTaxonName();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Parse lower taxa with basionym.");
            }

            // Remove wrapping i around tn[tn-part[@type='subgenus']]
            this.XmlDocument.InnerXml = Regex.Replace(
                this.XmlDocument.InnerXml,
                @"<i>(<tn(\s*>|\s[^<>]*>)<tn-part type=""genus""[^<>]*>[^<>]*</tn-part>\s*\(<tn-part type=""(subgenus|superspecies)""[^<>]*>.*?</tn>)</i>",
                "$1");
        }

        private void AddMissingEmptyTagsInTaxonName()
        {
            // Add missing tags in lower-taxa
            foreach (XmlNode lowerTaxon in this.XmlDocument.SelectNodes("//tn[@type='lower'][not(count(tn-part)=1 and tn-part/@type='subgenus')][count(tn-part[@type='genus'])=0 or (count(tn-part[@type='species'])=0 and count(tn-part[@type!='genus'][@type!='subgenus'][@type!='section'][@type!='subsection'])!=0)]", this.NamespaceManager))
            {
                XmlNode genus = lowerTaxon.SelectSingleNode(".//tn-part[@type='genus']", this.NamespaceManager);
                if (genus == null)
                {
                    XmlNode species = lowerTaxon.SelectSingleNode(".//tn-part[@type='species']", this.NamespaceManager);
                    if (species == null)
                    {
                        XmlElement speciesElement = this.XmlDocument.CreateElement("tn-part");

                        XmlAttribute elementType = this.XmlDocument.CreateAttribute("type");
                        elementType.InnerText = "species";
                        speciesElement.Attributes.Append(elementType);

                        XmlAttribute fullName = this.XmlDocument.CreateAttribute("full-name");
                        speciesElement.Attributes.Append(fullName);

                        lowerTaxon.PrependChild(speciesElement);
                    }

                    // Add genus tag
                    {
                        XmlElement genusElement = this.XmlDocument.CreateElement("tn-part");

                        XmlAttribute elementType = this.XmlDocument.CreateAttribute("type");
                        elementType.InnerText = "genus";
                        genusElement.Attributes.Append(elementType);

                        XmlAttribute fullName = this.XmlDocument.CreateAttribute("full-name");
                        genusElement.Attributes.Append(fullName);

                        lowerTaxon.PrependChild(genusElement);
                    }
                }
            }
        }

        private void AddFullNameAttribute()
        {
            // Add @full-name
            foreach (XmlNode lowerTaxon in this.XmlDocument.SelectNodes("//tn[@type='lower']/tn-part[not(@full-name)][@type!='sensu' and @type!='hybrid-sign' and @type!='uncertainty-rank' and @type!='infraspecific-rank' and @type!='authority' and @type!='basionym-authority'][contains(string(.), '.')]", this.NamespaceManager))
            {
                XmlAttribute fullName = this.XmlDocument.CreateAttribute("full-name");
                lowerTaxon.Attributes.Append(fullName);
            }
        }
    }
}

namespace ProcessingTools.BaseLibrary.Taxonomy.NlmSystem
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Globals.Loggers;

    public class Expander : TaggerBase
    {
        public const string GenusLeftTag = "<tn-part type=\"genus\">";
        public const string GenusRightTag = "</tn-part>";
        public const string SpeciesLeftTag = "<tn-part type=\"species\">";
        public const string SpeciesRightTag = "</tn-part>";
        public const string SubgenusLeftTag = "<tn-part type=\"subgenus\">";
        public const string SubgenusRgithTag = "</tn-part>";
        public const string SubspeciesLeftTag = "<tn-part type=\"subspecies\">";
        public const string SubspeciesRightTag = "</tn-part>";

        private Regex findLowerTaxa = new Regex(@"<i><tn[^>\-]*>([\s\S]*?)</tn></i>");
        private Regex findLowerTaxaMultiLine = new Regex(@"<i><tn[^>\-]*>([\s\S]*?)</tn></i>");

        private ILogger logger;

        public Expander(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public Expander(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public Expander(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void StableExpand()
        {
            // In this method it is supposed that the subspecies name is not shortened
            Taxonomy.PrintMethodMessage("StableExpand", this.logger);

            XmlNodeList shortTaxaList = this.XmlDocument.SelectNodes("//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']]][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']", NamespaceManager);
            XmlNodeList nonShortTaxaList = this.XmlDocument.SelectNodes("//tn[@type='lower'][not(tn-part[@full-name])][tn-part[@type='genus']]", NamespaceManager);

            List<string> shortTaxaListUnique = shortTaxaList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
            List<string> nonShortTaxaListUnique = nonShortTaxaList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();

            List<Species> speciesList = new List<Species>();
            foreach (string taxon in nonShortTaxaListUnique)
            {
                speciesList.Add(new Species(taxon));
            }

            foreach (string shortTaxon in shortTaxaListUnique)
            {
                string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                string replace = text;

                Species sp = new Species(shortTaxon);
                Taxonomy.PrintNextShortened(sp, this.logger);

                foreach (Species sp1 in speciesList)
                {
                    if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0)
                    {
                        Match matchGenus = Regex.Match(sp1.GenusName, sp.GenusPattern);
                        Match matchSubgenus = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                        Match matchSpecies = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                        // Check if the subgenus is empty
                        if (string.Compare(sp.SubgenusName, string.Empty) == 0)
                        {
                            if (matchGenus.Success && matchSpecies.Success)
                            {
                                if (string.Compare(sp1.SubgenusName, string.Empty) == 0)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                    replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                    replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                }
                                else
                                {
                                    Taxonomy.PrintSubstitutionMessageFail(sp, sp1, this.logger);
                                }
                            }
                        }
                        else
                        {
                            if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                            }
                        }
                    }
                }

                this.Xml = Regex.Replace(this.Xml, Regex.Escape(text), replace);
            }
        }

        public void StableExpand1()
        {
            // In this method it is supposed that the subspecies name is not shortened
            Taxonomy.PrintMethodMessage("StableExpand", this.logger);

            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                string replace = m.Value;
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && string.Compare(sp.SpeciesName, string.Empty) != 0)
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    for (Match taxon = this.findLowerTaxaMultiLine.Match(this.Xml); taxon.Success; taxon = taxon.NextMatch())
                    {
                        string replace1 = m.Value;

                        Species sp1 = new Species(taxon.Value);

                        // We are interested only on non-shortened lower taxa
                        if ((taxon.Value.IndexOf('.') < 0) && (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0))
                        {
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                            // Check if the subgenus is empty
                            if (string.Compare(sp.SubgenusName, string.Empty) == 0)
                            {
                                if (mgen.Success && msp.Success)
                                {
                                    if (string.Compare(sp1.SubgenusName, string.Empty) == 0)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn genus=\"" + sp1.GenusName + "\">");
                                    }
                                    else
                                    {
                                        this.logger?.Log("\tThere is a genus-species coincidence but the subgenus does not match:");
                                        this.logger?.Log("\t\t{0}\t|\t{1}", sp.SpeciesNameAsString, sp1.SpeciesNameAsString);
                                        this.logger?.Log("\t\tSubstitution will not be done!");
                                    }
                                }
                            }
                            else
                            {
                                if (mgen.Success && msgen.Success && msp.Success)
                                {
                                    Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                    replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn genus=\"" + sp1.GenusName + "\" subgenus=\"" + sp1.SubgenusName + "\">");
                                }
                            }
                        }
                    }

                    this.Xml = Regex.Replace(this.Xml, Regex.Escape(m.Value), replace);
                }
            }
        }

        public void UnstableExpand(int stage)
        {
            Taxonomy.PrintMethodMessage("UnstableExpand" + stage, this.logger);

            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                string xgenus = (sp.GenusName.IndexOf('.') > -1) ? sp.GenusName.Substring(0, sp.GenusName.Length - 1) + "[a-z]+?" : "SKIP";
                string xsubgenus = (sp.SubgenusName.IndexOf('.') > -1) ? sp.SubgenusName.Substring(0, sp.SubgenusName.Length - 1) + "[a-z]+?" : "SKIP";
                string xspecies = (sp.SpeciesName.IndexOf('.') > -1) ? sp.SpeciesName.Substring(0, sp.SpeciesName.Length - 1) + "[a-z]+?" : "SKIP";

                // Select only shortened taxa with non-zero species name
                if (sp.IsShortened && !sp.IsSpeciesNull)
                {
                    this.logger?.Log("\nNext shortened taxon:\t{0}", sp.AsString());

                    // Scan all lower-taxon names in the article
                    // TODO
                    Match div = Regex.Match(this.Xml, "<p>.*?" + Regex.Escape(m.Value));
                    if (div.Success)
                    {
                        // TODO
                        this.logger?.Log("Paragraph content:\n\t{0}\n", div.Value);
                        bool matchFound = false;
                        Species spl = new Species();
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(div.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            Match mgen = Regex.Match(sp1.GenusName, xgenus);
                            if (mgen.Success)
                            {
                                spl.SetGenus(sp1);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, xsubgenus);
                            if (msgen.Success)
                            {
                                spl.SetSubgenus(sp1);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, xspecies);
                            if (msp.Success)
                            {
                                spl.SetSpecies(sp1);
                                matchFound = true;
                            }

                            this.logger?.Log("........ Found: genus {0} | subgenus {1} | species {2}", sp1.GenusName, sp1.SubgenusName, sp1.SpeciesName);
                        }

                        if (matchFound)
                        {
                            this.logger?.Log("________ Substitution '{0}, ({1}), {2}'  by '{3}, ({4}), {5}'.", sp.GenusName, sp.SubgenusName, sp.SpeciesName, spl.GenusName, spl.SubgenusName, spl.SpeciesName);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (!spl.IsGenusNull)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(GenusLeftTag + sp.GenusName + GenusRightTag), GenusLeftTag + spl.GenusName + GenusRightTag);
                            }

                            if (!spl.IsSubgenusNull)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(SubgenusLeftTag + sp.SubgenusName + SubgenusRgithTag), SubgenusLeftTag + spl.SubgenusName + SubgenusRgithTag);
                            }

                            if (!spl.IsSpeciesNull)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(SpeciesLeftTag + sp.SpeciesName + SpeciesRightTag), SpeciesLeftTag + spl.SpeciesName + SpeciesRightTag);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(div.Value), Regex.Replace(div.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("________ No suitable genus name has been found in the current division.");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in such a division or is already unfolded");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableExpand1()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1", this.logger);
            //// On the first stage try to expand all quasi-stable cases like
            //// [Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species

            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                string replace = m.Value;
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && string.Compare(sp.SpeciesName, string.Empty) != 0)
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    bool found = false;
                    string replace1 = m.Value;
                    for (Match taxon = this.findLowerTaxaMultiLine.Match(this.Xml); taxon.Success; taxon = taxon.NextMatch())
                    {
                        Species sp1 = new Species(taxon.Value);
                        if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName) == 0)
                        {
                            // We are interested only in coincident subspecies names
                            if (string.Compare(sp.SpeciesName, sp1.SpeciesName) == 0)
                            {
                                // First process all taxa with coincident species names
                                if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0)
                                {
                                    // ... and coincident subgenus names, i.e. try to find suitable genus names
                                    Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                                    if (mgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                        found = true;
                                    }
                                }
                                else if (string.Compare(sp.GenusName, sp1.GenusName) == 0)
                                {
                                    // ... or coincident genus names, i.e. try to find suitable subgenus names
                                    Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern);
                                    if (msgen.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                        found = true;
                                    }
                                }
                            }

                            if (string.Compare(sp.GenusName, sp1.GenusName) == 0)
                            {
                                // First process all taxa with coincident genus names
                                if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0)
                                {
                                    // ... and coincident subgenus names, i.e. try to find suitable genus names
                                    Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                                    if (msp.Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        replace1 = Regex.Replace(replace1, Regex.Escape(sp.SpeciesTagged), sp1.SpeciesTagged);
                                        found = true;
                                    }
                                }
                            }
                        }
                    }

                    if (found)
                    {
                        replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                    }

                    this.Xml = Regex.Replace(this.Xml, Regex.Escape(m.Value), replace);
                }
            }
        }

        public void UnstableExpand2()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2", this.logger);

            // On the second stage try to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                string replace = m.Value;
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species and subgenus name
                if ((sp.GenusName.IndexOf('.') > -1) && (sp.SubgenusName.IndexOf('.') < 0) && (string.Compare(sp.SubgenusName, string.Empty) != 0) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    bool found = false;
                    string replace1 = m.Value;
                    for (Match taxon = this.findLowerTaxaMultiLine.Match(this.Xml); taxon.Success; taxon = taxon.NextMatch())
                    {
                        Species sp1 = new Species(taxon.Value);
                        if (string.Compare(sp.GenusName, sp1.GenusName) == 0 && string.Compare(sp.SubgenusName, sp1.SubgenusName) != 0)
                        {
                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                            if (msgen.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                found = true;
                            }
                        }

                        if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                        {
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                            if (mgen.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                found = true;
                            }
                        }
                    }

                    if (found)
                    {
                        replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                    }

                    this.Xml = Regex.Replace(this.Xml, Regex.Escape(m.Value), replace);
                }

                // Select only shortened taxa with non-zero species and subgenus name
                if ((sp.GenusName.IndexOf('.') < 0) && (sp.SubgenusName.IndexOf('.') > -1) && (string.Compare(sp.SubgenusName, string.Empty) != 0) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    bool found = false;
                    string replace1 = m.Value;
                    for (Match taxon = this.findLowerTaxaMultiLine.Match(this.Xml); taxon.Success; taxon = taxon.NextMatch())
                    {
                        Species sp1 = new Species(taxon.Value);
                        if (string.Compare(sp.GenusName, sp1.GenusName) == 0 && string.Compare(sp.SubgenusName, sp1.SubgenusName) != 0)
                        {
                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                            if (msgen.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                found = true;
                            }
                        }

                        if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                        {
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                            if (mgen.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                found = true;
                            }
                        }
                    }

                    if (found)
                    {
                        replace = Regex.Replace(replace1, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                    }

                    this.Xml = Regex.Replace(this.Xml, Regex.Escape(m.Value), replace);
                }
            }
        }

        public void UnstableExpand3()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs", this.logger);
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    Match paragraph = Regex.Match(this.Xml, "<p>.*?" + Regex.Escape(m.Value));
                    if (paragraph.Success)
                    {
                        this.logger?.Log("Paragraph content:\n\t{0}\n", paragraph.Value.RemoveTaxonNamePartTags());
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);

                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                            if (mgen.Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern);
                            if (msgen.Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                            if (msp.Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("paragraph", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (string.Compare(last.GenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (string.Compare(last.SubgenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (string.Compare(last.SpeciesName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the current paragraph.\n");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in a paragraph or is already expanded");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableExpand4()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 4: look in treatment sections", this.logger);
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    Match paragraph = Regex.Match(this.Xml, "<tp:treatment-sec[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (paragraph.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                            if (mgen.Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                            if (msgen.Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                            if (msp.Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("treatment section", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (string.Compare(last.GenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (string.Compare(last.SubgenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (string.Compare(last.SpeciesName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the current treatment section.\n");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in a treatment section or is already unfolded");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableExpand5()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 5: look in treatments", this.logger);
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    Match paragraph = Regex.Match(this.Xml, "<tp:taxon-treatment>[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (paragraph.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                            if (mgen.Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                            if (msgen.Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                            if (msp.Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("treatment", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (string.Compare(last.GenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (string.Compare(last.SubgenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (string.Compare(last.SpeciesName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the current treatment.\n");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in a treatment or is already unfolded");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableExpand6()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 6: look in sections", this.logger);
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    Match paragraph = Regex.Match(this.Xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (paragraph.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                            if (mgen.Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                            if (msgen.Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                            if (msp.Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("section", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (string.Compare(last.GenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (string.Compare(last.SubgenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (string.Compare(last.SpeciesName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the current section.\n");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in a section or is already unfolded");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableExpand7()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 7: WARNING: search from the beginnig", this.logger);
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    Match paragraph = Regex.Match(this.Xml, "<?xml[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (paragraph.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                            if (mgen.Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                            if (msgen.Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                            if (msp.Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("preceding text", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (string.Compare(last.GenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (string.Compare(last.SubgenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (string.Compare(last.SpeciesName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the preceding text.\n");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in the preceding text");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableExpand8()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 8: WARNING: search in the whole article", this.logger);
            for (Match m = this.findLowerTaxa.Match(this.Xml); m.Success; m = m.NextMatch())
            {
                Species sp = new Species(m.Value);
                if (Taxonomy.EmptyGenus(m.Value, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if ((m.Value.IndexOf('.') > -1) && (string.Compare(sp.SpeciesName, string.Empty) != 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // Scan all lower-taxon names in the article
                    Match paragraph = Regex.Match(this.Xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (paragraph.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(paragraph.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusSkipPattern);
                            if (mgen.Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            Match msgen = Regex.Match(sp1.SubgenusName, sp.SubspeciesSkipPattern);
                            if (msgen.Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            Match msp = Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern);
                            if (msp.Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("article", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = Regex.Replace(m.Value, @"<tn[^>\-]*>", "<tn unfold=\"true\">");
                            if (string.Compare(last.GenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (string.Compare(last.SubgenusName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (string.Compare(last.SpeciesName, string.Empty) != 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), Regex.Replace(paragraph.Value, Regex.Escape(m.Value), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the article.\n");
                        }
                    }
                    else
                    {
                        this.logger?.Log("This species is not in the article or is already unfolded");
                    }

                    this.logger?.Log("\n");
                }
            }
        }

        public void UnstableUnfold()
        {
            string genusPrefix = "<tn-part type=\"genus\">";
            string genusSuffix = "</tn-part>";
            string genusNPrefix = "(?<=" + genusPrefix + ")";
            string genusNSuffix = "(?=" + genusSuffix + ")";
            string genusSpeciesPattern = "<tn[^>]*?><tn-part type=\"genus\">[A-Z][a-z]*\\.</tn-part>\\s*<tn-part type=\"species\">.*?</tn-part></tn>";

            // Print all recognized genera in the article
            this.logger?.Log();
            Match genus = Regex.Match(this.Xml, genusNPrefix + "[A-Z][a-z\\.]+?" + genusNSuffix);
            while (genus.Success)
            {
                this.logger?.Log("Found genus: {0}", genus.Value);
                genus = genus.NextMatch();
            }

            this.logger?.Log("\n\n\n\n\n");

            // Show only genera in the current paragraph
            Match genSp = Regex.Match(this.Xml, genusSpeciesPattern);
            while (genSp.Success)
            {
                Match genusShort = Regex.Match(genSp.Value, genusNPrefix + "[A-Z][a-z]*?(?=\\.)");
                Match species = Regex.Match(genSp.Value, "(?<=<tn-part type=\"species\">).*?(?=<)");
                this.logger?.Log("Shortened species found:\t{0}. {1}\n", genusShort.Value, species.Value);
                bool matchFound = false;
                this.logger?.Log("Scanning containing paragraph to find suitable genus...");
                Match paragraph = Regex.Match(this.Xml, "<p>.*?" + Regex.Escape(genSp.Value));
                if (paragraph.Success)
                {
                    this.logger?.Log("Paragraph content:\n\t{0}\n", paragraph.Value);
                    string lastGenusFound = string.Empty;
                    matchFound = false;
                    Match genusPar = Regex.Match(paragraph.Value, genusNPrefix + Regex.Escape(genusShort.Value) + "[a-z]+?" + genusNSuffix);
                    while (genusPar.Success)
                    {
                        matchFound = true;
                        lastGenusFound = genusPar.Value;
                        this.logger?.Log("........ Found Genus in paragraph: {0}\n", lastGenusFound);
                        genusPar = genusPar.NextMatch();
                    }

                    if (matchFound)
                    {
                        this.logger?.Log("\n\tSpecies name '{0}. {1}' will be replaced by '{2} {1}' in the current paragraph.\n", genusShort.Value, species.Value, lastGenusFound);
                        string replace = Regex.Replace(paragraph.Value, ">" + genusPrefix + genusShort.Value + "\\." + genusSuffix, " unfold=\"true\"><tn-part type=\"genus\">" + lastGenusFound + "</tn-part>");
                        this.Xml = Regex.Replace(this.Xml, Regex.Escape(paragraph.Value), replace);
                    }
                    else
                    {
                        this.logger?.Log("\n\tNo suitable genus name has been found in the current paragraph.\n");
                    }
                }
                else
                {
                    this.logger?.Log("This species is not in a paragraph or is already unfolded");
                }

                this.logger?.Log("\n");
                genSp = genSp.NextMatch();
            }
        }
    }
}
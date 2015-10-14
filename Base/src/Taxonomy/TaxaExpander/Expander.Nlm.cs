namespace ProcessingTools.BaseLibrary.Taxonomy.Nlm
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class Expander : TaggerBase
    {
        private const string GenusLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"genus\">";
        private const string GenusRightTag = "</tp:taxon-name-part>";
        private const string SpeciesLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"species\">";
        private const string SpeciesRightTag = "</tp:taxon-name-part>";
        private const string SubgenusLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"subgenus\">";
        private const string SubgenusRightTag = "</tp:taxon-name-part>";
        private const string SubspeciesLeftTag = "<tp:taxon-name-part taxon-name-part-type=\"subspecies\">";
        private const string SubspeciesRightTag = "</tp:taxon-name-part>";

        private Regex findLowerTaxa = new Regex(@"<italic><tp:taxon-name[^>\-]*>(.*?)</tp:taxon-name></italic>");
        private Regex findLowerTaxaMultiLine = new Regex(@"<italic><tp:taxon-name[^>\-]*>([\s\S]*?)</tp:taxon-name></italic>");

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

        public void _UnstableExpand1()
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
                        replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                    }

                    this.Xml = Regex.Replace(this.Xml, Regex.Escape(m.Value), replace);
                }
            }
        }

        public void _UnstableExpand2()
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
                                Taxonomy.PrintSubstitutionMessage1(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                found = true;
                            }
                        }

                        if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                        {
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                            if (mgen.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage1(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                found = true;
                            }
                        }
                    }

                    if (found)
                    {
                        replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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
                                Taxonomy.PrintSubstitutionMessage1(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.SubgenusTagged), sp1.SubgenusTagged);
                                found = true;
                            }
                        }

                        if (string.Compare(sp.SubgenusName, sp1.SubgenusName) == 0 && string.Compare(sp.GenusName, sp1.GenusName) != 0)
                        {
                            Match mgen = Regex.Match(sp1.GenusName, sp.GenusPattern);
                            if (mgen.Success)
                            {
                                Taxonomy.PrintSubstitutionMessage1(sp, sp1, this.logger);
                                replace1 = Regex.Replace(replace1, Regex.Escape(sp.GenusTagged), sp1.GenusTagged);
                                found = true;
                            }
                        }
                    }

                    if (found)
                    {
                        replace = Regex.Replace(replace1, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
                    }

                    this.Xml = Regex.Replace(this.Xml, Regex.Escape(m.Value), replace);
                }
            }
        }

        public void _UnstableExpand3()
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
                    Match p = Regex.Match(this.Xml, "<p>.*?" + Regex.Escape(m.Value));
                    if (p.Success)
                    {
                        this.logger?.Log("Paragraph content:\n\t{0}\n", p.Value.RemoveTaxonNamePartTags());
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
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
                            string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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

        public void UnstableExpand1()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 1\nTrying to expand all quasi-stable cases like\n[Genus] ([Subgenus].) species ~~ [Genus]. ([Subenus]) species ~~ [Genus]. ([Subgenus].) species", this.logger);

            XmlNodeList lowerTaxa = this.XmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", NamespaceManager);
            List<Species> speciesList = new List<Species>();
            foreach (XmlNode node in lowerTaxa)
            {
                speciesList.Add(new Species(node.InnerXml));
            }

            for (int i = 0; i < lowerTaxa.Count; i++)
            {
                XmlNode node = lowerTaxa[i];
                Species sp = speciesList[i];
                if (Taxonomy.EmptyGenus(node.InnerXml, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if (sp.IsShortened && sp.SpeciesName.Length > 0)
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);
                    foreach (Species sp1 in speciesList)
                    {
                        SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                        if (compare.EqualSubspecies)
                        {
                            // We are interested only in coincident subspecies names
                            if (compare.EqaulSpecies)
                            {
                                // First process all taxa with coincident species names
                                if (compare.EqualSubgenera)
                                {
                                    // ... and coincident subgenus names, i.e. try to find suitable genus names
                                    if (Regex.Match(sp1.GenusName, sp.GenusSkipPattern).Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                    }
                                }
                                else if (compare.EqualGenera)
                                {
                                    // ... or coincident genus names, i.e. try to find suitable subgenus names
                                    if (Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern).Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                    }
                                }
                            }

                            if (compare.EqualGenera)
                            {
                                // First process all taxa with coincident genus names
                                if (compare.EqualSubgenera)
                                {
                                    // ... and coincident subgenus names, i.e. try to find suitable genus names
                                    if (Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern).Success)
                                    {
                                        Taxonomy.PrintSubstitutionMessage(sp, sp1, this.logger);
                                        node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UnstableExpand2()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 2\nTrying to expand all genus-subgenus abbreviations [Genus]. ([Subgenus].)", this.logger);

            XmlNodeList lowerTaxa = this.XmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", NamespaceManager);
            List<Species> speciesList = new List<Species>();
            foreach (XmlNode node in lowerTaxa)
            {
                speciesList.Add(new Species(node.InnerXml));
            }

            for (int i = 0; i < lowerTaxa.Count; i++)
            {
                XmlNode node = lowerTaxa[i];
                Species sp = speciesList[i];
                if (Taxonomy.EmptyGenus(node.InnerXml, sp, this.logger))
                {
                    return;
                }

                /*
                 * Select only shortened taxa with non-zero species and subgenus name
                 */
                if ((sp.GenusName.IndexOf('.') > -1) && ((sp.SubgenusName.IndexOf('.') < 0) && (sp.SubgenusName.Length > 0)) && (sp.SpeciesName.Length > 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);
                    foreach (Species sp1 in speciesList)
                    {
                        SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                        if (compare.EqualSubgenera && !compare.EqualGenera)
                        {
                            if (Regex.Match(sp1.GenusName, sp.GenusPattern).Success)
                            {
                                Taxonomy.PrintSubstitutionMessage1(sp, sp1, this.logger);
                                node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                            }
                        }
                    }
                }

                /*
                 * Select only shortened taxa with non-zero species and genus name
                 */
                if ((sp.SubgenusName.IndexOf('.') > -1) && ((sp.GenusName.IndexOf('.') < 0) && (sp.GenusName.Length > 0)) && (sp.SpeciesName.Length > 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);
                    foreach (Species sp1 in speciesList)
                    {
                        SpeciesComparison compare = new SpeciesComparison(sp, sp1);
                        if (compare.EqualGenera && !compare.EqualSubgenera)
                        {
                            if (Regex.Match(sp1.SubgenusName, sp.SubgenusPattern).Success)
                            {
                                Taxonomy.PrintSubstitutionMessage1(sp, sp1, this.logger);
                                node.InnerXml = Regex.Replace(node.InnerXml, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                            }
                        }
                    }
                }
            }
        }

        public void UnstableExpand3()
        {
            Taxonomy.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs", this.logger);

            XmlNodeList lowerTaxa = this.XmlDocument.SelectNodes("//tp:taxon-name[@type='lower']", NamespaceManager);
            foreach (XmlNode node in lowerTaxa)
            {
                Species sp = new Species(node.InnerXml);
                if (Taxonomy.EmptyGenus(node.InnerXml, sp, this.logger))
                {
                    return;
                }

                // Select only shortened taxa with non-zero species name
                if (sp.IsShortened && (sp.SpeciesName.Length > 0))
                {
                    Taxonomy.PrintNextShortened(sp, this.logger);

                    // TODO
                    for (Match p = Regex.Match(this.Xml, "<p>[\\s\\S]+?" + Regex.Escape(node.InnerXml)); p.Success; p = p.NextMatch())
                    {
                        this.logger?.Log("Paragraph content:\n\t{0}\n", p.Value.RemoveTaxonNamePartTags(), this.logger);

                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
                        {
                            Species sp1 = new Species(taxon.Value);
                            if (Regex.Match(sp1.GenusName, sp.GenusSkipPattern).Success)
                            {
                                last.SetGenus(sp1.GenusName);
                                matchFound = true;
                            }

                            if (Regex.Match(sp1.SubgenusName, sp.SubgenusSkipPattern).Success)
                            {
                                last.SetSubgenus(sp1.SubgenusName);
                                matchFound = true;
                            }

                            if (Regex.Match(sp1.SpeciesName, sp.SpeciesSkipPattern).Success)
                            {
                                last.SetSpecies(sp1.SpeciesName);
                                matchFound = true;
                            }

                            Taxonomy.PrintFoundMessage("paragraph", sp1, this.logger);
                        }

                        if (matchFound)
                        {
                            Taxonomy.PrintSubstitutionMessage(sp, last, this.logger);
                            string replace = node.InnerXml;
                            if (last.GenusName.Length > 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.GenusTagged), last.GenusTagged);
                            }

                            if (last.SubgenusName.Length > 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SubgenusTagged), last.SubgenusTagged);
                            }

                            if (last.SpeciesName.Length > 0)
                            {
                                replace = Regex.Replace(replace, Regex.Escape(sp.SpeciesTagged), last.SpeciesTagged);
                            }

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(node.InnerXml), replace));
                        }
                        else
                        {
                            this.logger?.Log("\n\tNo suitable genus name has been found in the current paragraph.\n");
                        }
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
                    Match p = Regex.Match(this.Xml, "<tp:treatment-sec[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (p.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
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
                            string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    Match p = Regex.Match(this.Xml, "<tp:taxon-treatment>[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (p.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
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
                            string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    Match p = Regex.Match(this.Xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (p.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
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
                            string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    Match p = Regex.Match(this.Xml, "<?xml[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (p.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
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
                            string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
                    Match p = Regex.Match(this.Xml, "<sec[\\s\\S]*?" + Regex.Escape(m.Value));
                    if (p.Success)
                    {
                        Species last = new Species();
                        bool matchFound = false;
                        for (Match taxon = this.findLowerTaxaMultiLine.Match(p.Value); taxon.Success; taxon = taxon.NextMatch())
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
                            string replace = Regex.Replace(m.Value, @"<tp:taxon-name[^>\-]*>", "<tp:taxon-name unfold=\"true\">");
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

                            this.Xml = Regex.Replace(this.Xml, Regex.Escape(p.Value), Regex.Replace(p.Value, Regex.Escape(m.Value), replace));
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
    }
}
namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class Expander : ConfigurableDocument
    {
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

        public static void PrintMethodMessage(string name, ILogger logger)
        {
            logger?.Log("\n\n#\n##\n### {0} will be executed...\n##\n#\n", name);
        }

        public static void PrintNextShortened(Species sp, ILogger logger)
        {
            logger?.Log("\nNext shortened taxon:\t{0}", sp.SpeciesNameAsString);
        }

        public static void PrintSubstitutionMessage(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tSubstitution:\t{0}\t-->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameAsString);
        }

        public static void PrintSubstitutionMessageFail(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tFailed Subst:\t{0}\t<->\t{1}", original.SpeciesNameAsString, substitution.SpeciesNameAsString);
        }

        public void StableExpand()
        {
            // In this method it is supposed that the subspecies name is not shortened
            Expander.PrintMethodMessage("StableExpand", this.logger);

            IEnumerable<string> shortTaxaListUnique = this.XmlDocument.GetListOfShortenedTaxa();
            IEnumerable<string> nonShortTaxaListUnique = this.XmlDocument.GetListOfNonShortenedTaxa();

            List<Species> speciesList = new List<Species>();
            foreach (string taxon in nonShortTaxaListUnique)
            {
                speciesList.Add(new Species(taxon));
            }

            string xml = this.Xml;

            foreach (string shortTaxon in shortTaxaListUnique)
            {
                string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                string replace = text;

                Species sp = new Species(shortTaxon);
                Expander.PrintNextShortened(sp, this.logger);

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
                                    Expander.PrintSubstitutionMessage(sp, sp1, this.logger);
                                    replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                    replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                                }
                                else
                                {
                                    Expander.PrintSubstitutionMessageFail(sp, sp1, this.logger);
                                }
                            }
                        }
                        else
                        {
                            if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                            {
                                Expander.PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                                replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                                replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                            }
                        }
                    }
                }

                xml = Regex.Replace(xml, Regex.Escape(text), replace);
            }

            this.Xml = xml;
        }

        public void UnstableExpand3()
        {
            Expander.PrintMethodMessage("UnstableExpand. STAGE 3: Look in paragraphs", this.logger);

            // Loop over paragraphs containong shortened taxa
            foreach (XmlNode p in this.XmlDocument.SelectNodes("//p[count(.//tn-part[normalize-space(@full-name)='']) > 0]"))
            {
                this.logger?.Log(p.InnerText);
                this.logger?.Log("\n\n");

                IEnumerable<string> shortTaxaListUnique = p.GetListOfShortenedTaxa();
                IEnumerable<string> nonShortTaxaListUnique = p.GetListOfNonShortenedTaxa();

                List<Species> speciesList = new List<Species>();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    speciesList.Add(new Species(taxon));
                }

                foreach (string taxon in shortTaxaListUnique)
                {
                    this.logger?.Log(taxon);
                }

                this.logger?.Log();
                foreach (string taxon in nonShortTaxaListUnique)
                {
                    this.logger?.Log(taxon);
                }

                this.logger?.Log("\n\n");
            }
        }

        // TODO
        public void UnstableExpand8()
        {
            Expander.PrintMethodMessage("UnstableExpand. STAGE 8: WARNING: search in the whole article", this.logger);

            IEnumerable<string> shortTaxaListUnique = this.XmlDocument.GetListOfShortenedTaxa();
            IEnumerable<string> nonShortTaxaListUnique = this.XmlDocument.GetListOfNonShortenedTaxa();

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
                Expander.PrintNextShortened(sp, this.logger);

                foreach (Species sp1 in speciesList)
                {
                    Match matchGenus = Regex.Match(sp1.GenusName, sp.GenusPattern);
                    Match matchSubgenus = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                    Match matchSpecies = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                    if (matchGenus.Success || matchSubgenus.Success || matchSpecies.Success)
                    {
                        Expander.PrintSubstitutionMessage(sp, sp1, this.logger);
                        replace = Regex.Replace(replace, "(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName);
                        replace = Regex.Replace(replace, "(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName);
                        replace = Regex.Replace(replace, "(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                    }
                }

                this.Xml = Regex.Replace(this.Xml, Regex.Escape(text), replace);
            }
        }

        public void ForceExactSpeciesMatchExpand()
        {
            var nodeListOfSpeciesInShortenedTaxaName = this.XmlDocument.SelectNodes("//tn[@type='lower'][normalize-space(tn-part[@type='species'])!=''][normalize-space(tn-part[@type='genus'])=''][normalize-space(tn-part[@type='genus']/@full-name)='']/tn-part[@type='species']");

            var speciesUniq = nodeListOfSpeciesInShortenedTaxaName.Cast<XmlNode>()
                .Select(n => n.InnerText)
                .Distinct()
                .ToList();

            speciesUniq.ForEach(s => this.logger?.Log(s));

            IDictionary<string, string[]> speciesGenusPairs = new Dictionary<string, string[]>();

            foreach (string species in speciesUniq)
            {
                var genera = this.XmlDocument.SelectNodes($"//tn[@type='lower'][normalize-space(tn-part[@type='species'])='{species}'][normalize-space(tn-part[@type='genus'])!='' or normalize-space(tn-part[@type='genus']/@full-name)!='']/tn-part[@type='genus']")
                    .Cast<XmlElement>()
                    .Select(g =>
                    {
                        if ((string.IsNullOrWhiteSpace(g.InnerText) || g.InnerText.Contains('.')) && g.Attributes["full-name"] != null)
                        {
                            return g.Attributes["full-name"].InnerText;
                        }
                        else
                        {
                            return g.InnerText;
                        }
                    })
                    .Distinct()
                    .ToList();


                speciesGenusPairs.Add(species, genera.ToArray());
            }

            foreach (string species in speciesGenusPairs.Keys)
            {
                this.logger?.Log(species);

                switch (speciesGenusPairs[species].Length)
                {
                    case 0:
                        this.logger?.Log(LogType.Warning, "No matches.");
                        break;

                    case 1:
                        string genus = speciesGenusPairs[species].FirstOrDefault();
                        this.logger?.Log(genus);

                        this.XmlDocument.SelectNodes($"//tn[@type='lower'][normalize-space(tn-part[@type='species'])='{species}'][normalize-space(tn-part[@type='genus'])=''][normalize-space(tn-part[@type='genus']/@full-name)='']/tn-part[@type='genus']")
                            .Cast<XmlElement>()
                            .AsParallel()
                            .ForAll(t =>
                            {
                                var fullNameAttribute = t.Attributes["full-name"];
                                if (fullNameAttribute == null)
                                {
                                    t.SetAttribute("full-name", genus);
                                }
                                else
                                {
                                    fullNameAttribute.InnerText = genus;
                                }
                            });

                        break;

                    default:
                        this.logger?.Log(LogType.Warning, "Multiple matches:");
                        speciesGenusPairs[species].ToList().ForEach(g => this.logger?.Log("--> {0}", g));
                        break;
                }

                this.logger?.Log();
            }
        }
    }
}
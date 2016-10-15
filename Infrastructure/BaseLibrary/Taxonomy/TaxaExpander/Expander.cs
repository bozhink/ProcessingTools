namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Models;

    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Extensions;

    public class Expander
    {
        private readonly ILogger logger;

        public Expander(ILogger logger)
        {
            this.logger = logger;
        }

        private static void PrintMethodMessage(string name, ILogger logger)
        {
            logger?.Log("\n\n#\n##\n### {0} will be executed...\n##\n#\n", name);
        }

        private static void PrintNextShortened(Species sp, ILogger logger)
        {
            logger?.Log("\nNext shortened taxon:\t{0}", sp.ToString());
        }

        private static void PrintSubstitutionMessage(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tSubstitution:\t{0}\t-->\t{1}", original.ToString(), substitution.ToString());
        }

        private static void PrintSubstitutionMessageFail(Species original, Species substitution, ILogger logger)
        {
            logger?.Log("\tFailed Subst:\t{0}\t<->\t{1}", original.ToString(), substitution.ToString());
        }

        public void StableExpand(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // In this method it is supposed that the subspecies name is not shortened
            PrintMethodMessage("StableExpand", this.logger);

            var shortTaxaListUnique = context.GetListOfShortenedTaxa();
            var nonShortTaxaListUnique = context.GetListOfNonShortenedTaxa();

            var speciesList = nonShortTaxaListUnique
                .Select(t => new Species(t))
                .ToList();

            string xml = context.InnerXml;

            foreach (string shortTaxon in shortTaxaListUnique)
            {
                string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                string replace = text;

                var sp = new Species(shortTaxon);
                PrintNextShortened(sp, this.logger);

                foreach (var sp1 in speciesList)
                {
                    if (string.Compare(sp.SubspeciesName, sp1.SubspeciesName, true) != 0)
                    {
                        continue;
                    }

                    var matchGenus = Regex.Match(sp1.GenusName, sp.GenusPattern);
                    var matchSubgenus = Regex.Match(sp1.SubgenusName, sp.SubgenusPattern);
                    var matchSpecies = Regex.Match(sp1.SpeciesName, sp.SpeciesPattern);

                    if (string.IsNullOrWhiteSpace(sp.SubgenusName))
                    {
                        if (matchGenus.Success && matchSpecies.Success)
                        {
                            if (string.IsNullOrWhiteSpace(sp1.SubgenusName))
                            {
                                PrintSubstitutionMessage(sp, sp1, this.logger);
                                replace = replace
                                    .RegexReplace("(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName)
                                    .RegexReplace("(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                            }
                            else
                            {
                                PrintSubstitutionMessageFail(sp, sp1, this.logger);
                            }
                        }
                    }
                    else
                    {
                        if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                        {
                            PrintSubstitutionMessage(sp, sp1, this.logger);
                            replace = replace
                                .RegexReplace("(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName)
                                .RegexReplace("(?<=type=\"subgenus\"[^>]+full-name=\")(?=\")", sp1.SubgenusName)
                                .RegexReplace("(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                        }
                    }
                }

                xml = Regex.Replace(xml, Regex.Escape(text), replace);
            }

            context.InnerXml = xml;
        }

        public void ForceExactSpeciesMatchExpand(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ICollection<TaxonName> taxonNames = new List<TaxonName>();
            context.SelectNodes(".//tn[@type='lower']")
                .Cast<XmlNode>()
                .ToList()
                .ForEach(t => taxonNames.Add(new TaxonName(t)));

            foreach (var taxonName in taxonNames)
            {
                this.logger?.Log("{0} {1} | {2}", taxonName.Id, taxonName.Type, string.Join(" / ", taxonName.Parts.Select(p => p.FullName).ToArray()));
            }

            string nodeListOfSpeciesInShortenedTaxaNameXPath = ".//tn[@type='lower'][normalize-space(tn-part[@type='species'])!=''][normalize-space(tn-part[@type='genus'])=''][normalize-space(tn-part[@type='genus']/@full-name)='']/tn-part[@type='species']";

            var speciesUniq = context.SelectNodes(nodeListOfSpeciesInShortenedTaxaNameXPath)
                .Cast<XmlNode>()
                .Select(n => n.InnerText)
                .Distinct()
                .ToList();

            speciesUniq.ForEach(s => this.logger?.Log(s));

            IDictionary<string, string[]> speciesGenusPairs = new Dictionary<string, string[]>();

            foreach (string species in speciesUniq)
            {
                var genera = context.SelectNodes($".//tn[@type='lower'][normalize-space(tn-part[@type='species'])='{species}'][normalize-space(tn-part[@type='genus'])!='' or normalize-space(tn-part[@type='genus']/@full-name)!='']/tn-part[@type='genus']")
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

                        context.SelectNodes($".//tn[@type='lower'][normalize-space(tn-part[@type='species'])='{species}'][normalize-space(tn-part[@type='genus'])=''][normalize-space(tn-part[@type='genus']/@full-name)='']/tn-part[@type='genus']")
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
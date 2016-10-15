namespace ProcessingTools.Bio.Taxonomy.Processors.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Parsers;
    using Models.Parsers;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Extensions;

    public class Expander : IExpander
    {
        private readonly ILogger logger;

        public Expander(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<object> Parse(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await Task.Run(() =>
            {
                this.AddFullNameAttributeToTaxonNamePartElements(context);

                this.AddIdAndPositionAttributesToTaxonNameElements(context);

                this.StableExpand(context);
                this.ForceExactSpeciesMatchExpand(context);

                this.RemoveIdAndPositionAttributesToTaxonNameElements(context);
            });

            return true;
        }

        private void AddFullNameAttributeToTaxonNamePartElements(XmlNode context)
        {
            context.SelectNodes($"{XmlInternalSchemaConstants.SelectTaxonNamePartsOfLowerTaxonNamesXPath}[not(@{XmlInternalSchemaConstants.FullNameAttributeName})]")
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(n =>
                {
                    string content = n.InnerText;
                    if (string.IsNullOrWhiteSpace(content) || content.Contains("."))
                    {
                        n.SetAttribute(XmlInternalSchemaConstants.FullNameAttributeName, string.Empty);
                    }
                    else
                    {
                        n.SetAttribute(XmlInternalSchemaConstants.FullNameAttributeName, content);
                    }
                });
        }

        private void AddIdAndPositionAttributesToTaxonNameElements(XmlNode context)
        {
            long counter = 1L;

            {
                var taxonNameElements = context.SelectNodes(XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath)
                    .Cast<XmlElement>();

                foreach (var taxonNameElement in taxonNameElements)
                {
                    taxonNameElement.SetAttribute(
                        XmlInternalSchemaConstants.IdAttributeName,
                        XmlInternalSchemaConstants.TaxonNameIdPrefix + counter);
                    taxonNameElement.SetAttribute(
                        XmlInternalSchemaConstants.PositionAttributeName,
                        counter.ToString());
                    ++counter;
                }
            }

            {
                var taxonNamePartElements = context.SelectNodes(XmlInternalSchemaConstants.SelectTaxonNamePartsOfLowerTaxonNamesXPath)
                    .Cast<XmlElement>();

                foreach (var taxonNamePartElement in taxonNamePartElements)
                {
                    taxonNamePartElement.SetAttribute(
                        XmlInternalSchemaConstants.IdAttributeName,
                        XmlInternalSchemaConstants.TaxonNamePartIdPrefix + counter);
                    ++counter;
                }
            }
        }

        private void RemoveIdAndPositionAttributesToTaxonNameElements(XmlNode context)
        {
            context.SelectNodes(XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath)
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(n =>
                {
                    n.RemoveAttribute(XmlInternalSchemaConstants.IdAttributeName);
                    n.RemoveAttribute(XmlInternalSchemaConstants.PositionAttributeName);
                });

            context.SelectNodes(XmlInternalSchemaConstants.SelectTaxonNamePartsOfLowerTaxonNamesXPath)
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(n =>
                {
                    n.RemoveAttribute(XmlInternalSchemaConstants.IdAttributeName);
                });
        }

        private void StableExpand(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // In this method it is supposed that the subspecies name is not shortened
            this.PrintMethodMessage(nameof(this.StableExpand));

            var shortTaxaListUnique = this.GetListOfShortenedTaxa(context);
            var nonShortTaxaListUnique = this.GetListOfNonShortenedTaxa(context);

            var speciesList = nonShortTaxaListUnique
                .Select(t => new Species(t))
                .ToList();

            string xml = context.InnerXml;

            foreach (string shortTaxon in shortTaxaListUnique)
            {
                string text = Regex.Replace(shortTaxon, " xmlns:\\w+=\".*?\"", string.Empty);
                string replace = text;

                var sp = new Species(shortTaxon);
                this.PrintNextShortened(sp);

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
                                this.PrintSubstitutionMessage(sp, sp1);
                                replace = replace
                                    .RegexReplace("(?<=type=\"genus\"[^>]+full-name=\")(?=\")", sp1.GenusName)
                                    .RegexReplace("(?<=type=\"species\"[^>]+full-name=\")(?=\")", sp1.SpeciesName);
                            }
                            else
                            {
                                this.PrintSubstitutionMessageFail(sp, sp1);
                            }
                        }
                    }
                    else
                    {
                        if (matchGenus.Success && matchSubgenus.Success && matchSpecies.Success)
                        {
                            this.PrintSubstitutionMessage(sp, sp1);
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

        private IQueryable<ITaxonName> GetContextTaxonVectorModel(XmlNode context)
        {
            var taxa = context.SelectNodes(XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath)
                .Cast<XmlNode>()
                .Select(t => new TaxonName(t))
                .ToArray();

            return new HashSet<ITaxonName>(taxa).AsQueryable();
        }

        private void ForceExactSpeciesMatchExpand(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var taxonNames = this.GetContextTaxonVectorModel(context);
            foreach (var taxonName in taxonNames.OrderBy(t => t.Position))
            {
                this.logger?.Log("{0} {1} {2} | {3}", taxonName.Id, taxonName.Type, taxonName.Position, string.Join(" / ", taxonName.Parts.Select(p => p.FullName + "_" + p.Rank).ToArray()));
            }

            string nodeListOfSpeciesInShortenedTaxaNameXPath = $"{XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath}[normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeSpeciesXPath})!=''][normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath})=''][normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}/@full-name)='']/{XmlInternalSchemaConstants.TaxonNamePartOfTypeSpeciesXPath}";

            var speciesUniq = context.SelectNodes(nodeListOfSpeciesInShortenedTaxaNameXPath)
                .Cast<XmlNode>()
                .Select(n => n.InnerText)
                .Distinct()
                .ToList();

            speciesUniq.ForEach(s => this.logger?.Log(s));

            IDictionary<string, string[]> speciesGenusPairs = new Dictionary<string, string[]>();

            foreach (string species in speciesUniq)
            {
                var genera = context.SelectNodes($"{XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath}[normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeSpeciesXPath})='{species}'][normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath})!='' or normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}/@full-name)!='']/{XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}")
                    .Cast<XmlElement>()
                    .Select(g =>
                    {
                        if ((string.IsNullOrWhiteSpace(g.InnerText) || g.InnerText.Contains('.')) && g.Attributes[XmlInternalSchemaConstants.FullNameAttributeName] != null)
                        {
                            return g.Attributes[XmlInternalSchemaConstants.FullNameAttributeName].InnerText;
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

                        context.SelectNodes($"{XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath}[normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeSpeciesXPath})='{species}'][normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath})=''][normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}/@full-name)='']/{XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}")
                            .Cast<XmlElement>()
                            .AsParallel()
                            .ForAll(t =>
                            {
                                var fullNameAttribute = t.Attributes[XmlInternalSchemaConstants.FullNameAttributeName];
                                if (fullNameAttribute == null)
                                {
                                    t.SetAttribute(XmlInternalSchemaConstants.FullNameAttributeName, genus);
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

        private void PrintMethodMessage(string name)
        {
            this.logger?.Log("\n\n#\n##\n### {0} will be executed...\n##\n#\n", name);
        }

        private void PrintNextShortened(Species sp)
        {
            this.logger?.Log("\nNext shortened taxon:\t{0}", sp.ToString());
        }

        private void PrintSubstitutionMessage(Species original, Species substitution)
        {
            this.logger?.Log("\tSubstitution:\t{0}\t-->\t{1}", original.ToString(), substitution.ToString());
        }

        private void PrintSubstitutionMessageFail(Species original, Species substitution)
        {
            this.logger?.Log("\tFailed Subst:\t{0}\t<->\t{1}", original.ToString(), substitution.ToString());
        }

        private IEnumerable<string> GetListOfNonShortenedTaxa(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = node.OwnerDocument();

            string xpath = $"{XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath}[not(tn-part[@full-name=''])][{XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}]";
            var result = node.SelectNodes(xpath)
                .Cast<XmlNode>()
                .Select(currentNode =>
                {
                    XmlElement taxonNameElement = document.CreateElement(XmlInternalSchemaConstants.TaxonNameElementName);
                    foreach (XmlNode innerNode in currentNode.SelectNodes(".//*"))
                    {
                        XmlElement taxonNamePartElement = document.CreateElement(XmlInternalSchemaConstants.TaxonNamePartElementName);

                        // Copy only *type* attributes
                        foreach (XmlAttribute attribute in innerNode.Attributes)
                        {
                            if (attribute.Name.Contains(XmlInternalSchemaConstants.TypeAttributeName))
                            {
                                XmlAttribute typeAttribute = document.CreateAttribute(attribute.Name);
                                typeAttribute.InnerText = attribute.InnerText;
                                taxonNamePartElement.Attributes.Append(typeAttribute);
                            }
                        }

                        // Gets the value of the @full-name attribute if present or the content of the node
                        var fullNameAttribute = innerNode.Attributes[XmlInternalSchemaConstants.FullNameAttributeName];
                        if (fullNameAttribute != null && !string.IsNullOrWhiteSpace(fullNameAttribute.InnerText))
                        {
                            taxonNamePartElement.InnerText = fullNameAttribute.InnerText;
                        }
                        else
                        {
                            taxonNamePartElement.InnerText = innerNode.InnerText;
                        }

                        taxonNameElement.AppendChild(taxonNamePartElement);
                    }

                    return taxonNameElement;
                })
                .ToList<XmlNode>()
                .GetStringListOfUniqueXmlNodes();

            return new HashSet<string>(result);
        }

        public IEnumerable<string> GetListOfShortenedTaxa(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            string xpath = $"{XmlInternalSchemaConstants.SelectLowerTaxonNamesXPath}[tn-part[@full-name[normalize-space(.)='']][normalize-space(.)!='']][{XmlInternalSchemaConstants.TaxonNamePartOfTypeGenusXPath}][normalize-space({XmlInternalSchemaConstants.TaxonNamePartOfTypeSpeciesXPath})!='']";
            var result = node.GetStringListOfUniqueXmlNodes(xpath);

            return new HashSet<string>(result);
        }
    }
}

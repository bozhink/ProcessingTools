namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Parsers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Processors.Models.Bio.Taxonomy;
    using ProcessingTools.Processors.Models.Contracts.Bio.Taxonomy;

    public class Expander : IExpander
    {
        private readonly ILogger logger;

        private readonly Expression<Func<ITaxonNamePart, bool>> matchNotResolvedGenera = MatchNotResolved(SpeciesPartType.Genus);
        private readonly Expression<Func<ITaxonNamePart, bool>> matchResolvedGenera = MatchResolved(SpeciesPartType.Genus);
        private readonly Expression<Func<ITaxonNamePart, bool>> partIsAbbreviatedAndNotResolved = p => p.IsAbbreviated && !p.IsResolved;
        private readonly Expression<Func<ITaxonNamePart, bool>> partIsResolvedOrNotAbbreviated = p => p.IsResolved || !p.IsAbbreviated;
        private readonly Expression<Func<ITaxonNamePart, bool>> partIsWithMeaningfullRank = p => p.Rank != SpeciesPartType.Undefined;

        public Expander(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<object> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run(() =>
            {
                // Parse references
                context.SelectNodes(XPathStrings.ReferencesXPath)
                    .Cast<XmlNode>()
                    .AsParallel()
                    .ForAll(n => this.ParseSync(n));

                ////context.SelectNodes(".//sec[not(sec)]")
                ////    .Cast<XmlNode>()
                ////    .AsParallel()
                ////    .ForAll(n => this.ParseSync(n));

                ////context.SelectNodes(".//p")
                ////    .Cast<XmlNode>()
                ////    .AsParallel()
                ////    .ForAll(n => this.ParseSync(n));

                // Parse documents in merged document
                context.SelectNodes(XPathStrings.HigherDocumentStructure)
                    .Cast<XmlNode>()
                    .AsParallel()
                    .ForAll(n => this.ParseSync(n));

                // Parse the whole context
                return this.ParseSync(context);
            });
        }

        private static Expression<Func<ITaxonNamePart, bool>> MatchNotResolved(SpeciesPartType rank) => p => (p.Rank == rank) && (p.IsAbbreviated && !p.IsResolved);

        private static Expression<Func<ITaxonNamePart, bool>> MatchResolved(SpeciesPartType rank) => p => (p.Rank == rank) && (p.IsResolved || !p.IsAbbreviated);

        private void AddIdAndPositionAttributesToTaxonNameElements(XmlNode context)
        {
            long counter = 1L;
            {
                var taxonNameElements = context.SelectNodes(XPathStrings.LowerTaxonNames)
                    .Cast<XmlElement>();

                foreach (var taxonNameElement in taxonNameElements)
                {
                    taxonNameElement.SetAttribute(
                        AttributeNames.Id,
                        AttributeValues.TaxonNameIdPrefix + counter);
                    taxonNameElement.SetAttribute(
                        AttributeNames.Position,
                        counter.ToString());
                    ++counter;
                }
            }

            {
                var taxonNamePartElements = context.SelectNodes(XPathStrings.TaxonNamePartsOfLowerTaxonNames)
                    .Cast<XmlElement>();

                foreach (var taxonNamePartElement in taxonNamePartElements)
                {
                    taxonNamePartElement.SetAttribute(
                        AttributeNames.Id,
                        AttributeValues.TaxonNamePartIdPrefix + counter);
                    ++counter;
                }
            }
        }

        private void EnsureFullNameAttributeToTaxonNamePartElements(XmlNode context)
        {
            context.SelectNodes($"{XPathStrings.TaxonNamePartsOfLowerTaxonNames}[not(@{AttributeNames.FullName})]")
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(n =>
                {
                    var rank = n.GetAttribute(AttributeNames.Type).ToSpeciesPartType();
                    if (rank == SpeciesPartType.Undefined)
                    {
                        if (n.HasAttribute(AttributeNames.FullName))
                        {
                            n.RemoveAttribute(AttributeNames.FullName);
                        }
                    }
                    else
                    {
                        if (!n.HasAttribute(AttributeNames.FullName))
                        {
                            string content = n.InnerText;
                            if (string.IsNullOrWhiteSpace(content) || content.Contains("."))
                            {
                                n.SetAttribute(AttributeNames.FullName, string.Empty);
                            }
                            else
                            {
                                n.SetAttribute(AttributeNames.FullName, content);
                            }
                        }
                    }
                });
        }

        private void ForceExactSpeciesMatchExpand(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string nodeListOfSpeciesInShortenedTaxaNameXPath = $"{XPathStrings.LowerTaxonNames}[normalize-space({XPathStrings.TaxonNamePartOfTypeSpecies})!=''][normalize-space({XPathStrings.TaxonNamePartOfTypeGenus})=''][normalize-space({XPathStrings.TaxonNamePartOfTypeGenus}/@full-name)='']/{XPathStrings.TaxonNamePartOfTypeSpecies}";

            var speciesUniq = context.SelectNodes(nodeListOfSpeciesInShortenedTaxaNameXPath)
                .Cast<XmlNode>()
                .Select(n => n.InnerText)
                .Distinct()
                .ToList();

            IDictionary<string, string[]> speciesGenusPairs = new Dictionary<string, string[]>();

            foreach (string species in speciesUniq)
            {
                var genera = context.SelectNodes($"{XPathStrings.LowerTaxonNames}[normalize-space({XPathStrings.TaxonNamePartOfTypeSpecies})='{species}'][normalize-space({XPathStrings.TaxonNamePartOfTypeGenus})!='' or normalize-space({XPathStrings.TaxonNamePartOfTypeGenus}/@{AttributeNames.FullName})!='']/{XPathStrings.TaxonNamePartOfTypeGenus}")
                    .Cast<XmlElement>()
                    .Select(g =>
                    {
                        if ((string.IsNullOrWhiteSpace(g.InnerText) || g.InnerText.Contains('.')) && g.Attributes[AttributeNames.FullName] != null)
                        {
                            return g.Attributes[AttributeNames.FullName].InnerText.Trim();
                        }
                        else
                        {
                            return g.InnerText.Trim();
                        }
                    })
                    .Where(g => !string.IsNullOrWhiteSpace(g) && !g.Contains("."))
                    .Distinct()
                    .ToList();

                speciesGenusPairs.Add(species, genera.ToArray());
            }

            foreach (string species in speciesGenusPairs.Keys)
            {
                this.logger?.Log(message: species);

                switch (speciesGenusPairs[species].Length)
                {
                    case 0:
                        this.logger?.Log(type: LogType.Warning, message: "No matches.");
                        break;

                    case 1:
                        string genus = speciesGenusPairs[species].FirstOrDefault();
                        this.logger?.Log(message: genus);

                        context.SelectNodes($"{XPathStrings.LowerTaxonNames}[normalize-space({XPathStrings.TaxonNamePartOfTypeSpecies})='{species}'][normalize-space({XPathStrings.TaxonNamePartOfTypeGenus})=''][normalize-space({XPathStrings.TaxonNamePartOfTypeGenus}/@full-name)='']/{XPathStrings.TaxonNamePartOfTypeGenus}")
                            .Cast<XmlElement>()
                            .AsParallel()
                            .ForAll(t =>
                            {
                                var fullNameAttribute = t.Attributes[AttributeNames.FullName];
                                if (fullNameAttribute == null)
                                {
                                    t.SetAttribute(AttributeNames.FullName, genus);
                                }
                                else
                                {
                                    fullNameAttribute.InnerText = genus;
                                }
                            });

                        break;

                    default:
                        this.logger?.Log(type: LogType.Warning, message: "Multiple matches:");
                        speciesGenusPairs[species].ToList().ForEach(g => this.logger?.Log("--> {0}", g));
                        break;
                }

                this.logger?.Log();
            }
        }

        private IQueryable<ITaxonName> GetContextTaxonVectorModel(XmlNode context)
        {
            var taxa = context.SelectNodes(XPathStrings.LowerTaxonNames)
                .Cast<XmlNode>()
                .Select(t => new TaxonName(t))
                .ToArray();

            return taxa.AsQueryable();
        }

        private IEnumerable<string> GetListOfNonShortenedTaxa(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = node.OwnerDocument();

            string xpath = $"{XPathStrings.LowerTaxonNames}[not(tn-part[@full-name=''])][{XPathStrings.TaxonNamePartOfTypeGenus}]";
            var result = node.SelectNodes(xpath)
                .Cast<XmlNode>()
                .Select(currentNode =>
                {
                    XmlElement taxonNameElement = document.CreateElement(ElementNames.TaxonName);
                    foreach (XmlNode innerNode in currentNode.SelectNodes(".//*"))
                    {
                        XmlElement taxonNamePartElement = document.CreateElement(ElementNames.TaxonNamePart);

                        // Copy only *type* attributes
                        foreach (XmlAttribute attribute in innerNode.Attributes)
                        {
                            if (attribute.Name.Contains(AttributeNames.Type))
                            {
                                XmlAttribute typeAttribute = document.CreateAttribute(attribute.Name);
                                typeAttribute.InnerText = attribute.InnerText;
                                taxonNamePartElement.Attributes.Append(typeAttribute);
                            }
                        }

                        // Gets the value of the @full-name attribute if present or the content of the node
                        var fullNameAttribute = innerNode.Attributes[AttributeNames.FullName];
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
                });

            return new HashSet<string>(result.Select(c => c.InnerXml));
        }

        private IEnumerable<string> GetListOfShortenedTaxa(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            string xpath = $"{XPathStrings.LowerTaxonNames}[tn-part[@full-name[normalize-space(.)='']][normalize-space(.)!='']][{XPathStrings.TaxonNamePartOfTypeGenus}][normalize-space({XPathStrings.TaxonNamePartOfTypeSpecies})!='']";

            var result = node.SelectNodes(xpath)
                .Cast<XmlNode>()
                .Select(c => c.InnerXml);

            return new HashSet<string>(result);
        }

        private void ImportModifiedItems(XmlNode context, IQueryable<ITaxonName> taxonNames)
        {
            var modifiedTaxonNameParts = taxonNames
                .Where(tn => tn.Parts.Any(p => p.IsModified))
                .SelectMany(tn => tn.Parts.Where(p => p.IsModified))
                .ToArray();

            modifiedTaxonNameParts.AsParallel()
                .ForAll(p =>
                {
                    var xpath = $".//{ElementNames.TaxonNamePart}[@{AttributeNames.Id}='{p.Id}']";

                    var taxonNamePartElement = context.SelectSingleNode(xpath) as XmlElement;
                    taxonNamePartElement.SetAttribute(AttributeNames.FullName, p.FullName);
                });
        }

        private object ParseSync(XmlNode context)
        {
            this.EnsureFullNameAttributeToTaxonNamePartElements(context);

            this.AddIdAndPositionAttributesToTaxonNameElements(context);

            try
            {
                var taxonNames = this.GetContextTaxonVectorModel(context);
                ////this.PrintContextVectorModel(taxonNames);

                this.UniqueGenusMatchExpand(taxonNames);
                this.StableExpand(taxonNames);
                ////this.ResolveSubgenericByGenusAndLowerMatches(taxonNames, SpeciesPartType.Species);
                this.ImportModifiedItems(context, taxonNames);
                this.ForceExactSpeciesMatchExpand(context);
            }
            catch (Exception e)
            {
                this.logger?.Log(exception: e, message: string.Empty);
            }

            this.RemoveIdAndPositionAttributesToTaxonNameElements(context);

            return true;
        }

        private void PrintContextVectorModel(IQueryable<ITaxonName> taxonNames)
        {
            this.logger?.Log();

            foreach (var taxonName in taxonNames.OrderBy(t => t.Position))
            {
                this.logger?.Log(taxonName);
            }

            this.logger?.Log();
        }

        private void RemoveIdAndPositionAttributesToTaxonNameElements(XmlNode context)
        {
            context.SelectNodes(XPathStrings.LowerTaxonNames)
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(n =>
                {
                    n.RemoveAttribute(AttributeNames.Id);
                    n.RemoveAttribute(AttributeNames.Position);
                });

            context.SelectNodes(XPathStrings.TaxonNamePartsOfLowerTaxonNames)
                .Cast<XmlElement>()
                .AsParallel()
                .ForAll(n =>
                {
                    n.RemoveAttribute(AttributeNames.Id);
                });
        }

        private void ResolveSubgenericByGenusAndLowerMatches(IQueryable<ITaxonName> taxonNames, SpeciesPartType rank)
        {
            if (rank <= SpeciesPartType.Genus)
            {
                return;
            }

            var matchResolved = MatchResolved(rank);
            var matchNotResolved = MatchNotResolved(rank);

            var taxaWithResolvedPart = taxonNames.Where(t => t.Parts.Any(this.matchResolvedGenera) && t.Parts.Any(matchResolved))
                .ToList();

            var taxaWithNotResolvedPart = taxonNames.Where(t => t.Parts.Any(this.matchResolvedGenera) && t.Parts.Any(matchNotResolved) && t.Parts.Any(p => p.Rank > rank));

            var messageBag = new StringBuilder();
            foreach (var taxon in taxaWithNotResolvedPart)
            {
                messageBag.Append(taxon);
                messageBag.AppendLine();

                try
                {
                    string genus = taxon.Parts.First(p => p.Rank == SpeciesPartType.Genus).FullName;

                    var query = taxaWithResolvedPart.Where(t => t.Parts.Single(p => p.Rank == SpeciesPartType.Genus).FullName == genus);
                    foreach (var part in taxon.Parts.Where(p => p.Rank > rank))
                    {
                        if (!part.IsAbbreviated || part.IsResolved)
                        {
                            query = query.Where(t => t.Parts.FirstOrDefault(p => p.Rank == part.Rank)?.FullName == part.FullName);
                        }
                    }

                    var matches = query.Distinct(new TaxonNameContentEqualityComparer()).ToArray();

                    if (matches.Length == 1)
                    {
                        var match = matches[0];

                        var taxonNamePartValue = match.Parts.First(p => p.Rank == rank).FullName;
                        foreach (var taxonNamePart in taxon.Parts)
                        {
                            if (taxonNamePart.Rank == rank)
                            {
                                taxonNamePart.FullName = taxonNamePartValue;
                                taxonNamePart.IsModified = true;

                                messageBag.AppendFormat("\tSubstitution ({0}):", taxonNamePart.Rank);
                                messageBag.AppendFormat("\t\t{0}", taxonNamePartValue);
                                messageBag.AppendLine();
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    // Skip
                }

                messageBag.AppendLine();
            }

            this.logger?.Log(message: messageBag?.ToString());
        }

        private void StableExpand(IQueryable<ITaxonName> taxonNames)
        {
            var abbreviatedTaxonNames = taxonNames.Where(tn => tn.Parts.Where(this.partIsWithMeaningfullRank).Any(this.partIsAbbreviatedAndNotResolved));
            var expandedTaxonNames = taxonNames.Where(tn => tn.Parts.All(this.partIsResolvedOrNotAbbreviated))
                .Distinct(new TaxonNameContentEqualityComparer())
                .ToList();

            var messages = new ConcurrentQueue<string>();

            abbreviatedTaxonNames.AsParallel()
                .ForAll(abbreviatedTaxonName =>
                {
                    var messageBag = new StringBuilder();
                    messageBag.Append(abbreviatedTaxonName);
                    messageBag.AppendLine();

                    var query = expandedTaxonNames.AsQueryable();

                    var meaningfullTaxonNameParts = abbreviatedTaxonName.Parts.Where(this.partIsWithMeaningfullRank);

                    // Builds the query
                    foreach (var taxonNamePart in meaningfullTaxonNameParts)
                    {
                        query = query.Where(e => e.Parts.Any(taxonNamePart.MatchExpression));
                    }

                    // Executes the query and gets matches
                    var matches = query.Select(e => new MinimalTaxonName
                    {
                        Parts = e.Parts.Select(p => new MinimalTaxonNamePart
                        {
                            Name = p.Name,
                            FullName = p.FullName,
                            Rank = p.Rank
                        })
                    })
                    .Distinct()
                    .ToArray();

                    if (matches.Length < 1)
                    {
                        messageBag.Append("\tNo matches.");
                        messageBag.AppendLine();
                    }
                    else
                    {
                        foreach (var taxonNamePart in meaningfullTaxonNameParts.Where(this.partIsAbbreviatedAndNotResolved))
                        {
                            var matchedFullNames = matches.SelectMany(t => t.Parts.Where(p => p.Rank == taxonNamePart.Rank))
                                .Select(p => p.FullName)
                                .Distinct()
                                .ToArray();

                            if (matchedFullNames.Length < 1)
                            {
                                messageBag.AppendFormat("\tError: taxon-name-part of rank {0} does not have valid matches:", taxonNamePart.Rank);
                                messageBag.AppendLine();
                            }
                            else if (matchedFullNames.Length > 1)
                            {
                                messageBag.AppendFormat("\tError: Multiple matches ({0}):", taxonNamePart.Rank);
                                messageBag.AppendLine();
                            }
                            else
                            {
                                var name = matchedFullNames[0];
                                taxonNamePart.FullName = name;
                                taxonNamePart.IsModified = true;

                                messageBag.AppendFormat("\tSubstitution ({0}):", taxonNamePart.Rank);
                                messageBag.AppendLine();
                            }

                            foreach (var match in matches)
                            {
                                messageBag.AppendFormat("\t\t{0}", match);
                                messageBag.AppendLine();
                            }
                        }
                    }

                    messageBag.AppendLine();

                    messages.Enqueue(messageBag.ToString());
                });

            foreach (var message in messages)
            {
                this.logger?.Log(message: message);
            }
        }

        private void UniqueGenusMatchExpand(IQueryable<ITaxonName> taxonNames)
        {
            var messages = new ConcurrentQueue<string>();

            var query = taxonNames.SelectMany(t => t.Parts.Where(this.matchResolvedGenera).Select(p => p.FullName));
            var genera = new HashSet<string>(query.ToArray());

            var abbreviatedGenera = taxonNames.SelectMany(t => t.Parts.Where(this.matchNotResolvedGenera));
            abbreviatedGenera.AsParallel()
                .ForAll(abbreviatedGenus =>
                {
                    var matches = genera.Where(g => g.IndexOf(abbreviatedGenus.Pattern) == 0).ToArray();
                    if (matches.Length == 1)
                    {
                        {
                            var messageBag = new StringBuilder();
                            messageBag.AppendFormat("\tSubstitution ({0}):", abbreviatedGenus);
                            messageBag.AppendLine();
                            foreach (var match in matches)
                            {
                                messageBag.AppendFormat("\t\t{0}", match);
                                messageBag.AppendLine();
                            }

                            messages.Enqueue(messageBag.ToString());
                        }

                        abbreviatedGenus.FullName = matches[0];
                        abbreviatedGenus.IsModified = true;
                    }
                });

            foreach (var message in messages)
            {
                this.logger?.Log(message: message);
            }
        }
    }
}

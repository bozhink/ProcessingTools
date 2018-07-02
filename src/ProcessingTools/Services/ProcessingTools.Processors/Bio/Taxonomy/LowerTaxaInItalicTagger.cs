// <copyright file="LowerTaxaInItalicTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Lower taxa in italic tagger.
    /// </summary>
    public class LowerTaxaInItalicTagger : ILowerTaxaInItalicTagger
    {
        private const string ItalicXPath = ".//i[not(ancestor::i)][not(ancestor::italic)][not(ancestor::Italic)][not(tn)]|.//italic[not(ancestor::i)][not(ancestor::italic)][not(ancestor::Italic)][not(tn)]|.//Italic[not(ancestor::i)][not(ancestor::italic)][not(ancestor::Italic)][not(tn)]";

        private readonly IPersonNamesHarvester personNamesHarvester;
        private readonly IBlackList blacklist;

        /// <summary>
        /// Initializes a new instance of the <see cref="LowerTaxaInItalicTagger"/> class.
        /// </summary>
        /// <param name="personNamesHarvester">Person names harvester.</param>
        /// <param name="blacklist">Taxonomic black list.</param>
        public LowerTaxaInItalicTagger(IPersonNamesHarvester personNamesHarvester, IBlackList blacklist)
        {
            this.personNamesHarvester = personNamesHarvester ?? throw new ArgumentNullException(nameof(personNamesHarvester));
            this.blacklist = blacklist ?? throw new ArgumentNullException(nameof(blacklist));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var knownLowerTaxaNames = this.GetKnownLowerTaxa(context);

            var plausibleLowerTaxa = new HashSet<string>(this.GetPlausibleLowerTaxa(context).Concat(knownLowerTaxaNames));

            plausibleLowerTaxa = new HashSet<string>((await this.ClearFakeTaxaNames(context, plausibleLowerTaxa).ConfigureAwait(false))
                .Select(name => name.ToLowerInvariant()));

            this.TagDirectTaxonomicMatches(context, plausibleLowerTaxa);

            return true;
        }

        private void TagDirectTaxonomicMatches(IDocument document, IEnumerable<string> taxonomicNames)
        {
            var comparer = StringComparer.InvariantCultureIgnoreCase;

            // Tag all direct matches
            document.SelectNodes(ItalicXPath)
                .AsParallel()
                .ForAll(node =>
                {
                    if (taxonomicNames.Contains(node.InnerText, comparer))
                    {
                        var tn = document.CreateTaxonNameXmlElement(TaxonType.Lower);
                        tn.InnerXml = node.InnerXml
                            .RegexReplace(@"\A([A-Z][A-Za-z]{0,2}\.)([a-z])", "$1 $2");
                        node.InnerXml = tn.OuterXml;
                    }
                });
        }

        private IEnumerable<string> GetPlausibleLowerTaxa(IDocument document)
        {
            var result = document.SelectNodes(ItalicXPath)
                .Select(x => x.InnerText)
                .Where(this.IsMatchingLowerTaxaFormat)
                .Distinct()
                .ToArray();

            return result;
        }

        private IEnumerable<string> GetKnownLowerTaxa(IDocument document)
        {
            var result = document.SelectNodes(".//tn[@type='lower']")
                .Select(x => x.InnerText)
                .Distinct()
                .ToArray();

            return result;
        }

        private bool IsMatchingLowerTaxaFormat(string textToCheck)
        {
            bool result = false;

            result |= Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*(?<species>[a-z\.×-]*)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*(?<species>[a-z\.×-]+)\s*(?<subspecies>[a-z×-]+)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*\(\s*(?<subgenus>[A-Za-z][a-z\.×]+)\s*\)\s*(?<species>[a-z\.×-]*)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z][a-z\.×]+[’”]?[‘“]?(?:\-[A-Z][a-z\.×]+)?[’”]?)\s*\(\s*(?<subgenus>[A-Za-z][a-z\.×]+)\s*\)\s*(?<species>[a-z\.×-]+)\s*(?<subspecies>[a-z×-]+)\Z") ||
                      Regex.IsMatch(textToCheck, @"\A(?<genus>[‘“]?[A-Z]{2,}[’”]?)\Z");

            result &= !textToCheck.Contains("s.n.") && !textToCheck.Contains(" coll.");

            return result;
        }

        private async Task<IEnumerable<string>> ClearFakeTaxaNames(IDocument document, IEnumerable<string> taxaNames)
        {
            var taxaNamesFirstWord = await this.GetTaxaNamesFirstWord(document, taxaNames).ConfigureAwait(false);

            var result = taxaNames.Where(tn => taxaNamesFirstWord.Contains(tn.GetFirstWord()));
            return result;
        }

        private async Task<IEnumerable<string>> GetTaxaNamesFirstWord(IDocument document, IEnumerable<string> taxaNames)
        {
            var stopWords = await this.GetStopWords(document.XmlDocument.DocumentElement).ConfigureAwait(false);

            var taxaNamesFirstWord = taxaNames.GetFirstWord()
                .Distinct()
                .DistinctWithStopWords(stopWords)
                .ToArray();

            return new HashSet<string>(taxaNamesFirstWord);
        }

        private async Task<IEnumerable<string>> GetStopWords(XmlNode context)
        {
            var personNames = await this.personNamesHarvester.HarvestAsync(context);
            var blacklistItems = await this.blacklist.GetItemsAsync();

            var stopWords = personNames
                .SelectMany(n => new[] { n.GivenNames, n.Surname, n.Suffix, n.Prefix })
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Union(blacklistItems)
                .Select(w => w.ToLowerInvariant())
                .Distinct()
                .ToArray();

            return stopWords;
        }
    }
}

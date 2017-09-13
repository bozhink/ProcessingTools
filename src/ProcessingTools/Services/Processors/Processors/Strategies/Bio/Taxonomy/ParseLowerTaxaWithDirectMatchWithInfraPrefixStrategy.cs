namespace ProcessingTools.Processors.Strategies.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Processors.Common.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Strategies.Bio.Taxonomy;

    public class ParseLowerTaxaWithDirectMatchWithInfraPrefixStrategy : IParseLowerTaxaWithDirectMatchWithInfraPrefixStrategy
    {
        public int ExecutionPriority => 100;

        public async Task<object> ParseAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var xml = context.InnerXml;

            var prefixes = SpeciesPartsPrefixesResolver.NonAmbiguousSpeciesPartsRanks
                .Select(p => p.Key)
                .OrderByDescending(k => k.Length)
                .ToArray();

            foreach (var prefix in prefixes)
            {
                var rank = SpeciesPartsPrefixesResolver.SpeciesPartsRanks[prefix].ToRankString();
                var replacement = @"<tn-part type=""" + rank + @""">$1</tn-part>";

                xml = xml
                    .RegexReplace(
                        @"(?i)(?<=\b" + prefix + @"\.?\s*<i\b[^>]*><tn\b[^>]+type=""lower""[^>]*>)(\S+)(?=</tn></i>)",
                        replacement)
                    .RegexReplace(
                        @"(?i)(?<=\b" + prefix + @"\.?\s*<tn\b[^>]+type=""lower""[^>]*>)(\S+)(?=</tn>)",
                        replacement);
            }

            context.InnerXml = xml;

            return await Task.FromResult(true).ConfigureAwait(false);
        }
    }
}

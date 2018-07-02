// <copyright file="ParseLowerTaxaWithDirectMatchWithInfraPrefixStrategy.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy.Strategies
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Parse lower taxa with direct match with infra-prefix strategy.
    /// </summary>
    public class ParseLowerTaxaWithDirectMatchWithInfraPrefixStrategy : IParseLowerTaxaWithDirectMatchWithInfraPrefixStrategy
    {
        /// <inheritdoc/>
        public int ExecutionPriority => 100;

        /// <inheritdoc/>
        public Task<object> ParseAsync(XmlNode context)
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

            return Task.FromResult<object>(true);
        }
    }
}

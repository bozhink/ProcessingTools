/*
 * 1 male, 1 female
 * 2 spec.
 * 2 exx.
 * 2 spp.
 * 2 exx
 * 1 ex
 * 1 ex.
 * 1♂&amp;1♀
 * 2 larvae
 * 1 larva
 * 0m, 7f, 3i
 */

namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;

    public class SpecimenCountDataMiner : ISpecimenCountDataMiner
    {
        private const string RangeOfItemsSubPattern = @"(?:\d+(?:\s*[–—−‒-]?\s*))+";
        private const string Pattern = @"((?i)" + RangeOfItemsSubPattern + @"[^\w<>\(\)\[\]]{0,5}(?:(?:[♀♂]|[fmij]\b|\bexx?\b\.?|\bspp\b\.?|\bmales?\b|\bfemales?\b|\bjuveniles?\b|\blarvae?\b|\badults?\b|(?:\bdry\b\s*|\bwet\b\s*)?\bspecimens?\b|\bspec\b\.?|\bsex undetermined\b|\bunsexed\b(?:\s+specimens?\b)?)\s*?)+)";

        public async Task<IEnumerable<string>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            Regex matchSpecimenCount = new Regex(Pattern);
            var result = new HashSet<string>(await context.GetMatchesAsync(matchSpecimenCount).ConfigureAwait(false));
            return result;
        }
    }
}

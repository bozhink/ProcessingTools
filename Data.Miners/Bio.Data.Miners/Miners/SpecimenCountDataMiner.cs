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
 */

namespace ProcessingTools.Bio.Data.Miners
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Infrastructure.Extensions;

    public class SpecimenCountDataMiner : ISpecimenCountDataMiner
    {
        private const string RangeOfItemsSubPattern = @"(?:\d+(?:\s*[–—−‒-]?\s*))+";
        private const string Pattern = @"((?i)" + RangeOfItemsSubPattern + @"[^\w<>\(\)\[\]]{0,5}(?:(?:[♀♂]|males?|females?|juveniles?|larvae?)\s*?)+)";

        public async Task<IQueryable<string>> Mine(string content)
        {
            Regex matchSpecimenCount = new Regex(Pattern);
            var result = new HashSet<string>(await content.GetMatchesAsync(matchSpecimenCount));
            return result.AsQueryable();
        }
    }
}
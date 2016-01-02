/*
 * 1 male, 1 female
 * 2 spec.
 * 2 exx.
 * 2 spp.
 * 1 ex.
 * 1♂&amp;1♀
 */

namespace ProcessingTools.Bio.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Extensions;

    public class SpecimenCountHarvester : ISpecimenCountHarvester
    {
        private const string Pattern = @"((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>\(\)\[\]]{0,5}(?:[♀♂]|males?|females?|juveniles?)+)";

        public async Task<IQueryable<string>> Harvest(string content)
        {
            Regex matchSpecimenCount = new Regex(Pattern);
            var result = new HashSet<string>(await content.GetMatchesAsync(matchSpecimenCount));
            return result.AsQueryable();
        }
    }
}
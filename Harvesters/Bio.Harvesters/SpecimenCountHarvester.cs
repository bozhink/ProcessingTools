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

    using Contracts;
    using Extensions;

    public class SpecimenCountHarvester : ISpecimenCountHarvester
    {
        private ICollection<string> data;

        public SpecimenCountHarvester()
        {
            this.data = new HashSet<string>();
        }

        public IQueryable<string> Data
        {
            get
            {
                return this.data.AsQueryable();
            }
        }

        public void Harvest(string content)
        {
            const string Pattern = @"((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>\(\)\[\]]{0,5}(?:[♀♂]|males?|females?|juveniles?)+)";
            Regex matchSpecimenCount = new Regex(Pattern);

            this.data = new HashSet<string>(content.GetMatches(matchSpecimenCount));
        }
    }
}
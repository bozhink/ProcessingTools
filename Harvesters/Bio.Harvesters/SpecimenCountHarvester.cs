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
    using System.Text.RegularExpressions;

    using Contracts;
    using Extensions;

    using ProcessingTools.Harvesters.Common.Factories;

    public class SpecimenCountHarvester : StringHarvesterFactory, ISpecimenCountHarvester
    {
        public override void Harvest(string content)
        {
            const string Pattern = @"((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>\(\)\[\]]{0,5}(?:[♀♂]|males?|females?|juveniles?)+)";
            Regex matchSpecimenCount = new Regex(Pattern);

            this.Items = new HashSet<string>(content.GetMatches(matchSpecimenCount));
        }
    }
}
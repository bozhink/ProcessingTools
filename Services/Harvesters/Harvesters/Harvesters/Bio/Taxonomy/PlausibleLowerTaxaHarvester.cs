namespace ProcessingTools.Harvesters.Harvesters.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Harvesters.Bio.Taxonomy;
    using System;

    public class PlausibleLowerTaxaHarvester : IPlausibleLowerTaxaHarvester
    {
        private readonly ILowerTaxaHarvester lowerTaxaHarvester;
        private readonly IPlausibleLowerTaxaInItalicHarvester plausibleLowerTaxaInItalicHarvester;

        public PlausibleLowerTaxaHarvester(
            ILowerTaxaHarvester lowerTaxaHarvester,
            IPlausibleLowerTaxaInItalicHarvester plausibleLowerTaxaInItalicHarvester)
        {
            if (lowerTaxaHarvester == null)
            {
                throw new ArgumentNullException(nameof(lowerTaxaHarvester));
            }

            if (plausibleLowerTaxaInItalicHarvester == null)
            {
                throw new ArgumentNullException(nameof(plausibleLowerTaxaInItalicHarvester));
            }

            this.lowerTaxaHarvester = lowerTaxaHarvester;
            this.plausibleLowerTaxaInItalicHarvester = plausibleLowerTaxaInItalicHarvester;
        }

        public async Task<IEnumerable<string>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var knownLowerTaxaNames = await this.lowerTaxaHarvester.Harvest(context);
            var plausibleLowerTaxaInItalics = await this.plausibleLowerTaxaInItalicHarvester.Harvest(context);
            var plausibleLowerTaxa = new HashSet<string>(plausibleLowerTaxaInItalics.Concat(knownLowerTaxaNames));

            return plausibleLowerTaxa;
        }
    }
}

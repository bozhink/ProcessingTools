﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessingTools.Bio.Taxonomy.Contracts;
using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
using ProcessingTools.Bio.Taxonomy.Types;

namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    public class AboveGenusTaxaRankResolverDataService : IAboveGenusTaxaRankResolverDataService
    {
        public Task<IQueryable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>(scientificNames
                .Select(s => new TaxonRankServiceModel
                {
                    ScientificName = s,
                    Rank = TaxonRankType.AboveGenus
                }));

            return Task.FromResult(result.AsQueryable());
        }
    }
}
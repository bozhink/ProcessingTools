﻿namespace ProcessingTools.Wcf.ProxyServices.Bio.Taxonomy.Services
{
    using System;
    using System.Linq;

    using DataContracts;

    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    using ServiceContracts;

    public class GbifClassificationService : IGbifClassificationService
    {
        private readonly IGbifTaxaClassificationDataService service;

        public GbifClassificationService(IGbifTaxaClassificationDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public TaxonClassification GetClassification(string scientificName)
        {
            var result = this.service.Resolve(scientificName).Result.FirstOrDefault();

            return new TaxonClassification
            {
                ScientificName = result.ScientificName,
                Rank = result.Rank
            };
        }
    }
}

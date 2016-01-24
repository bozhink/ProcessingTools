﻿namespace ProcessingTools.MainProgram.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseTreatmentMetaWithAphiaController : ParseTreatmentMetaControllerFactory, IParseTreatmentMetaWithAphiaController
    {
        private readonly IAphiaTaxaClassificationDataService service;

        public ParseTreatmentMetaWithAphiaController(IAphiaTaxaClassificationDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;
        }

        protected override ITaxaDataService<ITaxonClassification> Service => this.service;
    }
}
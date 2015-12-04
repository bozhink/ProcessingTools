﻿namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonomyDataServices;

    public class AphiaController : ApiController
    {
        private IAphiaTaxaClassificationDataService aphiaService;

        public AphiaController(IAphiaTaxaClassificationDataService aphiaService)
        {
            this.aphiaService = aphiaService;
        }

        public IHttpActionResult Get(string id)
        {
            var result = this.aphiaService.Resolve(id)?.ProjectTo<TaxonClassificationResponseModel>().ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
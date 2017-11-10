﻿namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Constants;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;
    using ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyBlackList;

    [Authorize]
    public class BioTaxonomyBlackListDataController : Controller
    {
        private readonly IBlackListDataService dataService;
        private readonly IBlackListSearchService searchService;

        public BioTaxonomyBlackListDataController(
            IBlackListDataService dataService,
            IBlackListSearchService searchService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            this.searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        [HttpPost]
        public async Task<JsonResult> Post(BlackListItemsRequestModel viewModel)
        {
            if (viewModel == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var taxa = viewModel.Items
                .Select(i => i.Content)
                .ToArray();

            await this.dataService.AddAsync(taxa).ConfigureAwait(false);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.GetEmptyJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.GetEmptyJsonResult();
            }

            var foundItems = (await this.searchService.Search(searchString).ConfigureAwait(false))
                .ToList();

            if (foundItems.Count < 1)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.GetEmptyJsonResult();
            }

            var responseItems = foundItems.Select(i => new BlackListItemResponseModel
            {
                Content = i
            });

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.GetJsonResult(
                new SearchResposeModel(
                    responseItems.ToArray()));
        }

        private JsonResult GetEmptyJsonResult()
        {
            return this.GetJsonResult(null);
        }

        private JsonResult GetJsonResult(object data)
        {
            return new JsonResult
            {
                ContentType = ContentTypes.Json,
                ContentEncoding = Defaults.Encoding,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = data
            };
        }
    }
}

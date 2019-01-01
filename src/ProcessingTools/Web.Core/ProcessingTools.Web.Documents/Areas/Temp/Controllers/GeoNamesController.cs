namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Web.Services.Geo;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Constants;
    using ProcessingTools.Web.Models.Geo.GeoNames;

    [Authorize]
    public class GeoNamesController : BaseMvcController
    {
        public const string AreaName = AreaNames.Data;
        public const string ControllerName = "GeoNames";
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string CreateActionName = ActionNames.Create;
        public const string EditActionName = ActionNames.Edit;
        public const string DeleteActionName = ActionNames.Delete;

        private readonly IGeoNamesWebService service;
        private readonly ILogger logger;

        public GeoNamesController(IGeoNamesWebService service, ILogger<GeoNamesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ActionName(IndexActionName)]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            int currentPage = p ?? PaginationConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PaginationConstants.DefaultLargeNumberOfItemsPerPage;

            var viewModel = await this.service.SelectAsync(currentPage, numberOfItemsPerPage).ConfigureAwait(false);

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = IndexActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(IndexActionName, viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(CreateActionName)]
        public async Task<ActionResult> Create([Bind(Include = nameof(GeoNamesRequestModel.Names))] GeoNamesRequestModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.service.InsertAsync(model).ConfigureAwait(false);
                }

                string returnUrl = this.Request[ContextKeys.ReturnUrl];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ControllerName);
            }

            return this.RedirectToAction(IndexActionName);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(EditActionName)]
        public async Task<ActionResult> Edit([Bind(Include = nameof(GeoNameRequestModel.Id) + "," + nameof(GeoNameRequestModel.Name))] GeoNameRequestModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.service.UpdateAsync(model).ConfigureAwait(false);
                }

                string returnUrl = this.Request[ContextKeys.ReturnUrl];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ControllerName);
            }

            return this.RedirectToAction(IndexActionName);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(DeleteActionName)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await this.service.DeleteAsync(id).ConfigureAwait(false);

                string returnUrl = this.Request[ContextKeys.ReturnUrl];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ControllerName);
            }

            return this.RedirectToAction(IndexActionName);
        }
    }
}

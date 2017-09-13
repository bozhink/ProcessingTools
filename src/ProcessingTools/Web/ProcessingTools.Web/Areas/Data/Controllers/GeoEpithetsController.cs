namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Constants;
    using ProcessingTools.Web.Contracts.Services.Geo;
    using ProcessingTools.Web.Models.Geo.GeoEpithets;

    [Authorize]
    public class GeoEpithetsController : BaseMvcController
    {
        public const string AreaName = AreaNames.Data;
        public const string ControllerName = "GeoEpithets";
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string CreateActionName = ActionNames.Create;
        public const string EditActionName = ActionNames.Edit;
        public const string DeleteActionName = ActionNames.Delete;

        private readonly IGeoEpithetsWebService service;
        private readonly ILogger logger;

        public GeoEpithetsController(IGeoEpithetsWebService service, ILogger logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger;
        }

        [HttpGet, ActionName(IndexActionName)]
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

            return this.View(IndexActionName, viewModel);
        }

        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = nameof(GeoEpithetsRequestModel.Names))] GeoEpithetsRequestModel model)
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
                this.logger?.Log(exception: ex, message: ControllerName);
            }

            return this.RedirectToAction(IndexActionName);
        }

        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = nameof(GeoEpithetRequestModel.Id) + "," + nameof(GeoEpithetRequestModel.Name))] GeoEpithetRequestModel model)
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
                this.logger?.Log(exception: ex, message: ControllerName);
            }

            return this.RedirectToAction(IndexActionName);
        }

        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
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
                this.logger?.Log(exception: ex, message: ControllerName);
            }

            return this.RedirectToAction(IndexActionName);
        }
    }
}

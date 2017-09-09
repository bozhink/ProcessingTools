namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Areas.Data.Services;
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

        private readonly IGeoNamesService service;
        private readonly ILogger logger;

        public GeoNamesController(IGeoNamesService service, ILogger logger)
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

            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            var viewModel = await this.service.SelectAsync(currentPage, numberOfItemsPerPage);

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = IndexActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(IndexActionName, viewModel);
        }

        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = nameof(GeoNamesRequestModel.Names))] GeoNamesRequestModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.service.InsertAsync(model);
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
        public async Task<ActionResult> Edit([Bind(Include = nameof(GeoNameRequestModel.Id) + "," + nameof(GeoNameRequestModel.Name))] GeoNameRequestModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.service.UpdateAsync(model);
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
                await this.service.DeleteAsync(id);

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

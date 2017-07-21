namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.ViewModels;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Areas.Data.Models.GeoEpithets;
    using ProcessingTools.Web.Constants;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.GeoEpithets.Views_Strings;

    [Authorize]
    public class GeoEpithetsController : BaseMvcController
    {
        public const string ControllerName = "GeoEpithets";
        public const string AreaName = AreaNames.Data;
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string CreateActionName = nameof(GeoEpithetsController.Create);
        public const string EditActionName = nameof(GeoEpithetsController.Edit);
        public const string DeleteActionName = ActionNames.Delete;

        private readonly IGeoEpithetsDataService service;
        private readonly IMapper mapper;

        public GeoEpithetsController(IGeoEpithetsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoEpithet, GeoEpithetViewModel>();
                c.CreateMap<GeoEpithetRequestModel, GeoEpithetViewModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        // GET: Data/GeoNames
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

            long numberOfItems = await this.service.SelectCountAsync(null);
            var data = await this.service.SelectAsync(null, currentPage * numberOfItemsPerPage, numberOfItemsPerPage, nameof(IGeoName.Name), SortOrder.Ascending);
            var items = data.Select(this.mapper.Map<GeoEpithetViewModel>).ToArray();

            var model = new ListWithPagingViewModel<GeoEpithetViewModel>(IndexActionName, numberOfItems, numberOfItemsPerPage, currentPage, items);
            var viewModel = new GeoEpithetsIndexPageViewModel
            {
                Model = model,
                PageTitle = Strings.IndexPageTitle
            };

            return this.View(IndexActionName, viewModel);
        }

        // POST: Data/GeoNames/Create
        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] GeoEpithetRequestModel model)
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

            return this.RedirectToAction(IndexActionName);
        }

        // POST: Data/GeoNames/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] GeoEpithetRequestModel model)
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

            return this.RedirectToAction(IndexActionName);
        }

        // POST: Data/GeoNames/Delete/5
        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.service.DeleteAsync(ids: id);

            string returnUrl = this.Request[ContextKeys.ReturnUrl];
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(IndexActionName);
        }
    }
}

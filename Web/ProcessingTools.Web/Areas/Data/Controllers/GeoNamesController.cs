namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Areas.Data.Models.GeoNames;
    using ProcessingTools.Web.Areas.Data.ViewModels.GeoNames;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Constants;
    using Strings = ProcessingTools.Web.Resources.Areas.Data.Views.GeoNames.Strings;

    public class GeoNamesController : BaseMvcController
    {
        public const string ControllerName = "GeoNames";
        public const string DetailsActionName = nameof(GeoNamesController.Details);
        public const string CreateActionName = nameof(GeoNamesController.Create);
        public const string EditActionName = nameof(GeoNamesController.Edit);
        public const string DeleteActionName = nameof(GeoNamesController.Delete);

        private readonly IGeoNamesDataService service;
        private readonly IMapper mapper;

        public GeoNamesController(IGeoNamesDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoName, GeoNameViewModel>();
                c.CreateMap<GeoNameRequestModel, GeoNameViewModel>();
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
            var items = data.Select(this.mapper.Map<GeoNameViewModel>).ToArray();

            var viewModel = new ListWithPagingViewModel<GeoNameViewModel>(IndexActionName, numberOfItems, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(IndexActionName, viewModel);
        }

        // GET: Data/GeoNames/Details/5
        [HttpGet, ActionName(DetailsActionName)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new GeoNamePageViewModel
            {
                Model = this.mapper.Map<GeoNameViewModel>(model),
                PageTitle = Strings.DetailsPageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(DetailsActionName, viewModel);
        }

        // GET: Data/GeoNames/Create
        [HttpGet, ActionName(CreateActionName)]
        public ActionResult Create()
        {
            var viewModel = new GeoNamePageViewModel
            {
                Model = new GeoNameViewModel { Id = -1 },
                PageTitle = Strings.CreatePageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // POST: Data/GeoNames/Create
        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] GeoNameRequestModel model)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];

            if (this.ModelState.IsValid)
            {
                await this.service.InsertAsync(model);
                await this.service.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction(IndexActionName);
            }

            model.Id = -1;
            var viewModel = new GeoNamePageViewModel
            {
                Model = this.mapper.Map<GeoNameViewModel>(model),
                PageTitle = Strings.CreatePageTitle,
                ReturnUrl = returnUrl
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // GET: Data/GeoNames/Edit/5
        [HttpGet, ActionName(EditActionName)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new GeoNamePageViewModel
            {
                Model = this.mapper.Map<GeoNameViewModel>(model),
                PageTitle = Strings.EditPageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // POST: Data/GeoNames/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] GeoNameRequestModel model)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];

            if (this.ModelState.IsValid)
            {
                await this.service.UpdateAsync(model);
                await this.service.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction(IndexActionName);
            }

            var viewModel = new GeoNamePageViewModel
            {
                Model = this.mapper.Map<GeoNameViewModel>(model),
                PageTitle = Strings.EditPageTitle,
                ReturnUrl = returnUrl
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // GET: Data/GeoNames/Delete/5
        [HttpGet, ActionName(DeleteActionName)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new GeoNamePageViewModel
            {
                Model = this.mapper.Map<GeoNameViewModel>(model),
                PageTitle = Strings.DeletePageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(DeleteActionName, viewModel);
        }

        // POST: Data/GeoNames/Delete/5
        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.service.DeleteAsync(id: id);
            await this.service.SaveChangesAsync();

            string returnUrl = this.Request[ContextKeys.ReturnUrl];
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(IndexActionName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}

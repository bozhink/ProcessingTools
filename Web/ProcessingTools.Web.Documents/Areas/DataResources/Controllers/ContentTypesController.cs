namespace ProcessingTools.Web.Documents.Areas.DataResources.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Constants;
    using ProcessingTools.Resources.Services.Data.Contracts;
    using ProcessingTools.Resources.Services.Data.Models;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Documents.Areas.DataResources.ViewModels.ContentTypes;
    using ProcessingTools.Web.Documents.Areas.DataResources.ViewModels.ContentTypes.Contracts;
    using ProcessingTools.Web.Documents.Factories;

    [Authorize]
    public class ContentTypesController : MvcControllerWithExceptionHandling
    {
        private const string ContentTypeValidationBinding = nameof(IContentTypeEditViewModel.Id) + "," + nameof(IContentTypeEditViewModel.Name);

        private readonly IContentTypesDataService service;

        public ContentTypesController(IContentTypesDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        protected override string InstanceName => InstanceNames.ContentTypesControllerInstanceName;

        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }

        // GET: /Data/Resources/ContentTypes
        [HttpGet]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            ValidationHelpers.ValidatePageNumber(currentPage);
            ValidationHelpers.ValidateNumberOfItemsPerPage(numberOfItemsPerPage);

            long numberOfItems = await this.service.Count();
            var items = (await this.service.All(currentPage, numberOfItemsPerPage))
                .Select(e => new ContentTypeIndexViewModel
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToList();

            var viewModel = new ListWithPagingViewModel<IContentTypeIndexViewModel>(nameof(this.Index), numberOfItems, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: /Data/Resources/ContentTypes/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            ValidationHelpers.ValidateId(id);

            var viewModel = await this.GetDetailsViewModel(id);
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: /Data/Resources/ContentTypes/Create
        [HttpGet]
        public ActionResult Create()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        // POST: /Data/Resources/ContentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = ContentTypeValidationBinding)] ContentTypeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.service.Add(new ContentTypeCreateServiceModel
                {
                    Id = model.Id,
                    Name = model.Name
                });

                this.Response.StatusCode = (int)HttpStatusCode.Redirect;
                return this.RedirectToAction(nameof(this.Index));
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.View(model);
        }

        // GET: /Data/Resources/ContentTypes/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            ValidationHelpers.ValidateId(id);

            var viewModel = await this.GetEditViewModel(id);
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // POST: /Data/Resources/ContentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = ContentTypeValidationBinding)] ContentTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.service.Update(new ContentTypeUpdateServiceModel
                {
                    Id = model.Id,
                    Name = model.Name
                });

                this.Response.StatusCode = (int)HttpStatusCode.Redirect;
                return this.RedirectToAction(nameof(this.Index));
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.View(model);
        }

        // GET: /Data/Resources/ContentTypes/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            ValidationHelpers.ValidateId(id);

            var viewModel = await this.GetDetailsViewModel(id);
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // POST: /Data/Resources/ContentTypes/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.service.Delete(id);
            this.Response.StatusCode = (int)HttpStatusCode.Redirect;
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.TryDispose();
            }

            base.Dispose(disposing);
        }

        private async Task<IContentTypeDetailsViewModel> GetDetailsViewModel(int? id)
        {
            var entity = await this.service.GetDetails(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var viewModel = new ContentTypeDetailsViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return viewModel;
        }

        private async Task<ContentTypeEditViewModel> GetEditViewModel(int? id)
        {
            var entity = await this.service.GetDetails(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var viewModel = new ContentTypeEditViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return viewModel;
        }
    }
}

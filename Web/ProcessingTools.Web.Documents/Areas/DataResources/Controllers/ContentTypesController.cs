namespace ProcessingTools.Web.Documents.Areas.DataResources.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Documents.Areas.DataResources.ViewModels.ContentTypes;
    using ProcessingTools.Web.Documents.Areas.DataResources.ViewModels.ContentTypes.Contracts;
    using ProcessingTools.Web.Documents.Factories;

    [Authorize]
    public class ContentTypesController : MvcControllerWithExceptionHandling
    {
        private const string ContentTypeValidationBinding = nameof(ContentType.Id) + "," + nameof(ContentType.Name);

        private readonly IDataResourcesDbContextProvider contextProvider;

        public ContentTypesController(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        protected override string InstanceName => InstanceNames.ContentTypesControllerInstanceName;

        // GET: /Data/Resources/ContentTypes
        [HttpGet]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            ValidationHelpers.ValidatePageNumber(currentPage);
            ValidationHelpers.ValidateNumberOfItemsPerPage(numberOfItemsPerPage);

            long numberOfItems = 0L;
            IEnumerable<IContentTypeIndexViewModel> items = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.ContentTypes
                    .OrderBy(e => e.Name)
                    .Skip(currentPage * numberOfItemsPerPage)
                    .Take(numberOfItemsPerPage)
                    .Select(e => new ContentTypeIndexViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                items = await query.ToListAsync();
                numberOfItems = await db.ContentTypes.LongCountAsync();
            }

            var viewModel = new ListWithPagingViewModel<IContentTypeIndexViewModel>(nameof(this.Index), numberOfItems, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: /Data/Resources/ContentTypes/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            ValidationHelpers.ValidateId(id);

            IContentTypeDetailsViewModel viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.ContentTypes
                    .Where(e => e.Id.ToString() == id.ToString())
                    .Select(e => new ContentTypeDetailsViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                viewModel = await query.FirstOrDefaultAsync();
            }

            if (viewModel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.View(viewModel);
        }

        // GET: /Data/Resources/ContentTypes/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: /Data/Resources/ContentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = ContentTypeValidationBinding)] ContentType model)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.ContentTypes.Add(model);
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // GET: /Data/Resources/ContentTypes/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            ValidationHelpers.ValidateId(id);

            ContentType viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.ContentTypes.Find(id));
            }

            if (viewModel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/ContentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = ContentTypeValidationBinding)] ContentType model)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // GET: /Data/Resources/ContentTypes/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            ValidationHelpers.ValidateId(id);

            IContentTypeDetailsViewModel viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.ContentTypes
                    .Where(e => e.Id.ToString() == id.ToString())
                    .Select(e => new ContentTypeDetailsViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                viewModel = await query.FirstOrDefaultAsync();
            }

            if (viewModel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/ContentTypes/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var db = this.contextProvider.Create())
            {
                var entity = await Task.FromResult(db.ContentTypes.Find(id));
                db.ContentTypes.Remove(entity);
                await db.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

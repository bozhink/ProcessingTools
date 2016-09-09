namespace ProcessingTools.Web.Documents.Areas.DataResources.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Entity.Models;

    public class ContentTypesController : Controller
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

        // GET: /Data/Resources/ContentTypes
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<ContentType> viewModels = null;

            using (var db = this.contextProvider.Create())
            {
                viewModels = await db.ContentTypes.ToListAsync();
            }

            if (viewModels == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModels);
        }

        // GET: /Data/Resources/ContentTypes/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContentType viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.ContentTypes.Find(id));
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
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
        public async Task<ActionResult> Create([Bind(Include = ContentTypeValidationBinding)] ContentType contentType)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.ContentTypes.Add(contentType);
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(contentType);
        }

        // GET: /Data/Resources/ContentTypes/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContentType viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.ContentTypes.Find(id));
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/ContentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = ContentTypeValidationBinding)] ContentType contentType)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.Entry(contentType).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(contentType);
        }

        // GET: /Data/Resources/ContentTypes/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContentType viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.ContentTypes.Find(id));
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
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

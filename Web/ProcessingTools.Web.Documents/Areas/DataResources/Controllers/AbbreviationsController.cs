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

    public class AbbreviationsController : Controller
    {
        private const string AbbreviationValidationBinding = nameof(Abbreviation.Id) + "," +
            nameof(Abbreviation.Name) + "," +
            nameof(Abbreviation.Definition) + "," +
            nameof(Abbreviation.ContentTypeId);

        private readonly IDataResourcesDbContextProvider contextProvider;

        public AbbreviationsController(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        // GET: /Data/Resources/Abbreviations
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Abbreviation> viewModels = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.Abbreviations.Include(a => a.ContentType);
                viewModels = await query.ToListAsync();
            }

            if (viewModels == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModels);
        }

        // GET: /Data/Resources/Abbreviations/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Abbreviation viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await db.Abbreviations.FindAsync(id);
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        // GET: /Data/Resources/Abbreviations/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList();
            return this.View();
        }

        // POST: /Data/Resources/Abbreviations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = AbbreviationValidationBinding)] Abbreviation abbreviation)
        {
            if (ModelState.IsValid)
            {
                abbreviation.Id = Guid.NewGuid();

                using (var db = this.contextProvider.Create())
                {
                    db.Abbreviations.Add(abbreviation);
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList(abbreviation.ContentTypeId);
            return this.View(abbreviation);
        }

        // GET: /Data/Resources/Abbreviations/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Abbreviation viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await db.Abbreviations.FindAsync(id);
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList(viewModel.ContentTypeId);
            return this.View(viewModel);
        }

        // POST: /Data/Resources/Abbreviations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = AbbreviationValidationBinding)] Abbreviation abbreviation)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.Entry(abbreviation).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList(abbreviation.ContentTypeId);
            return this.View(abbreviation);
        }

        // GET: /Data/Resources/Abbreviations/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Abbreviation viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await db.Abbreviations.FindAsync(id);
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/Abbreviations/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var db = this.contextProvider.Create())
            {
                var entity = await db.Abbreviations.FindAsync(id);
                db.Abbreviations.Remove(entity);
                await db.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        private async Task<SelectList> GetContentTypesSelectList()
        {
            IEnumerable<ContentType> items = null;

            using (var db = this.contextProvider.Create())
            {
                items = await db.ContentTypes.ToListAsync();
            }

            return new SelectList(items, nameof(ContentType.Id), nameof(ContentType.Name));
        }

        private async Task<SelectList> GetContentTypesSelectList(object selectedValue)
        {
            IEnumerable<ContentType> items = null;

            using (var db = this.contextProvider.Create())
            {
                items = await db.ContentTypes.ToListAsync();
            }

            return new SelectList(items, nameof(ContentType.Id), nameof(ContentType.Name), selectedValue);
        }
    }
}

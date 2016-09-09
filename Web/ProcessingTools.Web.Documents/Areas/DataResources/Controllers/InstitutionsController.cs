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

    public class InstitutionsController : Controller
    {
        private const string InstitutionValidationBinding = nameof(Institution.Id) + "," + nameof(Institution.Name);

        private readonly IDataResourcesDbContextProvider contextProvider;

        public InstitutionsController(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        // GET: /Data/Resources/Institutions
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Institution> viewModels = null;

            using (var db = this.contextProvider.Create())
            {
                viewModels = await db.Institutions.ToListAsync();
            }

            if (viewModels == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModels);
        }

        // GET: /Data/Resources/Institutions/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Institution viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.Institutions.Find(id));
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        // GET: /Data/Resources/Institutions/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: /Data/Resources/Institutions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = InstitutionValidationBinding)] Institution institution)
        {
            if (ModelState.IsValid)
            {
                institution.Id = Guid.NewGuid();

                using (var db = this.contextProvider.Create())
                {
                    db.Institutions.Add(institution);
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(institution);
        }

        // GET: /Data/Resources/Institutions/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Institution viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.Institutions.Find(id));
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/Institutions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = InstitutionValidationBinding)] Institution institution)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.Entry(institution).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(institution);
        }

        // GET: /Data/Resources/Institutions/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Institution viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                viewModel = await Task.FromResult(db.Institutions.Find(id));
            }

            if (viewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/Institutions/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var db = this.contextProvider.Create())
            {
                var entity = await Task.FromResult(db.Institutions.Find(id));
                db.Institutions.Remove(entity);
                await db.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

namespace ProcessingTools.Web.Documents.Areas.Journals.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using ProcessingTools.Documents.Data;
    using ProcessingTools.Documents.Data.Contracts;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Web.Common.Constants;

    using ViewModels.Publishers;

    public class PublishersController : Controller
    {
        private readonly DocumentsDbContext db;

        public PublishersController(IDocumentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.db = contextProvider.Create();
        }

        public static string ControllerName => ControllerConstants.PublishersControllerName;

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Journals/Publishers
        public async Task<ActionResult> Index()
        {
            var models = await this.db.Publishers
                .Select(e => new PublisherViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    AbbreviatedName = e.AbbreviatedName,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                })
                .ToListAsync();

            return this.View(models);
        }

        // GET: Journals/Publishers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = await this.db.Publishers
                .Include(p => p.Addresses)
                .Include(p => p.Journals)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }

            var model = await this.MapToDetailsViewModelWithoutCollections(entity);

            model.Addresses = entity.Addresses?.Select(a => new AddressViewModel
            {
                Id = a.Id,
                AddressString = a.AddressString
            }).ToList();

            model.Journals = entity.Journals?.Select(j => new JournalViewModel
            {
                Id = j.Id,
                Name = j.Name
            }).ToList();

            return this.View(model);
        }

        // GET: Journals/Publishers/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Journals/Publishers/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,AbbreviatedName")] PublisherCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var entity = new Publisher
                {
                    Name = model.Name,
                    AbbreviatedName = model.AbbreviatedName,
                    CreatedByUser = User.Identity.GetUserId(),
                    ModifiedByUser = User.Identity.GetUserId()
                };

                this.db.Publishers.Add(entity);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(model);
        }

        // GET: Journals/Publishers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = await Task.FromResult(this.db.Publishers.Find(id));
            if (entity == null)
            {
                return this.HttpNotFound();
            }

            return this.View(entity);
        }

        // POST: Journals/Publishers/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AbbreviatedName,CreatedByUserId,DateCreated,DateModified,ModifiedByUserId")] Publisher publisher)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(publisher).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = await Task.FromResult(this.db.Publishers.Find(id));
            if (entity == null)
            {
                return this.HttpNotFound();
            }

            var model = await this.MapToDetailsViewModelWithoutCollections(entity);

            return this.View(model);
        }

        // POST: Journals/Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var entity = await Task.FromResult(this.db.Publishers.Find(id));
            this.db.Publishers.Remove(entity);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task<PublisherDetailsViewModel> MapToDetailsViewModelWithoutCollections(Publisher entity)
        {
            string createdBy = (await this.UserManager.FindByIdAsync(entity.CreatedByUser)).UserName;
            string modifiedBy = (await this.UserManager.FindByIdAsync(entity.ModifiedByUser)).UserName;

            var model = new PublisherDetailsViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                AbbreviatedName = entity.AbbreviatedName,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
                CreatedBy = createdBy,
                ModifiedBy = modifiedBy
            };

            return model;
        }
    }
}

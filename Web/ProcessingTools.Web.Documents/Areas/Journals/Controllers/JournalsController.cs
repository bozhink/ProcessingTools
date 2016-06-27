namespace ProcessingTools.Web.Documents.Areas.Journals.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Documents.Data;
    using ProcessingTools.Documents.Data.Contracts;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Web.Common.Constants;

    public class JournalsController : Controller
    {
        private readonly DocumentsDbContext db;

        public JournalsController(IDocumentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.db = contextProvider.Create();
        }

        public static string ControllerName => ControllerConstants.JournalsControllerName;

        // GET: Journals/Journals
        public async Task<ActionResult> Index()
        {
            var journals = this.db.Journals.Include(j => j.Publisher);
            return this.View(await journals.ToListAsync());
        }

        // GET: Journals/Journals/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var journal = await Task.FromResult(this.db.Journals.Find(id));
            if (journal == null)
            {
                return this.HttpNotFound();
            }

            return this.View(journal);
        }

        // GET: Journals/Journals/Create
        public ActionResult Create()
        {
            this.ViewBag.PublisherId = new SelectList(this.db.Publishers, "Id", "Name");
            return this.View();
        }

        // POST: Journals/Journals/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,AbbreviatedName,JournalId,PrintIssn,ElectronicIssn,PublisherId,CreatedByUserId,DateCreated,DateModified,ModifiedByUserId")] Journal journal)
        {
            if (this.ModelState.IsValid)
            {
                journal.Id = Guid.NewGuid();
                this.db.Journals.Add(journal);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.PublisherId = new SelectList(this.db.Publishers, "Id", "Name", journal.PublisherId);
            return this.View(journal);
        }

        // GET: Journals/Journals/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var journal = await Task.FromResult(this.db.Journals.Find(id));
            if (journal == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.PublisherId = new SelectList(this.db.Publishers, "Id", "Name", journal.PublisherId);
            return this.View(journal);
        }

        // POST: Journals/Journals/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AbbreviatedName,JournalId,PrintIssn,ElectronicIssn,PublisherId,CreatedByUserId,DateCreated,DateModified,ModifiedByUserId")] Journal journal)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(journal).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.PublisherId = new SelectList(this.db.Publishers, "Id", "Name", journal.PublisherId);
            return this.View(journal);
        }

        // GET: Journals/Journals/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var journal = await Task.FromResult(this.db.Journals.Find(id));
            if (journal == null)
            {
                return this.HttpNotFound();
            }

            return this.View(journal);
        }

        // POST: Journals/Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var journal = await Task.FromResult(this.db.Journals.Find(id));
            this.db.Journals.Remove(journal);
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
    }
}

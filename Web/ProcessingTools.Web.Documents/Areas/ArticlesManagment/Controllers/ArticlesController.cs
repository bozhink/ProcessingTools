namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Documents.Data;
    using ProcessingTools.Documents.Data.Contracts;
    using ProcessingTools.Documents.Data.Models;

    public class ArticlesController : Controller
    {
        private readonly DocumentsDbContext db;

        public ArticlesController(IDocumentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.db = contextProvider.Create();
        }

        // GET: ArticlesManagment/Articles
        public async Task<ActionResult> Index()
        {
            var articles = this.db.Articles.Include(a => a.Journal);
            return this.View(await articles.ToListAsync());
        }

        // GET: ArticlesManagment/Articles/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = await Task.FromResult(this.db.Articles.Find(id));
            if (article == null)
            {
                return this.HttpNotFound();
            }

            return this.View(article);
        }

        // GET: ArticlesManagment/Articles/Create
        public ActionResult Create()
        {
            this.ViewBag.JournalId = new SelectList(this.db.Journals, "Id", "Name");
            return this.View();
        }

        // POST: ArticlesManagment/Articles/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,DateReceived,DateAccepted,DatePublished,Volume,Issue,FirstPage,LastPage,ELocationId,JournalId,CreatedByUserId,DateCreated,DateModified,ModifiedByUserId")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Id = Guid.NewGuid();
                this.db.Articles.Add(article);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.JournalId = new SelectList(this.db.Journals, "Id", "Name", article.JournalId);
            return this.View(article);
        }

        // GET: ArticlesManagment/Articles/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = await Task.FromResult(this.db.Articles.Find(id));
            if (article == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.JournalId = new SelectList(this.db.Journals, "Id", "Name", article.JournalId);
            return this.View(article);
        }

        // POST: ArticlesManagment/Articles/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,DateReceived,DateAccepted,DatePublished,Volume,Issue,FirstPage,LastPage,ELocationId,JournalId,CreatedByUserId,DateCreated,DateModified,ModifiedByUserId")] Article article)
        {
            if (ModelState.IsValid)
            {
                this.db.Entry(article).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.JournalId = new SelectList(this.db.Journals, "Id", "Name", article.JournalId);
            return this.View(article);
        }

        // GET: ArticlesManagment/Articles/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = await Task.FromResult(this.db.Articles.Find(id));
            if (article == null)
            {
                return this.HttpNotFound();
            }

            return this.View(article);
        }

        // POST: ArticlesManagment/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var article = await Task.FromResult(this.db.Articles.Find(id));
            this.db.Articles.Remove(article);
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

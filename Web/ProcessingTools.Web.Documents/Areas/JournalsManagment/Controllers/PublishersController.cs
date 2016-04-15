namespace ProcessingTools.Web.Documents.Areas.JournalsManagment.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Documents.Data.Repositories;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Documents.Services.Data;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Extensions;

    [Authorize]
    public class PublishersController : Controller
    {
        private readonly IPublishersDataService service;

        public PublishersController()
        {
            IDocumentsDbContextProvider contextProvider = new DocumentsDbContextProvider();
            IDocumentsRepositoryProvider<Publisher> repositoryProvider = new DocumentsRepositoryProvider<Publisher>(contextProvider);
            this.service = new PublishersDataService(repositoryProvider);
        }

        // GET: Publishers
        public async Task<ActionResult> Index()
        {
            var items = (await this.service.All()).ToList();

            return this.View(items);
        }

        // GET: Publishers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var publisher = (await this.service.Get(id)).FirstOrDefault();
                if (publisher == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(publisher);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,AbbreviatedName")] PublisherServiceModel publisher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    publisher.Id = Guid.NewGuid();
                    publisher.CreatedByUserId = this.GetUserId();
                    publisher.ModifiedByUserId = publisher.CreatedByUserId;
                    publisher.DateCreated = DateTime.UtcNow;
                    publisher.DateModified = publisher.DateCreated;

                    await this.service.Add(publisher);

                    return this.RedirectToAction(nameof(this.Index));
                }

                return this.View(publisher);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Publishers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var publisher = (await this.service.Get(id)).FirstOrDefault();
                if (publisher == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(publisher);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AbbreviatedName")] PublisherServiceModel publisher)
        {
            try
            {
                var entity = (await this.service.Get(publisher.Id)).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    entity.Name = publisher.Name;
                    entity.AbbreviatedName = publisher.AbbreviatedName;
                    entity.ModifiedByUserId = this.GetUserId();
                    entity.DateModified = DateTime.UtcNow;

                    await this.service.Update(entity);

                    return this.RedirectToAction(nameof(this.Index));
                }

                return this.View(publisher);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Publishers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var publisher = (await this.service.Get(id)).FirstOrDefault();
                if (publisher == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(publisher);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await this.service.Delete(id);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.TryDispose();
            }

            base.Dispose(disposing);
        }

        private string GetUserId()
        {
            int lengthOfGuid = Guid.NewGuid().ToString().Length;
            string name = User.Identity.Name;

            return name.Substring(0, Math.Min(name.Length, lengthOfGuid));
        }
    }
}

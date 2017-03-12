namespace ProcessingTools.Web.Areas.Journals.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Abstractions.Controllers;
    using ProcessingTools.Journals.Data.Common.Contracts.Models;
    using ProcessingTools.Journals.Data.Entity.Contracts;
    using ProcessingTools.Journals.Data.Entity.Models;
    using ProcessingTools.Journals.Data.Common.Contracts.Repositories;
    using ViewModels.Publishers;

    [Authorize]
    public class PublishersController : BaseMvcController
    {
        public const string ControllerName = "Publishers";
        public const string CreateActionName = "Create";
        public const string DeleteActionName = "Delete";
        public const string DetailsActionName = "Details";
        public const string EditActionName = "Edit";

        private readonly IPublishersRepository repository;

        public PublishersController(IPublishersRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        private Func<IPublisher, PublisherViewModel> MapModelToViewModel => p => new PublisherViewModel
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUser = p.CreatedByUser,
            ModifiedByUser = p.ModifiedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified,
            Addresses = p.Addresses.ToList<IAddress>()
        };

        private Func<PublisherViewModel, Publisher> MapViewModelToModel => p => new Publisher
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUser = p.CreatedByUser,
            ModifiedByUser = p.ModifiedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified
        };

        // GET: Journals/Publishers
        [HttpGet, ActionName(IndexActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var data = await this.repository.Query.ToListAsync();

            var viewModel = data.Select(this.MapModelToViewModel);

            return this.View(viewModel);
        }

        // GET: Journals/Publishers/Details/5
        [HttpGet, ActionName(DetailsActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = await this.repository.GetById(id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(data);

            return this.View(viewModel);
        }

        // GET: Journals/Publishers/Create
        [HttpGet, ActionName(CreateActionName)]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Journals/Publishers/Create
        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AbbreviatedName,Name")] PublisherViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.CreatedByUser = this.UserId;
                model.ModifiedByUser = model.CreatedByUser;
                model.DateCreated = DateTime.UtcNow;
                model.DateModified = model.DateCreated;

                var entity = this.MapViewModelToModel(model);

                await this.repository.Add(entity);
                await this.repository.SaveChanges();

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(model);
        }

        // GET: Journals/Publishers/Edit/5
        [HttpGet, ActionName(EditActionName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = await this.repository.GetById(id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(data);

            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AbbreviatedName,Name,DateCreated,CreatedByUser")] PublisherViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.ModifiedByUser = this.UserId;
                model.DateModified = DateTime.UtcNow;

                var entity = this.MapViewModelToModel(model);

                await this.repository.Update(entity);
                await this.repository.SaveChanges();

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(model);
        }

        // GET: Journals/Publishers/Delete/5
        [HttpGet, ActionName(DeleteActionName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = await this.repository.GetById(id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(data);

            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Delete/5
        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await this.repository.Delete(id);
            await this.repository.SaveChanges();

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

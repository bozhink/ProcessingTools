namespace ProcessingTools.Web.Areas.Journals.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Abstractions.Controllers;
    using Models.Publishers;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;
    using ProcessingTools.Journals.Services.Data.Contracts.Services;
    using ViewModels.Publishers;

    [Authorize]
    public class PublishersController : BaseMvcController
    {
        public const string ControllerName = "Publishers";
        public const string CreateActionName = "Create";
        public const string DeleteActionName = "Delete";
        public const string DetailsActionName = "Details";
        public const string EditActionName = "Edit";

        private readonly IPublishersDataService service;

        public PublishersController(IPublishersDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
            this.service.SaveToHistory = true;
        }

        private Func<IPublisher, PublisherViewModel> MapModelToViewModel => p => new PublisherViewModel
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName
        };

        private Func<IPublisherDetails, PublisherViewModel> MapDetailedModelToViewModel => p => new PublisherViewModel
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUser = p.CreatedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified,
            ModifiedByUser = p.ModifiedByUser,
            Addresses = p.Addresses
                .Select(a => new AddressViewModel
                {
                    Id = a.Id,
                    AddressString = a.AddressString,
                    CityId = a.CityId,
                    CountryId = a.CountryId
                })
            .ToList()
        };

        private Func<PublisherViewModel, Publisher> MapViewModelToModel => p => new Publisher
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName
        };

        // GET: Journals/Publishers
        [HttpGet, ActionName(IndexActionName)]
        [AllowAnonymous]
        public async Task<ActionResult> Index(int p = 0, int n = 10)
        {
            var data = await this.service.Select(this.UserId, p * n, n, x => x.Name);

            var viewModel = await data.Select(this.MapModelToViewModel).ToListAsync();

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

            var data = await this.service.GetDetails(this.UserId, id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapDetailedModelToViewModel(data);

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
        public async Task<ActionResult> Create([Bind(Include = "AbbreviatedName,Name")] Publisher model)
        {
            if (this.ModelState.IsValid)
            {
                await this.service.Add(this.UserId, model);

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(this.MapModelToViewModel(model));
        }

        // GET: Journals/Publishers/Edit/5
        [HttpGet, ActionName(EditActionName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = await this.service.GetDetails(this.UserId, id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapDetailedModelToViewModel(data);

            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AbbreviatedName,Name")] Publisher model)
        {
            if (this.ModelState.IsValid)
            {
                await this.service.Update(this.UserId, model);

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(this.MapModelToViewModel(model));
        }

        // GET: Journals/Publishers/Delete/5
        [HttpGet, ActionName(DeleteActionName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = await this.service.GetDetails(this.UserId, id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapDetailedModelToViewModel(data);

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

            await this.service.Delete(this.UserId, id);

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

namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Web.Areas.Data.Models.GeoNames;
    using ProcessingTools.Web.Areas.Data.ViewModels.GeoNames;

    public class GeoNamesController : Controller
    {
        public const string ControllerName = "GeoNames";
        public const string IndexActionName = "Index";
        public const string DetailsActionName = "Details";
        public const string CreateActionName = "Create";
        public const string EditActionName = "Edit";
        public const string DeleteActionName = "Delete";

        private readonly IGeoNamesDataService service;

        public GeoNamesController(IGeoNamesDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        // GET: Data/GeoNames
        [HttpGet, ActionName(IndexActionName)]
        public async Task<ActionResult> Index()
        {
            var data = await this.service.SelectAsync(null);

            var viewModel = data.Select(this.MapModelToViewModel)
            .ToArray();

            return this.View(IndexActionName, viewModel);
        }

        private Func<IGeoName, GeoNameViewModel> MapModelToViewModel => d => new GeoNameViewModel
        {
            Id = d.Id,
            Name = d.Name
        };

        // GET: Data/GeoNames/Details/5
        [HttpGet, ActionName(DetailsActionName)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(model);

            return this.View(DetailsActionName, viewModel);
        }

        // GET: Data/GeoNames/Create
        [HttpGet, ActionName(CreateActionName)]
        public ActionResult Create()
        {
            return this.View(CreateActionName);
        }

        // POST: Data/GeoNames/Create
        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] GeoNameRequestModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.service.InsertAsync(model);
                await this.service.SaveChangesAsync();

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(CreateActionName, model);
        }

        // GET: Data/GeoNames/Edit/5
        [HttpGet, ActionName(EditActionName)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(model);

            return this.View(EditActionName, viewModel);
        }

        // POST: Data/GeoNames/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] GeoNameRequestModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.service.UpdateAsync(model);
                await this.service.SaveChangesAsync();

                return this.RedirectToAction(IndexActionName);
            }

            return this.View(EditActionName, model);
        }

        // GET: Data/GeoNames/Delete/5
        [HttpGet, ActionName(DeleteActionName)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(model);

            return this.View(DeleteActionName, viewModel);
        }

        // POST: Data/GeoNames/Delete/5
        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.service.DeleteAsync(id: id);
            await this.service.SaveChangesAsync();

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

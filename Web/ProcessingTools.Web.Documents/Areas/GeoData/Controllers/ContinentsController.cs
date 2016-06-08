namespace ProcessingTools.Web.Documents.Areas.GeoData.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ViewModels.Continents;
    using ViewModels.Countries;

    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;
    using ProcessingTools.Web.Common.Constants;

    [Authorize]
    public class ContinentsController : Controller
    {
        private readonly IContinentsDataService service;

        public ContinentsController(IContinentsDataService service)
        {
            if (service == null)
            {
                throw new ArgumentException(nameof(service));
            }

            this.service = service;
        }

        public static string ControllerName => ControllerConstants.ContinentsControllerName;

        // GET: GeoData/Continents/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: GeoData/Continents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] DetailedContinentViewModel continent)
        {
            if (ModelState.IsValid)
            {
                await this.service.Add(this.MapDetailedViewModelToServiceModel(continent));
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(continent);
        }

        // GET: GeoData/Continents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var continent = await this.service.Get(id);
            if (continent == null)
            {
                return this.HttpNotFound();
            }

            return this.View(this.MapServiceModelToViewModel(continent));
        }

        // POST: GeoData/Continents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.service.Delete(id: id);
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: GeoData/Continents/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var continent = await this.service.Get(id);
            if (continent == null)
            {
                return this.HttpNotFound();
            }

            return this.View(this.MapServiceModelToDetailedViewModel(continent));
        }

        // GET: GeoData/Continents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var continent = await this.service.Get(id);
            if (continent == null)
            {
                return this.HttpNotFound();
            }

            return this.View(this.MapServiceModelToDetailedViewModel(continent));
        }

        // POST: GeoData/Continents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] DetailedContinentViewModel continent)
        {
            if (ModelState.IsValid)
            {
                await this.service.Update(this.MapDetailedViewModelToServiceModel(continent));
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(continent);
        }

        // GET: GeoData/Continents
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var items = (await this.service.All())
                .Select(this.MapServiceModelToViewModel)
                .ToList();

            return this.View(items);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewSynonym(ContinentSynonymViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.service.AddSynonym(model.ContinentId, new ContinentSynonymServiceModel
                {
                    Name = model.Name
                });
            }

            return this.RedirectToAction(nameof(this.Edit), new { id = model.ContinentId });
        }

        [HttpPost]
        public async Task<ActionResult> RemoveSynonym(ContinentSynonymViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.service.RemoveSynonym(model.ContinentId, new ContinentSynonymServiceModel
                {
                    Name = model.Name
                });
            }

            return this.RedirectToAction(nameof(this.Edit), new { id = model.ContinentId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.TryDispose();
            }

            base.Dispose(disposing);
        }

        private ContinentServiceModel MapDetailedViewModelToServiceModel(DetailedContinentViewModel continent)
        {
            var synonyms = continent.Synonyms
                .Select(s => new ContinentSynonymServiceModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();

            var countries = continent.Countries
                .Select(cs => new CountryServiceModel
                {
                    Id = cs.Id,
                    Name = cs.Name
                })
                .ToList();

            return new ContinentServiceModel
            {
                Id = continent.Id,
                Name = continent.Name,
                Synonyms = synonyms,
                Countries = countries
            };
        }

        private DetailedContinentViewModel MapServiceModelToDetailedViewModel(ContinentServiceModel continent)
        {
            var synonyms = continent.Synonyms
                .Select(s => new ContinentSynonymViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();

            var countries = continent.Countries
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            return new DetailedContinentViewModel
            {
                Id = continent.Id,
                Name = continent.Name,
                Synonyms = synonyms,
                Countries = countries
            };
        }

        private ContinentViewModel MapServiceModelToViewModel(ContinentServiceModel continent)
        {
            return new ContinentViewModel
            {
                Id = continent.Id,
                Name = continent.Name
            };
        }
    }
}

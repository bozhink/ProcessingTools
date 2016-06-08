﻿namespace ProcessingTools.Web.Documents.Areas.GeoData
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;

    [Authorize]
    public class ContinentsController : Controller
    {
        private readonly IGeoDataRepository<Continent> repository;

        public ContinentsController(IGeoDataRepository<Continent> repository)
        {
            if (repository == null)
            {
                throw new ArgumentException(nameof(repository));
            }

            this.repository = repository;
        }

        // GET: GeoData/Continents
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var items = (await this.repository.All()).ToList();
            return this.View(items);
        }

        // GET: GeoData/Continents/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var continent = await this.repository.Get(id);
            if (continent == null)
            {
                return this.HttpNotFound();
            }

            return this.View(continent);
        }

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
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                await this.repository.Add(continent);
                await this.repository.SaveChanges();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(continent);
        }

        // GET: GeoData/Continents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var continent = await this.repository.Get(id);
            if (continent == null)
            {
                return this.HttpNotFound();
            }

            return this.View(continent);
        }

        // POST: GeoData/Continents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                await this.repository.Update(continent);
                await this.repository.SaveChanges();
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

            var continent = await this.repository.Get(id);
            if (continent == null)
            {
                return this.HttpNotFound();
            }

            return this.View(continent);
        }

        // POST: GeoData/Continents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.repository.Delete(id: id);
            await this.repository.SaveChanges();
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repository.TryDispose();
            }

            base.Dispose(disposing);
        }
    }
}

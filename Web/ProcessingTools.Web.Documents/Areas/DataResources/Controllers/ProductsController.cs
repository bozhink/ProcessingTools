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

    public class ProductsController : Controller
    {
        private const string ProductValidationBinding = nameof(Product.Id) + "," + nameof(Product.Name);

        private readonly IDataResourcesDbContextProvider contextProvider;

        public ProductsController(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        // GET: /Data/Resources/Products
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Product> viewModels = null;

            using (var db = this.contextProvider.Create())
            {
                viewModels = await db.Products.ToListAsync();
            }

            if (viewModels == null)
            {
                return this.HttpNotFound();
            }

            return this.View(viewModels);
        }

        // GET: /Data/Resources/Products/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = null;

            using (var db = this.contextProvider.Create())
            {
                product = await Task.FromResult(db.Products.Find(id));
            }

            if (product == null)
            {
                return this.HttpNotFound();
            }

            return this.View(product);
        }

        // GET: /Data/Resources/Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: /Data/Resources/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = ProductValidationBinding)] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                using (var db = this.contextProvider.Create())
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(product);
        }

        // GET: /Data/Resources/Products/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = null;

            using (var db = this.contextProvider.Create())
            {
                product = await Task.FromResult(db.Products.Find(id));
            }

            if (product == null)
            {
                return this.HttpNotFound();
            }

            return this.View(product);
        }

        // POST: /Data/Resources/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = ProductValidationBinding)] Product product)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(product);
        }

        // GET: /Data/Resources/Products/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = null;

            using (var db = this.contextProvider.Create())
            {
                product = await Task.FromResult(db.Products.Find(id));
            }

            if (product == null)
            {
                return this.HttpNotFound();
            }

            return this.View(product);
        }

        // POST: /Data/Resources/Products/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var db = this.contextProvider.Create())
            {
                var entity = await Task.FromResult(db.Products.Find(id));
                db.Products.Remove(entity);
                await db.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

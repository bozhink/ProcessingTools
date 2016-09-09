namespace ProcessingTools.Web.Documents.Areas.DataResources.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Extensions;

    public class AbbreviationsController : Controller
    {
        private const string AbbreviationValidationBinding = nameof(Abbreviation.Id) + "," +
            nameof(Abbreviation.Name) + "," +
            nameof(Abbreviation.Definition) + "," +
            nameof(Abbreviation.ContentTypeId);

        private readonly IDataResourcesDbContextProvider contextProvider;

        public AbbreviationsController(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        // GET: /Data/Resources/Abbreviations
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Abbreviation> viewModels = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.Abbreviations.Include(a => a.ContentType);
                viewModels = await query.ToListAsync();
            }

            if (viewModels == null)
            {
                throw new EntityNotFoundException();
            }

            return this.View(viewModels);
        }

        // GET: /Data/Resources/Abbreviations/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Abbreviation viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.Abbreviations.Include(a => a.ContentType);
                viewModel = await query.FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());
            }

            if (viewModel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.View(viewModel);
        }

        // GET: /Data/Resources/Abbreviations/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList();
            return this.View();
        }

        // POST: /Data/Resources/Abbreviations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = AbbreviationValidationBinding)] Abbreviation model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();

                using (var db = this.contextProvider.Create())
                {
                    db.Abbreviations.Add(model);
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList(model.ContentTypeId);
            return this.View(model);
        }

        // GET: /Data/Resources/Abbreviations/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Abbreviation viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.Abbreviations.Include(a => a.ContentType);
                viewModel = await query.FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());
            }

            if (viewModel == null)
            {
                throw new EntityNotFoundException();
            }

            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList(viewModel.ContentTypeId);
            return this.View(viewModel);
        }

        // POST: /Data/Resources/Abbreviations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = AbbreviationValidationBinding)] Abbreviation model)
        {
            if (ModelState.IsValid)
            {
                using (var db = this.contextProvider.Create())
                {
                    db.Entry(model).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.ContentTypeId = await this.GetContentTypesSelectList(model.ContentTypeId);
            return this.View(model);
        }

        // GET: /Data/Resources/Abbreviations/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Abbreviation viewModel = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.Abbreviations.Include(a => a.ContentType);
                viewModel = await query.FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString());
            }

            if (viewModel == null)
            {
                throw new EntityNotFoundException();
            }

            return this.View(viewModel);
        }

        // POST: /Data/Resources/Abbreviations/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var db = this.contextProvider.Create())
            {
                var entity = await Task.FromResult(db.Abbreviations.Find(id));
                db.Abbreviations.Remove(entity);
                await db.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName).ExecuteResult(this.ControllerContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.DefaultNotFoundView(
                    InstanceNames.AbbreviationsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidIdException)
            {
                filterContext.Result = this.InvalidIdErrorView(
                    InstanceNames.AbbreviationsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    InstanceNames.AbbreviationsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    InstanceNames.AbbreviationsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    InstanceNames.AbbreviationsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    InstanceNames.AbbreviationsControllerInstanceName,
                    filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;
        }

        private async Task<SelectList> GetContentTypesSelectList()
        {
            IEnumerable<ContentType> items = null;

            using (var db = this.contextProvider.Create())
            {
                items = await db.ContentTypes.ToListAsync();
            }

            return new SelectList(items, nameof(ContentType.Id), nameof(ContentType.Name));
        }

        private async Task<SelectList> GetContentTypesSelectList(object selectedValue)
        {
            IEnumerable<ContentType> items = null;

            using (var db = this.contextProvider.Create())
            {
                items = await db.ContentTypes.ToListAsync();
            }

            return new SelectList(items, nameof(ContentType.Id), nameof(ContentType.Name), selectedValue);
        }
    }
}

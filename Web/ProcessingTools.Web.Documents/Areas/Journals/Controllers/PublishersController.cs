namespace ProcessingTools.Web.Documents.Areas.Journals.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models.Publishers;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Common.Constants;

    using ViewModels.Publishers;

    public class PublishersController : Controller
    {
        private readonly IPublishersDataService service;

        public PublishersController(IPublishersDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public static string ControllerName => ControllerConstants.PublishersControllerName;

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Journals/Publishers
        public async Task<ActionResult> Index()
        {
            // TODO: paging
            try
            {
                var viewModels = (await this.service.All(0, 15))
                    .Select(e => new PublisherViewModel
                    {
                        Id = e.Id,
                        Name = e.Name,
                        AbbreviatedName = e.AbbreviatedName,
                        DateCreated = e.DateCreated,
                        DateModified = e.DateModified
                    })
                    .ToList();

                return this.View(viewModels);
            }
            catch (InvalidPageNumberException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (InvalidItemsPerPageException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Index));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Journals/Publishers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var serviceModel = await this.service.GetDetails(id);
                var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);

                viewModel.Addresses = serviceModel.Addresses?.Select(a => new AddressViewModel
                {
                    Id = a.Id,
                    AddressString = a.AddressString
                }).ToList();

                viewModel.Journals = serviceModel.Journals?.Select(j => new JournalViewModel
                {
                    Id = j.Id,
                    Name = j.Name
                }).ToList();

                return this.View(viewModel);
            }
            catch (EntityNotFoundException)
            {
                return this.HttpNotFound();
            }
            catch (ArgumentNullException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Details));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Journals/Publishers/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Journals/Publishers/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,AbbreviatedName")] PublisherCreateViewModel publisher)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.service.Add(
                        User.Identity.GetUserId(),
                        new PublisherMinimalServiceModel
                        {
                            Name = publisher.Name,
                            AbbreviatedName = publisher.AbbreviatedName
                        });

                    return this.RedirectToAction(nameof(this.Index));
                }
                catch (ArgumentNullException)
                {
                    // TODO
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                catch (Exception e)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                    return this.View(ViewConstants.DefaultErrorViewName, error);
                }
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var serviceModel = await this.service.GetDetails(id);
                var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
                return this.View(viewModel);
            }
            catch (EntityNotFoundException)
            {
                return this.HttpNotFound();
            }
            catch (ArgumentNullException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Delete));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // POST: Journals/Publishers/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AbbreviatedName")] PublisherViewModel publisher)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.service.Update(
                            User.Identity.GetUserId(),
                            new PublisherMinimalServiceModel
                            {
                                Id = publisher.Id,
                                Name = publisher.Name,
                                AbbreviatedName = publisher.AbbreviatedName
                            });

                    return this.RedirectToAction(nameof(this.Index));
                }

                return this.View(publisher);
            }
            catch (EntityNotFoundException)
            {
                return this.HttpNotFound();
            }
            catch (ArgumentNullException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Delete));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Journals/Publishers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var serviceModel = await this.service.GetDetails(id);
                var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
                return this.View(viewModel);
            }
            catch (EntityNotFoundException)
            {
                return this.HttpNotFound();
            }
            catch (ArgumentNullException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Delete));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // POST: Journals/Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await this.service.Delete(id);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (EntityNotFoundException)
            {
                return this.HttpNotFound();
            }
            catch (ArgumentNullException)
            {
                // TODO
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Delete));
                return this.View(ViewConstants.DefaultErrorViewName, error);
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

        private async Task<PublisherDetailsViewModel> MapToDetailsViewModelWithoutCollections(PublisherServiceModel serviceModel)
        {
            string createdBy = (await this.UserManager.FindByIdAsync(serviceModel.CreatedByUser)).UserName;
            string modifiedBy = (await this.UserManager.FindByIdAsync(serviceModel.ModifiedByUser)).UserName;

            var model = new PublisherDetailsViewModel
            {
                Id = serviceModel.Id,
                Name = serviceModel.Name,
                AbbreviatedName = serviceModel.AbbreviatedName,
                DateCreated = serviceModel.DateCreated,
                DateModified = serviceModel.DateModified,
                CreatedBy = createdBy,
                ModifiedBy = modifiedBy
            };

            return model;
        }
    }
}

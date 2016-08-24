﻿namespace ProcessingTools.Web.Documents.Areas.Journals.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models.Publishers;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers;
    using ProcessingTools.Web.Documents.Extensions;

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

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Journals/Publishers
        public async Task<ActionResult> Index(int? p, int? n)
        {
            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            var items = (await this.service.All(currentPage, numberOfItemsPerPage))
                .Select(e => new PublisherViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    AbbreviatedName = e.AbbreviatedName,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                })
                .ToArray();

            var numberOfDocuments = await this.service.Count();

            var viewModel = new ListWithPagingViewModel<PublisherViewModel>(nameof(this.Index), numberOfDocuments, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: Journals/Publishers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var serviceModel = await this.service.GetDetails(id);
            var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);

            viewModel.Addresses = serviceModel.Addresses?.Select(a => new AddressViewModel
            {
                Id = a.Id,
                AddressString = a.AddressString
            }).ToList();

            return this.View(viewModel);
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
                await this.service.Add(
                    User.Identity.GetUserId(),
                    new PublisherMinimalServiceModel
                    {
                        Name = publisher.Name,
                        AbbreviatedName = publisher.AbbreviatedName
                    });

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var serviceModel = await this.service.GetDetails(id);
            var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Edit/5
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AbbreviatedName")] PublisherViewModel publisher)
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

        // GET: Journals/Publishers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var serviceModel = await this.service.GetDetails(id);
            var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await this.service.Delete(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.TryDispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.DefaultNotFoundView(
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidIdException)
            {
                filterContext.Result = this.InvalidIdErrorView(
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;
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

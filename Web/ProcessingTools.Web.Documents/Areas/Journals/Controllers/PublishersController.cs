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
            try
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
            catch (InvalidPageNumberException e)
            {
                return this.InvalidPageNumberErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultBackToListActionLinkTitle);
            }
            catch (InvalidItemsPerPageException e)
            {
                return this.InvalidNumberOfItemsPerPageErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultBackToListActionLinkTitle);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultIndexActionLinkTitle);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultIndexActionLinkTitle);
            }
        }

        // GET: Journals/Publishers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return this.InvalidIdErrorView(InstanceNames.PublishersControllerInstanceName, string.Empty, Resources.Strings.DefaultDetailsActionLinkTitle);
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
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultDetailsActionLinkTitle);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultDetailsActionLinkTitle);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultDetailsActionLinkTitle);
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
                catch (ArgumentException e)
                {
                    return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultCreateActionLinkTitle);
                }
                catch (Exception e)
                {
                    return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, Resources.Strings.DefaultCreateActionLinkTitle);
                }
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return this.InvalidIdErrorView(InstanceNames.PublishersControllerInstanceName, string.Empty, ContentConstants.DefaultEditActionLinkTitle);
            }

            try
            {
                var serviceModel = await this.service.GetDetails(id);
                var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
                return this.View(viewModel);
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultEditActionLinkTitle);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultEditActionLinkTitle);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultEditActionLinkTitle);
            }
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
                try
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
                catch (EntityNotFoundException e)
                {
                    return this.DefaultNotFoundView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultEditActionLinkTitle);
                }
                catch (ArgumentException e)
                {
                    return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultEditActionLinkTitle);
                }
                catch (Exception e)
                {
                    return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultEditActionLinkTitle);
                }
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.InvalidIdErrorView(InstanceNames.PublishersControllerInstanceName, string.Empty, ContentConstants.DefaultDeleteActionLinkTitle);
            }

            try
            {
                var serviceModel = await this.service.GetDetails(id);
                var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
                return this.View(viewModel);
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle);
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
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceNames.PublishersControllerInstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle);
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

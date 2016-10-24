namespace ProcessingTools.Web.Documents.Areas.Journals.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Constants;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models.Publishers;
    using ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers;
    using ProcessingTools.Web.Documents.Extensions;

    public class PublishersController : Controller
    {
        private const string BindingsIncludedForCreateAction = nameof(PublisherCreateViewModel.Name) + "," + nameof(PublisherCreateViewModel.AbbreviatedName);
        private const string BindingsIncludedForEditAction = nameof(PublisherViewModel.Id) + "," + nameof(PublisherViewModel.Name) + "," + nameof(PublisherViewModel.AbbreviatedName);

        private readonly IPublishersDataService publishersDataService;
        private readonly ICountriesDataService countriesDataService;
        private readonly ICitiesDataService citiesDataService;

        public PublishersController(
            IPublishersDataService publishersDataService,
            ICountriesDataService countriesDataService,
            ICitiesDataService citiesDataService)
        {
            if (publishersDataService == null)
            {
                throw new ArgumentNullException(nameof(publishersDataService));
            }

            if (countriesDataService == null)
            {
                throw new ArgumentNullException(nameof(countriesDataService));
            }

            if (citiesDataService == null)
            {
                throw new ArgumentNullException(nameof(citiesDataService));
            }

            this.publishersDataService = publishersDataService;
            this.countriesDataService = countriesDataService;
            this.citiesDataService = citiesDataService;
        }

        private object UserId => User.Identity.GetUserId();

        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }

        // GET: Journals/Publishers
        [HttpGet]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            var items = (await this.publishersDataService.All(currentPage, numberOfItemsPerPage))
                .Select(e => new PublisherViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    AbbreviatedName = e.AbbreviatedName,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                })
                .ToArray();

            var numberOfDocuments = await this.publishersDataService.Count();

            var viewModel = new ListWithPagingViewModel<PublisherViewModel>(nameof(this.Index), numberOfDocuments, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: Journals/Publishers/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var serviceModel = await this.publishersDataService.GetDetails(id);
            var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);

            viewModel.Addresses = serviceModel.Addresses?.Select(a => new AddressViewModel
            {
                Id = a.Id,
                AddressString = a.AddressString
            }).ToList();

            return this.View(viewModel);
        }

        // GET: Journals/Publishers/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var countries = (await this.countriesDataService.All())
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            var cities = (await this.citiesDataService.All())
                .Select(c => new CityViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            var model = new PublisherCreateViewModel
            {
                Cities = new SelectList(cities, nameof(CityViewModel.Id), nameof(CityViewModel.Name)),
                Countries = new SelectList(countries, nameof(CountryViewModel.Id), nameof(CountryViewModel.Name))
            };

            return this.View(model);
        }

        // POST: Journals/Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(/*[Bind(Include = BindingsIncludedForCreateAction)]*/ PublisherCreateViewModel publisher)
        {
            if (this.ModelState.IsValid)
            {
                // TODO: addresses
                var userId = this.UserId;
                var serviceModel = new PublisherAddableServiceModel
                {
                    Name = publisher.Name,
                    AbbreviatedName = publisher.AbbreviatedName
                };

                foreach (var address in publisher.Addresses)
                {
                    serviceModel.Addresses.Add(new PublisherAddress
                    {
                        Id = address.Id,
                        AddressString = address.AddressString,
                        CityId = address.CityId,
                        CountryId = address.CountryId
                    });
                }

                await this.publishersDataService.Add(userId, serviceModel);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var serviceModel = await this.publishersDataService.GetDetails(id);
            var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = BindingsIncludedForEditAction)] PublisherViewModel publisher)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.UserId;
                var serviceModel = new PublisherUpdatableServiceModel
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    AbbreviatedName = publisher.AbbreviatedName
                };

                await this.publishersDataService.Update(userId, serviceModel);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(publisher);
        }

        // GET: Journals/Publishers/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var serviceModel = await this.publishersDataService.GetDetails(id);
            var viewModel = await this.MapToDetailsViewModelWithoutCollections(serviceModel);
            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Delete/5
        [HttpPost, ActionName(ActionNames.DeafultDeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await this.publishersDataService.Delete(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.publishersDataService.TryDispose();
            }

            base.Dispose(disposing);
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
                    InstanceNames.PublishersControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidUserIdException)
            {
                filterContext.Result = this.InvalidUserIdErrorView(
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

        private async Task<PublisherDetailsViewModel> MapToDetailsViewModelWithoutCollections(IPublisherSimpleServiceModel serviceModel)
        {
            string createdByUserName = await this.GetUserNameByUserId(serviceModel.CreatedByUser);
            string modifiedByUserName = await this.GetUserNameByUserId(serviceModel.ModifiedByUser);

            var model = new PublisherDetailsViewModel
            {
                Id = serviceModel.Id,
                Name = serviceModel.Name,
                AbbreviatedName = serviceModel.AbbreviatedName,
                DateCreated = serviceModel.DateCreated,
                DateModified = serviceModel.DateModified,
                CreatedBy = createdByUserName,
                ModifiedBy = modifiedByUserName
            };

            return model;
        }
    }
}

namespace ProcessingTools.Web.Areas.Journals.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using ProcessingTools.Common;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;
    using ProcessingTools.Journals.Services.Data.Contracts.Services;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Areas.Journals.Models.Publishers;
    using ProcessingTools.Web.Areas.Journals.ViewModels.Publishers;

    [Authorize]
    public class PublishersController : BaseMvcController
    {
        public const string ControllerName = "Publishers";
        public const string CreateActionName = "Create";
        public const string DeleteActionName = "Delete";
        public const string DetailsActionName = "Details";
        public const string EditActionName = "Edit";
        public const string AddressesActionName = "Addresses";

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

        private Func<IAddress, Address> MapAddress => a => new Address
        {
            Id = a.Id,
            AddressString = a.AddressString,
            CityId = a.CityId,
            CountryId = a.CountryId
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
        public async Task<ActionResult> Create([Bind(Include = "AbbreviatedName,Name")] Publisher model, string addresses)
        {
            if (this.ModelState.IsValid)
            {
                var modelId = await this.service.Add(this.UserId, model);
                await this.UpdateAddressesFromJson(modelId, addresses);

                return this.RedirectToAction(BaseMvcController.IndexActionName);
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

            var data = await this.service.Get(this.UserId, id);
            if (data == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.MapModelToViewModel(data);

            return this.View(viewModel);
        }

        // POST: Journals/Publishers/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AbbreviatedName,Name")] Publisher model, string addresses)
        {
            if (this.ModelState.IsValid)
            {
                var modelId = await this.service.Update(this.UserId, model);
                await this.UpdateAddressesFromJson(modelId, addresses);

                return this.RedirectToAction(BaseMvcController.IndexActionName);
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

            return this.RedirectToAction(BaseMvcController.IndexActionName);
        }

        [HttpGet, ActionName(AddressesActionName)]
        public async Task<JsonResult> Addresses(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = new JsonResult
            {
                ContentType = ContentTypes.Json,
                ContentEncoding = Defaults.DefaultEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new Address[] { }
            };

            var model = await this.service.GetDetails(this.UserId, id);
            if (model?.Addresses?.Count > 0)
            {
                result.Data = model.Addresses.Select(this.MapAddress).ToArray();
            }

            return result;
        }

        [HttpPost, ActionName(AddressesActionName)]
        public async Task<JsonResult> Addresses(string id, Address[] addresses)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await this.UpdateAddresses(id, addresses);

            return await this.Addresses(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }

        private async Task UpdateAddressesFromJson(object modelId, string addresses)
        {
            if (!string.IsNullOrWhiteSpace(addresses) && addresses != "[]")
            {
                try
                {
                    var addressesArray = JsonConvert.DeserializeObject<Address[]>(addresses);
                    await this.UpdateAddresses(modelId, addressesArray);
                }
                catch
                {
                }
            }
        }

        private async Task UpdateAddresses(object modelId, Address[] addresses)
        {
            if (addresses?.Length > 0)
            {
                foreach (var address in addresses)
                {
                    try
                    {
                        switch (address.Status)
                        {
                            case UpdateStatus.Modified:
                                await this.service.UpdateAddress(this.UserId, modelId, address);
                                break;

                            case UpdateStatus.Added:
                                await this.service.AddAddress(this.UserId, modelId, address);
                                break;

                            case UpdateStatus.Removed:
                                await this.service.RemoveAddress(this.UserId, modelId, address.Id);
                                break;

                            default:
                                break;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}

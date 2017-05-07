namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using Newtonsoft.Json;
    using ProcessingTools.Common;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Areas.Data.Filters.Continents;
    using ProcessingTools.Web.Areas.Data.Models.Continents;
    using ProcessingTools.Web.Areas.Data.Models.Shared;
    using ProcessingTools.Web.Areas.Data.ViewModels.Continents;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Constants;
    using Strings = ProcessingTools.Web.Resources.Areas.Data.Views.Continents.Strings;

    [Authorize]
    public class ContinentsController : BaseMvcController
    {
        public const string ControllerName = "Continents";
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string DetailsActionName = nameof(ContinentsController.Details);
        public const string CreateActionName = nameof(ContinentsController.Create);
        public const string EditActionName = nameof(ContinentsController.Edit);
        public const string DeleteActionName = nameof(ContinentsController.Delete);
        public const string SynonymsActionName = nameof(ContinentsController.Synonyms);

        private readonly IContinentsDataService service;
        private readonly IContinentSynonymsDataService synonymsService;
        private readonly IMapper mapper;

        public ContinentsController(IContinentsDataService service, IContinentSynonymsDataService synonymsService)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (synonymsService == null)
            {
                throw new ArgumentNullException(nameof(synonymsService));
            }

            this.service = service;
            this.synonymsService = synonymsService;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IContinent, ContinentViewModel>()
                    .ForMember(
                        destinationMember: d => d.Synonyms,
                        memberOptions: o => o.ResolveUsing(x => string.Empty))
                    .ForMember(
                        destinationMember: d => d.Countries,
                        memberOptions: o => o.UseValue<IEnumerable<CountryViewModel>>(null))
                    .ForMember(
                        destinationMember: d => d.NumberOfCountries,
                        memberOptions: o => o.ResolveUsing(x => x.Countries.Count));
                c.CreateMap<ContinentRequestModel, ContinentViewModel>()
                    .ForMember(
                        destinationMember: d => d.Synonyms,
                        memberOptions: o => o.ResolveUsing(x => string.Empty))
                    .ForMember(
                        destinationMember: d => d.Countries,
                        memberOptions: o => o.UseValue<IEnumerable<CountryViewModel>>(null))
                    .ForMember(
                        destinationMember: d => d.NumberOfCountries,
                        memberOptions: o => o.ResolveUsing(x => x.Countries.Count));
                c.CreateMap<IContinentSynonym, SynonymResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        // GET: Data/Continents
        [HttpGet, ActionName(IndexActionName)]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            long numberOfItems = await this.service.SelectCountAsync(null);
            var data = await this.service.SelectAsync(null, currentPage * numberOfItemsPerPage, numberOfItemsPerPage, nameof(IContinent.Name), SortOrder.Ascending);
            var items = data.Select(this.mapper.Map<ContinentViewModel>).ToArray();

            var viewModel = new ListWithPagingViewModel<ContinentViewModel>(IndexActionName, numberOfItems, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(IndexActionName, viewModel);
        }

        // GET: Data/Continents/Details/5
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

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.DetailsPageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(DetailsActionName, viewModel);
        }

        // GET: Data/Continents/Create
        [HttpGet, ActionName(CreateActionName)]
        public ActionResult Create()
        {
            var viewModel = new ContinentPageViewModel
            {
                Model = new ContinentViewModel { Id = -1 },
                PageTitle = Strings.CreatePageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // POST: Data/Continents/Create
        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] ContinentRequestModel model)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];

            if (this.ModelState.IsValid)
            {
                await this.service.InsertAsync(model);
                await this.service.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction(IndexActionName);
            }

            model.Id = -1;
            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.CreatePageTitle,
                ReturnUrl = returnUrl
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // GET: Data/Continents/Edit/5
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

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.EditPageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // POST: Data/Continents/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ContinentRequestModel model, string synonyms)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];

            if (this.ModelState.IsValid)
            {
                await this.service.UpdateAsync(model);
                await this.UpdateSynonymsFromJson(model.Id, synonyms);

                await this.service.SaveChangesAsync();
                await this.synonymsService.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }

                return this.RedirectToAction(IndexActionName);
            }

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.EditPageTitle,
                ReturnUrl = returnUrl
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(EditActionName, viewModel);
        }

        // GET: Data/Continents/Delete/5
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

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.DeletePageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(DeleteActionName, viewModel);
        }

        // POST: Data/Continents/Delete/5
        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await this.service.DeleteAsync(id: id);
            await this.service.SaveChangesAsync();

            string returnUrl = this.Request[ContextKeys.ReturnUrl];
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(IndexActionName);
        }

        // GET: Data/Continents/Synonyms/5
        [HttpGet, ActionName(SynonymsActionName)]
        public async Task<JsonResult> Synonyms(int id)
        {
            var result = new JsonResult
            {
                ContentType = ContentTypes.Json,
                ContentEncoding = Defaults.DefaultEncoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new SynonymResponseModel[] { }
            };

            var data = await this.synonymsService.SelectAsync(new ContinentSynonymsFilter
            {
                ParentId = id
            });

            if (data?.Length > 0)
            {
                result.Data = data.Select(s => this.mapper.Map<SynonymResponseModel>(s)).ToArray();
            }

            return result;
        }

        private async Task UpdateSynonymsFromJson(int modelId, string synonyms)
        {
            if (!string.IsNullOrWhiteSpace(synonyms) && synonyms != "[]")
            {
                try
                {
                    var synonymsArray = JsonConvert.DeserializeObject<ContinentSynonymRequestModel[]>(synonyms);
                    await this.UpdateSynonyms(modelId, synonymsArray);
                }
                catch
                {
                }
            }
        }

        private async Task UpdateSynonyms(int modelId, ContinentSynonymRequestModel[] synonyms)
        {
            if (synonyms?.Length > 0)
            {
                foreach (var synonym in synonyms)
                {
                    try
                    {
                        synonym.ParentId = modelId;
                        switch (synonym.Status)
                        {
                            case UpdateStatus.Modified:
                                await this.synonymsService.UpdateAsync(synonym);
                                break;

                            case UpdateStatus.Added:
                                await this.synonymsService.InsertAsync(synonym);
                                break;

                            case UpdateStatus.Removed:
                                await this.synonymsService.DeleteAsync(synonym.Id);
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
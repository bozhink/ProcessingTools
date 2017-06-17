namespace ProcessingTools.Web.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper;
    using Newtonsoft.Json;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.ViewModels;
    using ProcessingTools.Web.Abstractions.Controllers;
    using ProcessingTools.Web.Areas.Data.Models.Continents;
    using ProcessingTools.Web.Areas.Data.Models.Shared;
    using ProcessingTools.Web.Constants;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.Continents.Views_Strings;

    [Authorize]
    public class ContinentsController : BaseMvcController
    {
        public const string ControllerName = "Continents";
        public const string AreaName = AreaNames.Data;
        public const string IndexActionName = RouteValues.IndexActionName;
        public const string DetailsActionName = nameof(ContinentsController.Details);
        public const string CreateActionName = nameof(ContinentsController.Create);
        public const string EditActionName = nameof(ContinentsController.Edit);
        public const string DeleteActionName = nameof(ContinentsController.Delete);
        public const string SynonymsActionName = nameof(ContinentsController.Synonyms);

        private readonly IContinentsDataService service;
        private readonly ICountriesDataService countriesService;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ContinentsController(IContinentsDataService service, ICountriesDataService countriesService, ILoggerFactory loggerFactory)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.countriesService = countriesService ?? throw new ArgumentNullException(nameof(countriesService));
            this.logger = loggerFactory?.CreateLogger(this.GetType());

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IContinent, ContinentViewModel>()
                    .ForMember(
                        destinationMember: d => d.Synonyms,
                        memberOptions: o => o.ResolveUsing(x => x.Synonyms?.Select(s => new SynonymViewModel
                        {
                            Id = s.Id,
                            Name = s.Name,
                            LanguageCode = s.LanguageCode.ToString()
                        })))
                    .ForMember(
                        destinationMember: d => d.Countries,
                        memberOptions: o => o.ResolveUsing(x => x.Countries?.Select(s => new CountryViewModel
                        {
                            Id = s.Id,
                            Name = s.Name,
                            LanguageCode = s.LanguageCode
                        })))
                    .ForMember(
                        destinationMember: d => d.NumberOfCountries,
                        memberOptions: o => o.ResolveUsing(x => x.Countries.Count));

                c.CreateMap<ContinentRequestModel, ContinentViewModel>()
                    .ForMember(
                        destinationMember: d => d.Synonyms,
                        memberOptions: o => o.ResolveUsing(x => x.Synonyms?.Select(s => new SynonymViewModel
                        {
                            Id = s.Id,
                            Name = s.Name,
                            LanguageCode = s.LanguageCode.ToString()
                        })))
                    .ForMember(
                        destinationMember: d => d.Countries,
                        memberOptions: o => o.ResolveUsing(x => x.Countries?.Select(s => new CountryViewModel
                        {
                            Id = s.Id,
                            Name = s.Name,
                            LanguageCode = s.LanguageCode
                        })))
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

            var model = new ListWithPagingViewModel<ContinentViewModel>(IndexActionName, numberOfItems, numberOfItemsPerPage, currentPage, items);
            var viewModel = new ContinentIndexPageViewModel
            {
                Model = model,
                PageTitle = Strings.IndexPageTitle
            };

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = IndexActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(IndexActionName, viewModel);
        }

        // GET: Data/Continents/Details/5
        [HttpGet, ActionName(DetailsActionName)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                this.logger?.Log(LogType.Info, id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                this.logger?.Log(LogType.Error, id);
                return this.HttpNotFound();
            }

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.DetailsPageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            var countries = await this.countriesService.SelectAsync(null);
            foreach (var synonym in viewModel.Model.Synonyms)
            {
                synonym.LanguageCode = countries.Where(c => c.Id.ToString() == synonym.LanguageCode)
                    .Select(c => c.LanguageCode)
                    .FirstOrDefault();
            }

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = DetailsActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
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

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = CreateActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(EditActionName, viewModel);
        }

        // POST: Data/Continents/Create
        [HttpPost, ActionName(CreateActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = nameof(ContinentRequestModel.Name) + "," + nameof(ContinentRequestModel.AbbreviatedName))] ContinentRequestModel model, string synonyms, bool exit = false, bool createNew = false, bool cancel = false)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];

            if (cancel)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction(IndexActionName);
                }
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    var id = await this.InsertModel(model, synonyms);

                    if (createNew)
                    {
                        return this.RedirectToAction(CreateActionName, routeValues: new { ReturnUrl = returnUrl });
                    }

                    if (id == null || exit)
                    {
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return this.Redirect(returnUrl);
                        }
                        else
                        {
                            return this.RedirectToAction(IndexActionName);
                        }
                    }

                    return this.RedirectToAction(EditActionName, routeValues: new { id = id, ReturnUrl = returnUrl });
                }
                else
                {
                    this.AddErrors(Strings.InvalidDataErrorMessage);
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                this.AddErrors(e.Message);
            }

            model.Id = -1;
            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.CreatePageTitle,
                ReturnUrl = returnUrl
            };

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = CreateActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(EditActionName, viewModel);
        }

        // GET: Data/Continents/Edit/5
        [HttpGet, ActionName(EditActionName)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                this.logger?.Log(LogType.Info, id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                this.logger?.Log(LogType.Error, id);
                return this.HttpNotFound();
            }

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.EditPageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = EditActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(EditActionName, viewModel);
        }

        // POST: Data/Continents/Edit/5
        [HttpPost, ActionName(EditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = nameof(ContinentRequestModel.Id) + "," + nameof(ContinentRequestModel.Name) + "," + nameof(ContinentRequestModel.AbbreviatedName))] ContinentRequestModel model, string synonyms, bool exit = false, bool createNew = false, bool cancel = false)
        {
            string returnUrl = this.Request[ContextKeys.ReturnUrl];

            if (cancel)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction(IndexActionName);
                }
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.service.UpdateAsync(model);
                    await this.UpdateSynonymsFromJson(model.Id, synonyms);

                    if (createNew)
                    {
                        return this.RedirectToAction(CreateActionName, routeValues: new { ReturnUrl = returnUrl });
                    }

                    if (exit)
                    {
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return this.Redirect(returnUrl);
                        }
                        else
                        {
                            return this.RedirectToAction(IndexActionName);
                        }
                    }

                    return this.RedirectToAction(EditActionName, routeValues: new { id = model.Id, ReturnUrl = returnUrl });
                }
                else
                {
                    this.AddErrors(Strings.InvalidDataErrorMessage);
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                this.AddErrors(e.Message);
            }

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.EditPageTitle,
                ReturnUrl = returnUrl
            };

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = EditActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(EditActionName, viewModel);
        }

        // GET: Data/Continents/Delete/5
        [HttpGet, ActionName(DeleteActionName)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                this.logger?.Log(LogType.Info, id);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await this.service.GetByIdAsync(id);
            if (model == null)
            {
                this.logger?.Log(LogType.Error, id);
                return this.HttpNotFound();
            }

            var viewModel = new ContinentPageViewModel
            {
                Model = this.mapper.Map<ContinentViewModel>(model),
                PageTitle = Strings.DeletePageTitle,
                ReturnUrl = this.Request[ContextKeys.ReturnUrl]
            };

            this.ViewData[ContextKeys.AreaName] = AreaName;
            this.ViewData[ContextKeys.ControllerName] = ControllerName;
            this.ViewData[ContextKeys.ActionName] = DeleteActionName;
            this.ViewData[ContextKeys.BackActionName] = IndexActionName;
            return this.View(DeleteActionName, viewModel);
        }

        // POST: Data/Continents/Delete/5
        [HttpPost, ActionName(DeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await this.service.DeleteAsync(id: id);

                string returnUrl = this.Request[ContextKeys.ReturnUrl];
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception e)
            {
                this.logger?.Log(e, string.Empty);
                this.AddErrors(e.Message);
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
                ContentEncoding = Defaults.Encoding,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new SynonymResponseModel[] { }
            };

            var data = await this.service.SelectSynonymsAsync(id, null);

            if (data?.Length > 0)
            {
                result.Data = data.Select(s => this.mapper.Map<SynonymResponseModel>(s)).ToArray();
            }

            return result;
        }

        private async Task<object> InsertModel(ContinentRequestModel model, string synonyms)
        {
            object id = null;
            bool inserted = false;
            if (!string.IsNullOrWhiteSpace(synonyms) && synonyms != "[]")
            {
                try
                {
                    var synonymsArray = JsonConvert.DeserializeObject<ContinentSynonymRequestModel[]>(synonyms);
                    var addedSynonyms = synonymsArray.Where(s => s.Status == UpdateStatus.Added).ToArray();
                    id = await this.service.InsertAsync(model, addedSynonyms);
                    inserted = true;
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                    inserted = false;
                }
            }

            if (!inserted)
            {
                id = await this.service.InsertAsync(model);
            }

            return id;
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
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }
            }
        }

        private async Task UpdateSynonyms(int modelId, ContinentSynonymRequestModel[] synonyms)
        {
            if (synonyms?.Length > 0)
            {
                try
                {
                    foreach (var synonym in synonyms)
                    {
                        synonym.ParentId = modelId;
                    }

                    var modifiedSynonyms = synonyms.Where(s => s.Status == UpdateStatus.Modified).ToArray();
                    if (modifiedSynonyms.Length > 0)
                    {
                        await this.service.UpdateSynonymsAsync(modelId, modifiedSynonyms);
                    }

                    var addedSynonyms = synonyms.Where(s => s.Status == UpdateStatus.Added).ToArray();
                    if (addedSynonyms.Length > 0)
                    {
                        await this.service.AddSynonymsAsync(modelId, addedSynonyms);
                    }

                    var removedSynonyms = synonyms.Where(s => s.Status == UpdateStatus.Removed).Select(s => s.Id).ToArray();
                    if (removedSynonyms.Length > 0)
                    {
                        await this.service.RemoveSynonymsAsync(modelId, removedSynonyms);
                    }
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }
            }
        }
    }
}

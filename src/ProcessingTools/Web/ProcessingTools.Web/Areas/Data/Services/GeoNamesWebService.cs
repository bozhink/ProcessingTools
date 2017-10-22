﻿namespace ProcessingTools.Web.Areas.Data.Services
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Data.Geo;
    using ProcessingTools.Web.Areas.Data.Controllers;
    using ProcessingTools.Web.Contracts.Services.Geo;
    using ProcessingTools.Web.Models.Geo.GeoNames;
    using ProcessingTools.Web.Models.Shared;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.GeoNames.Views_Strings;

    public class GeoNamesWebService : IGeoNamesWebService
    {
        private readonly IGeoNamesDataService service;
        private readonly IMapper mapper;

        public GeoNamesWebService(IGeoNamesDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoName, GeoNameViewModel>();
                c.CreateMap<GeoNameRequestModel, GeoNameViewModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<GeoNamesIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage)
        {
            if (currentPage < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (numberOfItemsPerPage < PaginationConstants.MinimalItemsPerPage || numberOfItemsPerPage > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            long numberOfItems = await this.service.SelectCountAsync(null).ConfigureAwait(false);
            var data = await this.service.SelectAsync(null, currentPage * numberOfItemsPerPage, numberOfItemsPerPage, nameof(IGeoName.Name), SortOrder.Ascending).ConfigureAwait(false);
            var items = data.Select(this.mapper.Map<GeoNameViewModel>).ToArray();

            var model = new ListWithPagingViewModel<GeoNameViewModel>(GeoNamesController.IndexActionName, numberOfItems, numberOfItemsPerPage, currentPage, items);
            var viewModel = new GeoNamesIndexPageViewModel
            {
                Model = model,
                PageTitle = Strings.IndexPageTitle
            };

            return viewModel;
        }

        public async Task InsertAsync(GeoNamesRequestModel model)
        {
            await this.service.InsertAsync(model.ToArray()).ConfigureAwait(false);
        }

        public async Task UpdateAsync(GeoNameRequestModel model)
        {
            await this.service.UpdateAsync(model).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            await this.service.DeleteAsync(ids: id).ConfigureAwait(false);
        }
    }
}

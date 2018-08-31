﻿namespace ProcessingTools.Web.Areas.Data.Services
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Web.Services.Geo;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;
    using ProcessingTools.Web.Areas.Data.Controllers;
    using ProcessingTools.Web.Models.Geo.GeoEpithets;
    using ProcessingTools.Web.Models.Shared;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.GeoEpithets.Views_Strings;

    public class GeoEpithetsWebService : IGeoEpithetsWebService
    {
        private readonly IGeoEpithetsDataService service;
        private readonly IMapper mapper;

        public GeoEpithetsWebService(IGeoEpithetsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoEpithet, GeoEpithetViewModel>();
                c.CreateMap<GeoEpithetRequestModel, GeoEpithetViewModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<GeoEpithetsIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage)
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
            var items = data.Select(this.mapper.Map<GeoEpithetViewModel>).ToArray();

            var model = new ListWithPagingViewModel<GeoEpithetViewModel>(GeoEpithetsController.IndexActionName, numberOfItems, numberOfItemsPerPage, currentPage, items);
            var viewModel = new GeoEpithetsIndexPageViewModel
            {
                Model = model,
                PageTitle = Strings.IndexPageTitle
            };
            return viewModel;
        }

        public async Task InsertAsync(GeoEpithetsRequestModel model)
        {
            await this.service.InsertAsync(model.ToArray()).ConfigureAwait(false);
        }

        public async Task UpdateAsync(GeoEpithetRequestModel model)
        {
            await this.service.UpdateAsync(model).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            await this.service.DeleteAsync(ids: id).ConfigureAwait(false);
        }
    }
}

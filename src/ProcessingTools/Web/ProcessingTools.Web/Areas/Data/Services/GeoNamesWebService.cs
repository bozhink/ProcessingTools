namespace ProcessingTools.Web.Areas.Data.Services
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Contracts.Web.Services.Geo;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Web.Areas.Data.Controllers;
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

        public Task InsertAsync(GeoNamesRequestModel model)
        {
            return this.service.InsertAsync(model.ToArray());
        }

        public Task UpdateAsync(GeoNameRequestModel model)
        {
            return this.service.UpdateAsync(model);
        }

        public Task DeleteAsync(int id)
        {
            return this.service.DeleteAsync(ids: id);
        }
    }
}

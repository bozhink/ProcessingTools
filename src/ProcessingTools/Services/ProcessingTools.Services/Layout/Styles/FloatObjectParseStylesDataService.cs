// <copyright file="FloatObjectParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Layout.Styles;
    using ProcessingTools.Contracts.Services.Models.Layout.Styles;
    using ProcessingTools.Contracts.Services.Models.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Layout.Styles;
    using ProcessingTools.Services.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles data service.
    /// </summary>
    public class FloatObjectParseStylesDataService : IFloatObjectParseStylesDataService
    {
        private readonly IFloatObjectParseStylesDataAccessObject dataAccessObject;
        private readonly IMapper mapper;
        private readonly IObjectHistoryDataService objectHistoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public FloatObjectParseStylesDataService(IFloatObjectParseStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IFloatObjectParseStyleDataTransferObject, FloatObjectParseStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IFloatObjectDetailsParseStyleDataTransferObject, FloatObjectDetailsParseStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataTransferObject, StyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>().As<StyleModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.DeleteInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IFloatObjectParseStyleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IFloatObjectDetailsParseStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id)
        {
            var style = await this.dataAccessObject.GetStyleByIdAsync(id).ConfigureAwait(false);
            return this.mapper.Map<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>(style);
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel[]> GetStylesForSelectAsync()
        {
            var styles = await this.dataAccessObject.GetStylesForSelectAsync().ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IFloatObjectInsertParseStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IFloatObjectParseStyleModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            return this.SelectInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IFloatObjectDetailsParseStyleModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            return this.SelectDetailsInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IFloatObjectUpdateParseStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateInternalAsync(model);
        }

        private async Task<object> DeleteInternalAsync(object id)
        {
            var result = await this.dataAccessObject.DeleteAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<IFloatObjectParseStyleModel> GetByIdInternalAsync(object id)
        {
            var parseStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (parseStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IFloatObjectParseStyleDataTransferObject, FloatObjectParseStyleModel>(parseStyle);

            return model;
        }

        private async Task<IFloatObjectDetailsParseStyleModel> GetDetailsByIdInternalAsync(object id)
        {
            var parseStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (parseStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IFloatObjectDetailsParseStyleDataTransferObject, FloatObjectDetailsParseStyleModel>(parseStyle);

            return model;
        }

        private async Task<object> InsertInternalAsync(IFloatObjectInsertParseStyleModel model)
        {
            var parseStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (parseStyle == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(parseStyle.ObjectId, parseStyle).ConfigureAwait(false);

            return parseStyle.ObjectId;
        }

        private async Task<IFloatObjectDetailsParseStyleModel[]> SelectDetailsInternalAsync(int skip, int take)
        {
            var parseStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return Array.Empty<IFloatObjectDetailsParseStyleModel>();
            }

            var items = parseStyles.Select(this.mapper.Map<IFloatObjectDetailsParseStyleDataTransferObject, FloatObjectDetailsParseStyleModel>).ToArray();
            return items;
        }

        private async Task<IFloatObjectParseStyleModel[]> SelectInternalAsync(int skip, int take)
        {
            var parseStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return Array.Empty<IFloatObjectParseStyleModel>();
            }

            var items = parseStyles.Select(this.mapper.Map<IFloatObjectParseStyleDataTransferObject, FloatObjectParseStyleModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateInternalAsync(IFloatObjectUpdateParseStyleModel model)
        {
            var parseStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (parseStyle == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(parseStyle.ObjectId, parseStyle).ConfigureAwait(false);

            return parseStyle.ObjectId;
        }
    }
}

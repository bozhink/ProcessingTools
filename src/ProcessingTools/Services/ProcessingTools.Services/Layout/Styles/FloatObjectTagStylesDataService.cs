// <copyright file="FloatObjectTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles data service.
    /// </summary>
    public class FloatObjectTagStylesDataService : IFloatObjectTagStylesDataService
    {
        private readonly IFloatObjectTagStylesDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public FloatObjectTagStylesDataService(IFloatObjectTagStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IFloatObjectTagStyleDataModel, FloatObjectTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IFloatObjectDetailsTagStyleDataModel, FloatObjectDetailsTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IFloatObjectInsertTagStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var tagStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (tagStyle == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(tagStyle.ObjectId, tagStyle).ConfigureAwait(false);

            return tagStyle.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IFloatObjectUpdateTagStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var tagStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (tagStyle == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(tagStyle.ObjectId, tagStyle).ConfigureAwait(false);

            return tagStyle.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.dataAccessObject.DeleteAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectTagStyleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var tagStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (tagStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IFloatObjectTagStyleDataModel, FloatObjectTagStyleModel>(tagStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectDetailsTagStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var tagStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (tagStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IFloatObjectDetailsTagStyleDataModel, FloatObjectDetailsTagStyleModel>(tagStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectTagStyleModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var tagStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return new IFloatObjectTagStyleModel[] { };
            }

            var items = tagStyles.Select(this.mapper.Map<IFloatObjectTagStyleDataModel, FloatObjectTagStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectDetailsTagStyleModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var tagStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return new IFloatObjectDetailsTagStyleModel[] { };
            }

            var items = tagStyles.Select(this.mapper.Map<IFloatObjectDetailsTagStyleDataModel, FloatObjectDetailsTagStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();
    }
}

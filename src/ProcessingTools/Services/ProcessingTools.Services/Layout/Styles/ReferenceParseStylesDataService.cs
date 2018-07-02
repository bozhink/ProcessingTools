// <copyright file="ReferenceParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Services.Models.Layout.Styles;
    using ProcessingTools.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles data service.
    /// </summary>
    public class ReferenceParseStylesDataService : IReferenceParseStylesDataService
    {
        private readonly IReferenceParseStylesDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public ReferenceParseStylesDataService(IReferenceParseStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IReferenceParseStyleDataModel, ReferenceParseStyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IReferenceDetailsParseStyleDataModel, ReferenceDetailsParseStyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataModel, StyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataModel, IIdentifiedStyleModel>().As<StyleModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IReferenceInsertParseStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var parseStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (parseStyle == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(parseStyle.ObjectId, parseStyle).ConfigureAwait(false);

            return parseStyle.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IReferenceUpdateParseStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var parseStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (parseStyle == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(parseStyle.ObjectId, parseStyle).ConfigureAwait(false);

            return parseStyle.ObjectId;
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
        public async Task<IReferenceParseStyleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var parseStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (parseStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IReferenceParseStyleDataModel, ReferenceParseStyleModel>(parseStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IReferenceDetailsParseStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var parseStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (parseStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IReferenceDetailsParseStyleDataModel, ReferenceDetailsParseStyleModel>(parseStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IReferenceParseStyleModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var parseStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return new IReferenceParseStyleModel[] { };
            }

            var items = parseStyles.Select(this.mapper.Map<IReferenceParseStyleDataModel, ReferenceParseStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IReferenceDetailsParseStyleModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var parseStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return new IReferenceDetailsParseStyleModel[] { };
            }

            var items = parseStyles.Select(this.mapper.Map<IReferenceDetailsParseStyleDataModel, ReferenceDetailsParseStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id)
        {
            var style = await this.dataAccessObject.GetStyleByIdAsync(id).ConfigureAwait(false);
            return this.mapper.Map<IIdentifiedStyleDataModel, IIdentifiedStyleModel>(style);
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel[]> GetStylesForSelectAsync()
        {
            var styles = await this.dataAccessObject.GetStylesForSelectAsync().ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IIdentifiedStyleDataModel, IIdentifiedStyleModel>).ToArray();
        }
    }
}

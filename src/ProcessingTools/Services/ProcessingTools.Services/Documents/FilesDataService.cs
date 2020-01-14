// <copyright file="FilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Files;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Models.Documents.Files;
    using ProcessingTools.Services.Models.Documents.Files;

    /// <summary>
    /// Files data service.
    /// </summary>
    public class FilesDataService : IFilesDataService
    {
        private readonly IFilesDataAccessObject dataAccessObject;
        private readonly IMapper mapper;
        private readonly IObjectHistoryDataService objectHistoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public FilesDataService(IFilesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IFileDataTransferObject, FileModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IFileDetailsDataTransferObject, FileDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
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
        public Task<IFileModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IFileDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IFileInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IList<IFileModel>> SelectAsync(int skip, int take)
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
        public Task<IList<IFileDetailsModel>> SelectDetailsAsync(int skip, int take)
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
        public Task<object> UpdateAsync(IFileUpdateModel model)
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

        private async Task<IFileModel> GetByIdInternalAsync(object id)
        {
            var file = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (file == null)
            {
                return null;
            }

            var model = this.mapper.Map<IFileDataTransferObject, FileModel>(file);

            return model;
        }

        private async Task<IFileDetailsModel> GetDetailsByIdInternalAsync(object id)
        {
            var file = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (file == null)
            {
                return null;
            }

            var model = this.mapper.Map<IFileDetailsDataTransferObject, FileDetailsModel>(file);

            return model;
        }

        private async Task<object> InsertInternalAsync(IFileInsertModel model)
        {
            var file = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (file == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(file.ObjectId, file).ConfigureAwait(false);

            return file.ObjectId;
        }

        private async Task<IList<IFileDetailsModel>> SelectDetailsInternalAsync(int skip, int take)
        {
            var files = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (files == null || !files.Any())
            {
                return Array.Empty<IFileDetailsModel>();
            }

            var items = files.Select(this.mapper.Map<IFileDetailsDataTransferObject, FileDetailsModel>).ToArray();
            return items;
        }

        private async Task<IList<IFileModel>> SelectInternalAsync(int skip, int take)
        {
            var files = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (files == null || !files.Any())
            {
                return Array.Empty<IFileModel>();
            }

            var items = files.Select(this.mapper.Map<IFileDataTransferObject, FileModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateInternalAsync(IFileUpdateModel model)
        {
            var file = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (file == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(file.ObjectId, file).ConfigureAwait(false);

            return file.ObjectId;
        }
    }
}

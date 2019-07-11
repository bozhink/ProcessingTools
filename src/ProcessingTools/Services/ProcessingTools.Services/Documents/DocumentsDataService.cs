// <copyright file="DocumentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Documents;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Models.Documents.Documents;
    using ProcessingTools.Services.Models.Documents.Documents;

    /// <summary>
    /// Documents data service.
    /// </summary>
    public class DocumentsDataService : IDocumentsDataService
    {
        private readonly IDocumentsDataAccessObject dataAccessObject;
        private readonly IMapper mapper;
        private readonly IObjectHistoryDataService objectHistoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public DocumentsDataService(IDocumentsDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IDocumentDataTransferObject, DocumentModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IDocumentDetailsDataTransferObject, DocumentDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IDocumentArticleDataTransferObject, DocumentArticleModel>();
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
        public Task<IDocumentModel[]> GetArticleDocumentsAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return this.GetArticleDocumentsInternalAsync(articleId);
        }

        /// <inheritdoc/>
        public Task<IDocumentModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IDocumentDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IDocumentArticleModel> GetDocumentArticleAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return this.GetDocumentArticleInternalAsync(articleId);
        }

        /// <inheritdoc/>
        public Task<string> GetDocumentContentAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDocumentContentInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IDocumentInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IDocumentModel[]> SelectAsync(int skip, int take)
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
        public Task<IDocumentDetailsModel[]> SelectDetailsAsync(int skip, int take)
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
        public Task<object> SetAsFinalAsync(object id, string articleId)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return this.SetAsFinalInternalAsync(id, articleId);
        }

        /// <inheritdoc/>
        public Task<long> SetDocumentContentAsync(object id, string content)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.SetDocumentContentInternalAsync(id, content);
        }

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IDocumentUpdateModel model)
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

        private async Task<IDocumentModel[]> GetArticleDocumentsInternalAsync(string articleId)
        {
            var documents = await this.dataAccessObject.GetArticleDocumentsAsync(articleId).ConfigureAwait(false);

            if (documents == null || !documents.Any())
            {
                return Array.Empty<IDocumentModel>();
            }

            var model = documents.Select(this.mapper.Map<IDocumentDataTransferObject, DocumentModel>).ToArray();

            return model;
        }

        private async Task<IDocumentModel> GetByIdInternalAsync(object id)
        {
            var document = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (document == null)
            {
                return null;
            }

            var model = this.mapper.Map<IDocumentDataTransferObject, DocumentModel>(document);

            return model;
        }

        private async Task<IDocumentDetailsModel> GetDetailsByIdInternalAsync(object id)
        {
            var document = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (document == null)
            {
                return null;
            }

            var model = this.mapper.Map<IDocumentDetailsDataTransferObject, DocumentDetailsModel>(document);

            return model;
        }

        private async Task<IDocumentArticleModel> GetDocumentArticleInternalAsync(string articleId)
        {
            var article = await this.dataAccessObject.GetDocumentArticleAsync(articleId).ConfigureAwait(false);
            if (article == null)
            {
                return null;
            }

            var model = this.mapper.Map<IDocumentArticleDataTransferObject, DocumentArticleModel>(article);

            return model;
        }

        private async Task<string> GetDocumentContentInternalAsync(object id)
        {
            return await this.dataAccessObject.GetDocumentContentAsync(id).ConfigureAwait(false);
        }

        private async Task<object> InsertInternalAsync(IDocumentInsertModel model)
        {
            var document = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (document == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(document.ObjectId, document).ConfigureAwait(false);

            return document.ObjectId;
        }

        private async Task<IDocumentDetailsModel[]> SelectDetailsInternalAsync(int skip, int take)
        {
            var documents = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (documents == null || !documents.Any())
            {
                return Array.Empty<IDocumentDetailsModel>();
            }

            var items = documents.Select(this.mapper.Map<IDocumentDetailsDataTransferObject, DocumentDetailsModel>).ToArray();
            return items;
        }

        private async Task<IDocumentModel[]> SelectInternalAsync(int skip, int take)
        {
            var documents = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (documents == null || !documents.Any())
            {
                return Array.Empty<IDocumentModel>();
            }

            var items = documents.Select(this.mapper.Map<IDocumentDataTransferObject, DocumentModel>).ToArray();
            return items;
        }

        private async Task<object> SetAsFinalInternalAsync(object id, string articleId)
        {
            return await this.dataAccessObject.SetAsFinalAsync(id, articleId).ConfigureAwait(false);
        }

        private async Task<long> SetDocumentContentInternalAsync(object id, string content)
        {
            return await this.dataAccessObject.SetDocumentContentAsync(id, content).ConfigureAwait(false);
        }

        private async Task<object> UpdateInternalAsync(IDocumentUpdateModel model)
        {
            var document = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (document == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(document.ObjectId, document).ConfigureAwait(false);

            return document.ObjectId;
        }
    }
}

// <copyright file="DocumentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;
    using ProcessingTools.Services.Models.Documents.Documents;

    /// <summary>
    /// Documents data service.
    /// </summary>
    public class DocumentsDataService : IDocumentsDataService
    {
        private readonly IDocumentsDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

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
                c.CreateMap<IDocumentDataModel, DocumentModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IDocumentDetailsDataModel, DocumentDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IDocumentInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var document = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (document == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(document.ObjectId, document).ConfigureAwait(false);

            return document.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IDocumentUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var document = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (document == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(document.ObjectId, document).ConfigureAwait(false);

            return document.ObjectId;
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
        public async Task<IDocumentModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var document = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (document == null)
            {
                return null;
            }

            var model = this.mapper.Map<IDocumentDataModel, DocumentModel>(document);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var document = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (document == null)
            {
                return null;
            }

            var model = this.mapper.Map<IDocumentDetailsDataModel, DocumentDetailsModel>(document);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IDocumentModel[]> GetArticleDocumentsAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var documents = await this.dataAccessObject.GetArticleDocumentsAsync(articleId).ConfigureAwait(false);

            if (documents == null || !documents.Any())
            {
                return new IDocumentModel[] { };
            }

            var model = documents.Select(this.mapper.Map<IDocumentDataModel, DocumentModel>).ToArray();

            return model;
        }

        /// <inheritdoc/>
        public async Task<string> GetDocumentContentAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string content = await this.dataAccessObject.GetDocumentContentAsync(id).ConfigureAwait(false);

            return content;
        }

        /// <inheritdoc/>
        public async Task<IDocumentModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var documents = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (documents == null || !documents.Any())
            {
                return new IDocumentModel[] { };
            }

            var items = documents.Select(this.mapper.Map<IDocumentDataModel, DocumentModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDetailsModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var documents = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (documents == null || !documents.Any())
            {
                return new IDocumentDetailsModel[] { };
            }

            var items = documents.Select(this.mapper.Map<IDocumentDetailsDataModel, DocumentDetailsModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<long> SetDocumentContentAsync(object id, string content)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.dataAccessObject.SetDocumentContentAsync(id, content).ConfigureAwait(false);

            return result;
        }
    }
}

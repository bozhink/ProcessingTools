// <copyright file="ArticlesDataService.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Articles;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Models.Documents.Articles;
    using ProcessingTools.Services.Models.Documents.Articles;

    /// <summary>
    /// Articles data service.
    /// </summary>
    public class ArticlesDataService : IArticlesDataService
    {
        private readonly IArticlesDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public ArticlesDataService(IArticlesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IArticleJournalDataTransferObject, ArticleJournalModel>();
                c.CreateMap<IArticleJournalDataTransferObject, IArticleJournalModel>().As<ArticleJournalModel>();

                c.CreateMap<IArticleDataTransferObject, ArticleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IArticleDetailsDataTransferObject, ArticleDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()))
                    .ForMember(sm => sm.Journal, o => o.MapFrom(dm => dm.Journal));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IArticleInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IArticleUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateInternalAsync(model);
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
        public Task<IArticleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IArticleDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IArticleModel[]> SelectAsync(int skip, int take)
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
        public Task<IArticleDetailsModel[]> SelectDetailsAsync(int skip, int take)
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
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<IArticleJournalModel[]> GetArticleJournalsAsync()
        {
            var journals = await this.dataAccessObject.GetArticleJournalsAsync().ConfigureAwait(false);
            if (journals == null || !journals.Any())
            {
                return Array.Empty<IArticleJournalModel>();
            }

            return journals.Select(this.mapper.Map<IArticleJournalDataTransferObject, ArticleJournalModel>).ToArray();
        }

        /// <inheritdoc/>
        public Task<object> GetJournalStyleIdAsync(object id) => this.dataAccessObject.GetJournalStyleIdAsync(id);

        /// <inheritdoc/>
        public Task<object> FinalizeAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.FinalizeInternalAsync(id);
        }

        private async Task<object> InsertInternalAsync(IArticleInsertModel model)
        {
            var article = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (article == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(article.ObjectId, article).ConfigureAwait(false);

            return article.ObjectId;
        }

        private async Task<object> UpdateInternalAsync(IArticleUpdateModel model)
        {
            var article = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (article == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(article.ObjectId, article).ConfigureAwait(false);

            return article.ObjectId;
        }

        private async Task<object> DeleteInternalAsync(object id)
        {
            var result = await this.dataAccessObject.DeleteAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<IArticleModel> GetByIdInternalAsync(object id)
        {
            var article = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (article == null)
            {
                return null;
            }

            var model = this.mapper.Map<IArticleDataTransferObject, ArticleModel>(article);

            return model;
        }

        private async Task<IArticleDetailsModel> GetDetailsByIdInternalAsync(object id)
        {
            var article = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (article == null)
            {
                return null;
            }

            var model = this.mapper.Map<IArticleDetailsDataTransferObject, ArticleDetailsModel>(article);

            return model;
        }

        private async Task<IArticleModel[]> SelectInternalAsync(int skip, int take)
        {
            var articles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (articles == null || !articles.Any())
            {
                return Array.Empty<IArticleModel>();
            }

            var items = articles.Select(this.mapper.Map<IArticleDataTransferObject, ArticleModel>).ToArray();
            return items;
        }

        private async Task<IArticleDetailsModel[]> SelectDetailsInternalAsync(int skip, int take)
        {
            var articles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (articles == null || !articles.Any())
            {
                return Array.Empty<IArticleDetailsModel>();
            }

            var items = articles.Select(this.mapper.Map<IArticleDetailsDataTransferObject, ArticleDetailsModel>).ToArray();
            return items;
        }

        private async Task<object> FinalizeInternalAsync(object id)
        {
            var article = await this.dataAccessObject.FinalizeAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (article == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(article.ObjectId, article).ConfigureAwait(false);

            return article.ObjectId;
        }
    }
}

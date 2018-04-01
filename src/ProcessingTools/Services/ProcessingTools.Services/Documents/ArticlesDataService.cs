// <copyright file="ArticlesDataService.cs" company="ProcessingTools">
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
    using ProcessingTools.Data.Models.Contracts.Documents.Articles;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;
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
                c.CreateMap<IArticleJournalDataModel, ArticleJournalModel>();
                c.CreateMap<IArticleJournalDataModel, IArticleJournalModel>().As<ArticleJournalModel>();

                c.CreateMap<IArticleDataModel, ArticleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IArticleDetailsDataModel, ArticleDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()))
                    .ForMember(sm => sm.Journal, o => o.MapFrom(dm => dm.Journal));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IArticleInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var article = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (article == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(article.ObjectId, article).ConfigureAwait(false);

            return article.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IArticleUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var article = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (article == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(article.ObjectId, article).ConfigureAwait(false);

            return article.ObjectId;
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
        public async Task<IArticleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var article = await this.dataAccessObject.GetById(id).ConfigureAwait(false);

            if (article == null)
            {
                return null;
            }

            var model = this.mapper.Map<IArticleDataModel, ArticleModel>(article);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IArticleDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var article = await this.dataAccessObject.GetDetailsById(id).ConfigureAwait(false);

            if (article == null)
            {
                return null;
            }

            var model = this.mapper.Map<IArticleDetailsDataModel, ArticleDetailsModel>(article);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IArticleModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var articles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (articles == null || !articles.Any())
            {
                return new IArticleModel[] { };
            }

            var items = articles.Select(this.mapper.Map<IArticleDataModel, ArticleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IArticleDetailsModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var articles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (articles == null || !articles.Any())
            {
                return new IArticleDetailsModel[] { };
            }

            var items = articles.Select(this.mapper.Map<IArticleDetailsDataModel, ArticleDetailsModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<IArticleJournalModel[]> GetArticleJournalsAsync()
        {
            var journals = await this.dataAccessObject.GetArticleJournalsAsync().ConfigureAwait(false);
            if (journals == null || !journals.Any())
            {
                return new IArticleJournalModel[] { };
            }

            return journals.Select(this.mapper.Map<IArticleJournalDataModel, ArticleJournalModel>).ToArray();
        }
    }
}

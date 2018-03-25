// <copyright file="MongoArticlesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Documents.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Articles;
    using ProcessingTools.Data.Models.Documents.Mongo;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Documents.Articles;

    /// <summary>
    /// MongoDB implementation of <see cref="IArticlesDataAccessObject"/>.
    /// </summary>
    public class MongoArticlesDataAccessObject : MongoDataAccessObjectBase<Article>, IArticlesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoArticlesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoArticlesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IArticleInsertModel, Article>();
                c.CreateMap<IArticleUpdateModel, Article>();
            });

            this.mapper = mapperConfiguration.CreateMapper();

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W)
            };
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(a => a.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IArticleDataModel> GetById(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var article = await this.Collection.Find(a => a.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return article;
        }

        /// <inheritdoc/>
        public async Task<IArticleDetailsDataModel> GetDetailsById(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var article = await this.Collection.Find(a => a.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            if (article != null)
            {
                var journalId = article.JournalId.ToNewGuid();

                article.Journal = await this.GetArticleJournalsQuery(j => j.ObjectId == journalId).FirstOrDefaultAsync().ConfigureAwait(false);
            }

            return article;
        }

        /// <inheritdoc/>
        public async Task<IArticleJournalDataModel[]> GetArticleJournalsAsync()
        {
            var journals = await this.GetArticleJournalsQuery(j => true).ToListAsync().ConfigureAwait(false);

            return journals.ToArray<IArticleJournalDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IArticleDataModel> InsertAsync(IArticleInsertModel model)
        {
            if (model == null)
            {
                return null;
            }

            var article = this.mapper.Map<IArticleInsertModel, Article>(model);
            article.ObjectId = this.applicationContext.GuidProvider.Invoke();
            article.ModifiedBy = this.applicationContext.UserContext.UserId;
            article.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            article.CreatedBy = article.ModifiedBy;
            article.CreatedOn = article.ModifiedOn;
            article.Id = null;

            await this.Collection.InsertOneAsync(article, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return article;
        }

        /// <inheritdoc/>
        public async Task<IArticleDataModel[]> SelectAsync(int skip, int take)
        {
            var articles = await this.Collection.Find(a => true)
                .SortByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (articles == null || !articles.Any())
            {
                return new IArticleDataModel[] { };
            }

            return articles.ToArray<IArticleDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IArticleDetailsDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var articles = await this.Collection.Find(a => true)
                .SortByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (articles == null || !articles.Any())
            {
                return new IArticleDetailsDataModel[] { };
            }

            var journals = await this.GetArticleJournalsAsync().ConfigureAwait(false);

            if (journals != null && journals.Any())
            {
                articles.ForEach(a =>
                {
                    a.Journal = journals.FirstOrDefault(j => j.Id == a.JournalId);
                });
            }

            return articles.ToArray<IArticleDetailsDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(a => true);
        }

        /// <inheritdoc/>
        public async Task<IArticleDataModel> UpdateAsync(IArticleUpdateModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var article = this.mapper.Map<IArticleUpdateModel, Article>(model);
            article.ModifiedBy = this.applicationContext.UserContext.UserId;
            article.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<Article>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<Article>()
                .Set(a => a.Title, model.Title)
                .Set(a => a.SubTitle, model.SubTitle)
                .Set(a => a.JournalId, model.JournalId)
                .Set(a => a.PublishedOn, model.PublishedOn)
                .Set(a => a.AcceptedOn, model.AcceptedOn)
                .Set(a => a.ReceivedOn, model.ReceivedOn)
                .Set(a => a.VolumeSeries, model.VolumeSeries)
                .Set(a => a.Volume, model.Volume)
                .Set(a => a.Issue, model.Issue)
                .Set(a => a.IssuePart, model.IssuePart)
                .Set(a => a.ELocationId, model.ELocationId)
                .Set(a => a.FirstPage, model.FirstPage)
                .Set(a => a.LastPage, model.LastPage)
                .Set(a => a.NumberOfPages, model.NumberOfPages)
                .Set(a => a.ModifiedBy, article.ModifiedBy)
                .Set(a => a.ModifiedOn, article.ModifiedOn);
            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return article;
        }

        private IFindFluent<Journal, ArticleJournal> GetArticleJournalsQuery(System.Linq.Expressions.Expression<Func<Journal, bool>> filter)
        {
            return this.GetCollection<Journal>().Find(filter)
                .Project(j => new ArticleJournal
                {
                    Id = j.ObjectId.ToString(),
                    Name = j.Name,
                    AbbreviatedName = j.AbbreviatedName
                });
        }
    }
}

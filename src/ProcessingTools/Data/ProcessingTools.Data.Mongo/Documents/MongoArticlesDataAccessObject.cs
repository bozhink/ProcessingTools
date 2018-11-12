// <copyright file="MongoArticlesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common;
    using Common.Contracts;
    using Contracts.Documents;
    using Extensions;
    using Models.Contracts.Documents.Articles;
    using Models.Mongo.Documents;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts;
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
                c.CreateMap<Journal, ArticleJournal>()
                    .ForMember(aj => aj.Id, o => o.ResolveUsing(j => j.ObjectId.ToString()));
                c.CreateMap<Journal, IArticleJournalDataModel>().As<ArticleJournal>();
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

            long numberOfDocuments = await this.GetCollection<Document>().CountDocumentsAsync(j => j.ArticleId == id.ToString()).ConfigureAwait(false);
            if (numberOfDocuments > 0L)
            {
                throw new DeleteUnsuccessfulException("Article will not be deleted because it contains documents.");
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
        public async Task<IArticleDataModel> GetByIdAsync(object id)
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
        public async Task<IArticleDetailsDataModel> GetDetailsByIdAsync(object id)
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

                if (article.DbJournal != null)
                {
                    article.Journal = this.mapper.Map<Journal, IArticleJournalDataModel>(article.DbJournal);
                }

                if (article.Journal == null)
                {
                    article.Journal = await this.GetArticleJournalsQuery(j => j.ObjectId == journalId).FirstOrDefaultAsync().ConfigureAwait(false);
                }
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
            article.IsFinalized = false;
            article.IsDeployed = false;
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
                return Array.Empty<IArticleDataModel>();
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
                return Array.Empty<IArticleDetailsDataModel>();
            }

            var journals = await this.GetArticleJournalsAsync().ConfigureAwait(false);

            if (journals != null && journals.Any())
            {
                foreach (var article in articles)
                {
                    if (article.DbJournal != null)
                    {
                        article.Journal = this.mapper.Map<Journal, IArticleJournalDataModel>(article.DbJournal);
                    }

                    if (article.Journal == null)
                    {
                        article.Journal = journals.FirstOrDefault(j => j.Id == article.JournalId);
                    }
                }
            }

            return articles.ToArray<IArticleDetailsDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(a => true);
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
                .Set(m => m.ArticleId, model.ArticleId)
                .Set(m => m.Title, model.Title)
                .Set(m => m.SubTitle, model.SubTitle)
                .Set(m => m.Doi, model.Doi)
                .Set(m => m.JournalId, model.JournalId)
                .Set(m => m.PublishedOn, model.PublishedOn)
                .Set(m => m.ArchivedOn, model.ArchivedOn)
                .Set(m => m.AcceptedOn, model.AcceptedOn)
                .Set(m => m.ReceivedOn, model.ReceivedOn)
                .Set(m => m.VolumeSeries, model.VolumeSeries)
                .Set(m => m.Volume, model.Volume)
                .Set(m => m.Issue, model.Issue)
                .Set(m => m.IssuePart, model.IssuePart)
                .Set(m => m.ELocationId, model.ELocationId)
                .Set(m => m.FirstPage, model.FirstPage)
                .Set(m => m.LastPage, model.LastPage)
                .Set(m => m.NumberOfPages, model.NumberOfPages)
                .Set(m => m.ModifiedBy, article.ModifiedBy)
                .Set(m => m.ModifiedOn, article.ModifiedOn);
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

        /// <inheritdoc/>
        public async Task<object> GetJournalStyleIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var query = this.Collection.Aggregate()
                .Match(a => a.ObjectId == objectId)
                .Lookup<Journal, ArticleJournalAggregation>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<Journal>(),
                    localField: nameof(Article.JournalId),
                    foreignField: nameof(Journal.ObjectId),
                    @as: nameof(ArticleJournalAggregation.Journals))
                .Project(a => new JournalsProjectAggregation { Journals = a.Journals })
                .Unwind(a => a.Journals)
                .As<JournalsUnwindAggregation>()
                .Project(j => new { j.Journals.JournalStyleId })
                .Skip(0)
                .Limit(1);

            var result = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return result?.JournalStyleId;
        }

        /// <inheritdoc/>
        public async Task<IArticleDataModel> FinalizeAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string articleId = id.ToString();

            var finalDocumentId = await this.GetCollection<Document>()
                .Find(d => d.ArticleId == articleId && d.IsFinal)
                .Project(d => d.ObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (finalDocumentId == Guid.Empty)
            {
                throw new InvalidOperationException("Specified article does not have any final document");
            }

            Guid articleObjectId = id.ToNewGuid();
            var article = await this.Collection
                .Find(a => a.ObjectId == articleObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (article == null || string.IsNullOrWhiteSpace(article.JournalId))
            {
                throw new InvalidOperationException("Specified article does not have valid Journal ID");
            }

            Guid journalObjectId = article.JournalId.ToNewGuid();
            var journal = await this.GetCollection<Journal>()
                .Find(j => j.ObjectId == journalObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (journal == null)
            {
                throw new InvalidOperationException("Specified journal is null.");
            }

            Guid publisherObjectId = journal.PublisherId.ToNewGuid();
            var publisher = await this.GetCollection<Publisher>()
                .Find(p => p.ObjectId == publisherObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            journal.DbPublisher = publisher ?? throw new InvalidOperationException("Specified publisher is null");

            var filterDefinition = new FilterDefinitionBuilder<Article>().Eq(m => m.ObjectId, articleObjectId);
            var updateDefinition = new UpdateDefinitionBuilder<Article>()
                .Set(m => m.IsFinalized, true)
                .Set(m => m.FinalDocumentId, finalDocumentId.ToString())
                .Set(m => m.DbJournal, journal)
                .Set(m => m.ModifiedBy, this.applicationContext.UserContext.UserId)
                .Set(m => m.ModifiedOn, this.applicationContext.DateTimeProvider.Invoke());
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

            article.DbJournal = journal;

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

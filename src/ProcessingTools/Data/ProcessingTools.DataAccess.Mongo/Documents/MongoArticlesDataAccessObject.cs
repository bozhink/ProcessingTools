// <copyright file="MongoArticlesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Resources;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Articles;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Documents.Articles;
    using ProcessingTools.Data.Models.Mongo.Documents;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

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
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoArticlesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
            };
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            long numberOfDocuments = await this.GetCollection<Document>().CountDocumentsAsync(j => j.ArticleId == id.ToString()).ConfigureAwait(false);
            if (numberOfDocuments > 0L)
            {
                throw new DeleteUnsuccessfulException(StringResources.ArticleWillNotBeDeletedBecauseItContainsDocuments);
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
        public async Task<IArticleDataTransferObject> GetByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var article = await this.Collection.Find(a => a.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return article;
        }

        /// <inheritdoc/>
        public async Task<IArticleDetailsDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id is null)
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
                    article.Journal = this.mapper.Map<Journal, IArticleJournalDataTransferObject>(article.DbJournal);
                }

                if (article.Journal is null)
                {
                    article.Journal = await this.GetArticleJournalsQuery(j => j.ObjectId == journalId).FirstOrDefaultAsync().ConfigureAwait(false);
                }
            }

            return article;
        }

        /// <inheritdoc/>
        public async Task<IArticleJournalDataTransferObject[]> GetArticleJournalsAsync()
        {
            var journals = await this.GetArticleJournalsQuery(j => true).ToListAsync().ConfigureAwait(false);

            return journals.ToArray<IArticleJournalDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IArticleDataTransferObject> InsertAsync(IArticleInsertModel model)
        {
            if (model is null)
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
        public async Task<IList<IArticleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var articles = await this.Collection.Find(a => true)
                .SortByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (articles is null || !articles.Any())
            {
                return Array.Empty<IArticleDataTransferObject>();
            }

            return articles.ToArray<IArticleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IArticleDetailsDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var articles = await this.Collection.Find(a => true)
                .SortByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (articles is null || !articles.Any())
            {
                return Array.Empty<IArticleDetailsDataTransferObject>();
            }

            var journals = await this.GetArticleJournalsAsync().ConfigureAwait(false);

            if (journals != null && journals.Any())
            {
                foreach (var article in articles)
                {
                    if (article.DbJournal != null)
                    {
                        article.Journal = this.mapper.Map<Journal, IArticleJournalDataTransferObject>(article.DbJournal);
                    }

                    if (article.Journal is null)
                    {
                        article.Journal = journals.FirstOrDefault(j => j.Id == article.JournalId);
                    }
                }
            }

            return articles.ToArray<IArticleDetailsDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(a => true);
        }

        /// <inheritdoc/>
        public async Task<IArticleDataTransferObject> UpdateAsync(IArticleUpdateModel model)
        {
            if (model is null)
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
                IsUpsert = false,
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
            if (id is null)
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
        public Task<IArticleDataTransferObject> FinalizeAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.FinalizeInternalAsync(id);
        }

        private async Task<IArticleDataTransferObject> FinalizeInternalAsync(object id)
        {
            string articleId = id.ToString();

            var finalDocumentId = await this.GetCollection<Document>()
                .Find(d => d.ArticleId == articleId && d.IsFinal)
                .Project(d => d.ObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (finalDocumentId == Guid.Empty)
            {
                throw new InvalidOperationException(StringResources.SpecifiedArticleDoesNotHaveAnyFinalDocuments);
            }

            Guid articleObjectId = id.ToNewGuid();
            var article = await this.Collection
                .Find(a => a.ObjectId == articleObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (article is null || string.IsNullOrWhiteSpace(article.JournalId))
            {
                throw new InvalidOperationException(StringResources.SpecifiedArticleDoesNotHaveValidJournalId);
            }

            Guid journalObjectId = article.JournalId.ToNewGuid();
            var journal = await this.GetCollection<Journal>()
                .Find(j => j.ObjectId == journalObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (journal is null)
            {
                throw new InvalidOperationException(StringResources.SpecifiedJournalIsNull);
            }

            Guid publisherObjectId = journal.PublisherId.ToNewGuid();
            var publisher = await this.GetCollection<Publisher>()
                .Find(p => p.ObjectId == publisherObjectId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            journal.DbPublisher = publisher ?? throw new InvalidOperationException(StringResources.SpecifiedPublisherIsNull);

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
                IsUpsert = false,
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
                    AbbreviatedName = j.AbbreviatedName,
                });
        }
    }
}

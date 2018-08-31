// <copyright file="MongoJournalsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Data.Models.Contracts.Documents.Journals;
    using ProcessingTools.Data.Models.Documents.Mongo;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Documents.Journals;

    /// <summary>
    /// MongoDB implementation of <see cref="IJournalsDataAccessObject"/>.
    /// </summary>
    public class MongoJournalsDataAccessObject : MongoDataAccessObjectBase<Journal>, IJournalsDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoJournalsDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoJournalsDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IJournalInsertModel, Journal>();
                c.CreateMap<IJournalUpdateModel, Journal>();
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

            long numberOfArticles = await this.GetCollection<Article>().CountDocumentsAsync(a => a.JournalId == id.ToString()).ConfigureAwait(false);
            if (numberOfArticles > 0L)
            {
                throw new DeleteUnsuccessfulException("Journal will not be deleted because it contains related articles.");
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(j => j.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var journal = await this.Collection.Find(j => j.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return journal;
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsDataModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var journal = await this.Collection.Find(j => j.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            if (journal != null)
            {
                var publisherId = journal.PublisherId.ToNewGuid();

                journal.Publisher = await this.GetJournalPublishersQuery(p => p.ObjectId == publisherId).FirstOrDefaultAsync().ConfigureAwait(false);

                journal.NumberOfArticles = await this.GetCollection<Article>().CountDocumentsAsync(a => a.JournalId == journal.ObjectId.ToString()).ConfigureAwait(false);
            }

            return journal;
        }

        /// <inheritdoc/>
        public async Task<IJournalPublisherDataModel[]> GetJournalPublishersAsync()
        {
            var publishers = await this.GetJournalPublishersQuery(p => true).ToListAsync().ConfigureAwait(false);

            return publishers.ToArray<IJournalPublisherDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel> InsertAsync(IJournalInsertModel model)
        {
            if (model == null)
            {
                return null;
            }

            var journal = this.mapper.Map<IJournalInsertModel, Journal>(model);
            journal.ObjectId = this.applicationContext.GuidProvider.Invoke();
            journal.ModifiedBy = this.applicationContext.UserContext.UserId;
            journal.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            journal.CreatedBy = journal.ModifiedBy;
            journal.CreatedOn = journal.ModifiedOn;
            journal.Id = null;

            await this.Collection.InsertOneAsync(journal, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return journal;
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel[]> SelectAsync(int skip, int take)
        {
            var journals = await this.Collection.Find(j => true)
                .SortBy(j => j.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (journals == null || !journals.Any())
            {
                return Array.Empty<IJournalDataModel>();
            }

            return journals.ToArray<IJournalDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var journals = await this.Collection.Find(j => true)
                .SortBy(j => j.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (journals == null || !journals.Any())
            {
                return Array.Empty<IJournalDetailsDataModel>();
            }

            var publishers = await this.GetJournalPublishersAsync().ConfigureAwait(false);

            if (publishers != null && publishers.Any())
            {
                journals.ForEach(j =>
                {
                    j.Publisher = publishers.FirstOrDefault(p => p.Id == j.PublisherId);
                });
            }

            var articles = this.GetCollection<Article>().AsQueryable()
                .GroupBy(a => a.JournalId)
                .Select(g => new { JournalId = g.Key, Count = g.LongCount() })
                .ToArray();

            if (articles != null && articles.Any())
            {
                journals.ForEach(j =>
                {
                    j.NumberOfArticles = articles.FirstOrDefault(a => a.JournalId == j.ObjectId.ToString())?.Count ?? 0L;
                });
            }

            return journals.ToArray<IJournalDetailsDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(j => true);
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel> UpdateAsync(IJournalUpdateModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var journal = this.mapper.Map<IJournalUpdateModel, Journal>(model);
            journal.ModifiedBy = this.applicationContext.UserContext.UserId;
            journal.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<Journal>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<Journal>()
                .Set(j => j.Name, model.Name)
                .Set(j => j.AbbreviatedName, model.AbbreviatedName)
                .Set(j => j.JournalId, model.JournalId)
                .Set(j => j.PrintIssn, model.PrintIssn)
                .Set(j => j.ElectronicIssn, model.ElectronicIssn)
                .Set(j => j.PublisherId, model.PublisherId)
                .Set(j => j.JournalStyleId, model.JournalStyleId)
                .Set(j => j.ModifiedBy, journal.ModifiedBy)
                .Set(j => j.ModifiedOn, journal.ModifiedOn);
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

            return journal;
        }

        private IFindFluent<Publisher, JournalPublisher> GetJournalPublishersQuery(System.Linq.Expressions.Expression<Func<Publisher, bool>> filter)
        {
            return this.GetCollection<Publisher>().Find(filter)
                .Project(p => new JournalPublisher
                {
                    Id = p.ObjectId.ToString(),
                    Name = p.Name,
                    AbbreviatedName = p.AbbreviatedName
                });
        }
    }
}

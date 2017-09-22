namespace ProcessingTools.Documents.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Data.Common.Extensions;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;
    using ProcessingTools.Documents.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Mongo.Models;

    public class MongoJournalMetaRepository : MongoRepository<JournalMeta>, IMongoJournalMetaRepository
    {
        public MongoJournalMetaRepository(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }

        public IQueryable<IJournalMeta> Query => this.Collection.AsQueryable().AsQueryable<IJournalMeta>();

        private Func<IJournalMeta, JournalMeta> MapContractToModel => m => new JournalMeta
        {
            AbbreviatedJournalTitle = m.AbbreviatedJournalTitle,
            ArchiveNamePattern = m.ArchiveNamePattern,
            FileNamePattern = m.FileNamePattern,
            IssnEPub = m.IssnEPub,
            IssnPPub = m.IssnPPub,
            JournalId = m.JournalId,
            JournalTitle = m.JournalTitle,
            PublisherName = m.PublisherName
        };

        public async Task<object> AddAsync(IJournalMeta entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapContractToModel(entity);
            await this.Collection.InsertOneAsync(dbmodel).ConfigureAwait(false);

            return dbmodel;
        }

        public virtual Task<long> CountAsync()
        {
            var result = this.Query.LongCount();
            return Task.FromResult(result);
        }

        public virtual Task<long> CountAsync(Expression<Func<IJournalMeta, bool>> filter)
        {
            var result = this.Query.LongCount(filter);
            return Task.FromResult(result);
        }

        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter).ConfigureAwait(false);
            return result;
        }

        public Task<IEnumerable<IJournalMeta>> FindAsync(Expression<Func<IJournalMeta, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.FromResult(this.Collection.AsQueryable().AsQueryable<IJournalMeta>().Where(filter).AsEnumerable());
        }

        public Task<IJournalMeta> FindFirstAsync(Expression<Func<IJournalMeta, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.FromResult(this.Collection.AsQueryable().AsQueryable<IJournalMeta>().FirstOrDefault(filter));
        }

        public async Task<IJournalMeta> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<object> UpdateAsync(IJournalMeta entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapContractToModel(entity);
            var id = dbmodel.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, dbmodel).ConfigureAwait(false);
            return result;
        }

        public async Task<object> UpdateAsync(object id, IUpdateExpression<IJournalMeta> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var updateQuery = this.ConvertUpdateExpressionToMongoUpdateQuery(updateExpression);
            var filter = this.GetFilterById(id);
            var result = await this.Collection.UpdateOneAsync(filter, updateQuery).ConfigureAwait(false);
            return result;
        }

        protected UpdateDefinition<JournalMeta> ConvertUpdateExpressionToMongoUpdateQuery(IUpdateExpression<IJournalMeta> updateExpression)
        {
            var updateCommands = updateExpression.UpdateCommands.ToArray();
            if (updateCommands.Length < 1)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var updateCommand = updateCommands[0];
            var updateQuery = Builders<JournalMeta>.Update
                .Set(updateCommand.FieldName, updateCommand.Value);
            for (int i = 1; i < updateCommands.Length; ++i)
            {
                updateCommand = updateCommands[i];
                updateQuery = updateQuery.Set(updateCommand.FieldName, updateCommand.Value);
            }

            return updateQuery;
        }
    }
}

namespace ProcessingTools.Documents.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using Models;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Data.Common.Extensions;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

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

        public async Task<object> Add(IJournalMeta entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapContractToModel(entity);
            await this.Collection.InsertOneAsync(dbmodel);

            return dbmodel;
        }

        public virtual Task<long> Count()
        {
            var result = this.Query.LongCount();
            return Task.FromResult(result);
        }

        public virtual Task<long> Count(Expression<Func<IJournalMeta, bool>> filter)
        {
            var result = this.Query.LongCount(filter);
            return Task.FromResult(result);
        }

        public async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
        }

        public Task<IEnumerable<IJournalMeta>> Find(Expression<Func<IJournalMeta, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.FromResult(this.Collection.AsQueryable().AsQueryable<IJournalMeta>().Where(filter).AsEnumerable());
        }

        public Task<IJournalMeta> FindFirst(Expression<Func<IJournalMeta, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.FromResult(this.Collection.AsQueryable().AsQueryable<IJournalMeta>().FirstOrDefault(filter));
        }

        public async Task<IJournalMeta> GetById(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<object> Update(IJournalMeta entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbmodel = this.MapContractToModel(entity);
            var id = dbmodel.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, dbmodel);
            return result;
        }

        public async Task<object> Update(object id, IUpdateExpression<IJournalMeta> update)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var updateQuery = this.ConvertUpdateExpressionToMongoUpdateQuery(update);
            var filter = this.GetFilterById(id);
            var result = await this.Collection.UpdateOneAsync(filter, updateQuery);
            return result;
        }

        protected UpdateDefinition<JournalMeta> ConvertUpdateExpressionToMongoUpdateQuery(IUpdateExpression<IJournalMeta> update)
        {
            var updateCommands = update.UpdateCommands.ToArray();
            if (updateCommands.Length < 1)
            {
                throw new ArgumentNullException(nameof(update));
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

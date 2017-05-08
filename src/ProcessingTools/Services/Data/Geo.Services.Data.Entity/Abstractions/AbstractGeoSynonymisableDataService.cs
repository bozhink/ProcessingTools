﻿namespace ProcessingTools.Geo.Services.Data.Entity.Abstractions
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;

    public abstract partial class AbstractGeoSynonymisableDataService<TEntity, TModel, TFilter, TSynonymEntity, TSynonymModel, TSynonymFilter> : IDataServiceAsync<TModel, TFilter>, ISynonymisableDataService<TSynonymModel, TSynonymFilter>
        where TEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ProcessingTools.Contracts.Data.Geo.Models.ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentifiable
        where TFilter : IFilter
        where TSynonymEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ProcessingTools.Contracts.Data.Geo.Models.ISynonym
        where TSynonymModel : class, ISynonym
        where TSynonymFilter : ISynonymFilter
    {
        private readonly IEnvironment environment;
        private readonly IGeoRepository<TEntity> repository;
        private readonly IGeoRepository<TSynonymEntity> synonymRepository;

        public AbstractGeoSynonymisableDataService(IGeoRepository<TEntity> repository, IGeoRepository<TSynonymEntity> synonymRepository, IEnvironment environment)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (synonymRepository == null)
            {
                throw new ArgumentNullException(nameof(synonymRepository));
            }

            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            this.repository = repository;
            this.synonymRepository = synonymRepository;
            this.environment = environment;
        }

        protected abstract IMapper Mapper { get; }

        protected IEnvironment Environment => this.environment;

        protected IGeoRepository<TEntity> Repository => this.repository;

        protected IGeoRepository<TSynonymEntity> SynonymRepository => this.synonymRepository;

        public async Task<object> AddSynonymsAsync(int modelId, params TSynonymModel[] synonyms)
        {
            if (synonyms == null || synonyms.Length < 1)
            {
                throw new ArgumentNullException(nameof(synonyms));
            }

            var entity = await this.repository.Queryable()
                .Include(e => e.Synonyms)
                .FirstOrDefaultAsync(e => e.Id == modelId);
            if (entity == null)
            {
                return null;
            }

            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;

            foreach (var synonym in synonyms)
            {
                var synonymEntity = this.Mapper.Map<TSynonymModel, TSynonymEntity>(synonym);
                synonymEntity.CreatedBy = user;
                synonymEntity.CreatedOn = now;
                synonymEntity.ModifiedBy = user;
                synonymEntity.ModifiedOn = now;

                entity.Synonyms.Add(synonymEntity);
            }

            entity.ModifiedBy = user;
            entity.ModifiedOn = now;
            this.repository.Update(entity);

            return entity.Synonyms.Count;
        }

        public virtual Task<object> DeleteAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            object id = model.Id;
            return this.DeleteAsync(id: id);
        }

        public virtual Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.repository.Delete(id: id);
            return Task.FromResult(id);
        }

        public virtual async Task<TModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.repository.Queryable()
                .Include(e => e.Synonyms)
                .FirstOrDefaultAsync(e => (object)e.Id == id);
            if (entity == null)
            {
                return null;
            }

            var model = this.Mapper.Map<TEntity, TModel>(entity);

            return model;
        }

        public virtual async Task<TSynonymModel> GetSynonymByIdAsync(int modelId, int id)
        {
            var entity = await this.repository.Queryable()
                .Where(e => e.Id == modelId)
                .SelectMany(e => e.Synonyms)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
            {
                return null;
            }

            var model = this.Mapper.Map<TSynonymEntity, TSynonymModel>(entity);

            return model;
        }

        public virtual async Task<object> InsertAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.Mapper.Map<TModel, TEntity>(model);
            var result = await this.InsertEntityAsync(entity);
            return result.Id;
        }

        public async Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds)
        {
            if (synonymIds == null || synonymIds.Length < 1)
            {
                throw new ArgumentNullException(nameof(synonymIds));
            }

            var entity = await this.repository.Queryable()
                .Include(e => e.Synonyms)
                .FirstOrDefaultAsync(e => e.Id == modelId);
            if (entity == null)
            {
                return null;
            }

            var ids = entity.Synonyms
                .Where(s => synonymIds.Contains(s.Id))
                .Select(s => s.Id)
                .Distinct()
                .ToArray();
            foreach (var id in ids)
            {
                this.synonymRepository.Delete(id: id);
            }

            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;
            entity.ModifiedBy = user;
            entity.ModifiedOn = now;
            this.repository.Update(entity);

            return ids.Length;
        }

        public virtual async Task<object> SaveChangesAsync()
        {
            return await this.repository.SaveChangesAsync();
        }

        public virtual async Task<TModel[]> SelectAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);

            var data = await query.ToListAsync();
            var result = data.Select(e => this.Mapper.Map<TEntity, TModel>(e)).ToArray();
            return result;
        }

        public virtual async Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            var query = this.SelectQuery(this.GetQuery(filter), skip, take, sortColumn, sortOrder);

            var data = await query.ToListAsync();
            var result = data.Select(e => this.Mapper.Map<TEntity, TModel>(e)).ToArray();
            return result;
        }

        public virtual async Task<long> SelectCountAsync(TFilter filter)
        {
            var query = this.GetQuery(filter);

            var result = await query.LongCountAsync();
            return result;
        }

        public virtual async Task<long> SelectSynonymCountAsync(int modelId, TSynonymFilter filter)
        {
            var query = this.GetQuery(modelId, filter);

            var result = await query.LongCountAsync();
            return result;
        }

        public virtual async Task<TSynonymModel[]> SelectSynonymsAsync(int modelId, TSynonymFilter filter)
        {
            var query = this.GetQuery(modelId, filter);

            var data = await query.ToListAsync();
            var result = data.Select(e => this.Mapper.Map<TSynonymEntity, TSynonymModel>(e)).ToArray();
            return result;
        }

        public virtual async Task<object> UpdateAsync(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.Mapper.Map<TModel, TEntity>(model);

            return await this.UpdateEntityAsync(entity);
        }

        public async Task<object> UpdateSynonymsAsync(int modelId, params TSynonymModel[] synonyms)
        {
            if (synonyms == null || synonyms.Length < 1)
            {
                throw new ArgumentNullException(nameof(synonyms));
            }

            var entity = await this.repository.Queryable()
                .Include(e => e.Synonyms)
                .FirstOrDefaultAsync(e => e.Id == modelId);
            if (entity == null || synonyms.Any(s => s.ParentId != modelId))
            {
                return null;
            }

            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;

            int count = 0;
            foreach (var synonym in synonyms)
            {
                var synonymEntity = this.Mapper.Map<TSynonymModel, TSynonymEntity>(synonym);
                synonymEntity.ModifiedBy = user;
                synonymEntity.ModifiedOn = now;

                var dbsynonymEntity = entity.Synonyms.FirstOrDefault(s => s.Id == synonymEntity.Id);
                if (dbsynonymEntity != null)
                {
                    this.Mapper.Map<TSynonymEntity, TSynonymEntity>(synonymEntity, dbsynonymEntity);
                    this.synonymRepository.Update(entity: dbsynonymEntity);
                    ++count;
                }
            }

            entity.ModifiedBy = user;
            entity.ModifiedOn = now;
            this.repository.Update(entity);

            return await Task.FromResult(count);
        }

        protected abstract IQueryable<TEntity> GetQuery(TFilter filter);

        protected virtual IQueryable<TSynonymEntity> GetQuery(int modelId, TSynonymFilter filter)
        {
            var query = this.repository.Queryable()
                .Where(e => e.Id == modelId)
                .SelectMany(e => e.Synonyms);

            if (filter != null)
            {
                query = query.Where(
                     c =>
                         (!filter.Id.HasValue || c.Id == filter.Id) &&
                         (string.IsNullOrEmpty(filter.Name) || c.Name.ToLower().Contains(filter.Name.ToLower())) &&
                         (!filter.LanguageCode.HasValue || c.LanguageCode == filter.LanguageCode));
            }

            return query;
        }

        protected async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;

            entity.CreatedBy = user;
            entity.CreatedOn = now;
            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            this.repository.Add(entity);

            return await Task.FromResult(entity);
        }

        protected async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            string user = this.environment.User.Id;
            var now = this.environment.DateTime.Now;

            entity.ModifiedBy = user;
            entity.ModifiedOn = now;

            var dbentity = this.repository.Get(entity.Id);
            if (dbentity == null)
            {
                return null;
            }

            this.Mapper.Map<TEntity, TEntity>(entity, dbentity);

            this.repository.Update(dbentity);

            return await Task.FromResult(dbentity);
        }
    }
}
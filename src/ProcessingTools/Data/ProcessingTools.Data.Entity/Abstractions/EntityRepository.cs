// <copyright file="EntityRepository.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class EntityRepository<TContext, TEntity> : IEntityCrudRepository<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        public EntityRepository(TContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.GetDbSet<TEntity>();
        }

        ~EntityRepository()
        {
            this.Dispose(false);
        }

        public virtual IQueryable<TEntity> Query => this.DbSet.AsQueryable();

        private TContext Context { get; }

        private DbSet<TEntity> DbSet { get; }

        public virtual async Task<object> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return await this.AddAsync(entity, this.DbSet).ConfigureAwait(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task<object> SaveChangesAsync() => await this.Context.SaveChangesAsync().ConfigureAwait(false);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }

        private Task<T> AddAsync<T>(T entity, DbSet<T> set)
                    where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            var entry = this.GetEntry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return Task.FromResult(entity);
            }
            else
            {
                set.Add(entity);
                return Task.FromResult(entity);
            }
        }

        private DbSet<T> GetDbSet<T>()
            where T : class
        {
            return this.Context.Set<T>();
        }

        private EntityEntry<T> GetEntry<T>(T entity)
            where T : class => this.Context.Entry(entity);
    }
}

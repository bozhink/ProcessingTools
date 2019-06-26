﻿namespace ProcessingTools.Data.Entity.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class EntityGenericRepository<TContext, TEntity> : EntityRepository<TContext, TEntity, TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        public EntityGenericRepository(IDbContextProvider<TContext> contextProvider)
            : base(contextProvider)
        {
        }

        protected override Func<TEntity, TEntity> MapEntityToDbModel => e => e;

        public override async Task<object> AddAsync(TEntity entity) => await this.AddAsync(entity, this.DbSet).ConfigureAwait(false);
    }
}

namespace ProcessingTools.Documents.Data.Common.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    public class DocumentsRepository<T> : IDocumentsRepository<T>, IDisposable
        where T : class
    {
        public DocumentsRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual Task<IQueryable<T>> All()
        {
            return Task.FromResult(this.DbSet.AsQueryable());
        }

        public virtual Task<T> Get(object id)
        {
            return Task.FromResult(this.DbSet.Find(id));
        }

        public virtual Task Add(T entity)
        {
            return Task.Run(() =>
            {
                var entry = this.Context.Entry(entity);
                if (entry.State != EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                }
                else
                {
                    this.DbSet.Add(entity);
                }
            });
        }

        public virtual Task Update(T entity)
        {
            return Task.Run(() =>
            {
                var entry = this.Context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    this.DbSet.Attach(entity);
                }

                entry.State = EntityState.Modified;
            });
        }

        public virtual Task Delete(T entity)
        {
            return Task.Run(() =>
            {
                var entry = this.Context.Entry(entity);
                if (entry.State != EntityState.Deleted)
                {
                    entry.State = EntityState.Deleted;
                }
                else
                {
                    this.DbSet.Attach(entity);
                    this.DbSet.Remove(entity);
                }
            });
        }

        public virtual async Task Delete(object id)
        {
            var entity = await this.Get(id);
            if (entity != null)
            {
                await this.Delete(entity);
            }
        }

        public Task<int> SaveChanges()
        {
            return this.Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }
    }
}
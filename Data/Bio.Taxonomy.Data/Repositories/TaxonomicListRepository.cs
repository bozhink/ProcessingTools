namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;

    public class TaxonomicListRepository<T> : ITaxonomicListRepository<T>
    {
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> All()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> All(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
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
                // There is nothing to be disposed.
            }
        }
    }
}

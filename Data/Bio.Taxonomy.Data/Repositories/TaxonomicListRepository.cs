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

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(object id)
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
    }
}

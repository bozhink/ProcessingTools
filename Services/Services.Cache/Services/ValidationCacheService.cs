namespace ProcessingTools.Services.Cache
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models.Contracts;

    // TODO: Needs implementation.
    public class ValidationCacheService : IValidationCacheService
    {
        public Task Add(string key, IValidationCacheServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<IValidationCacheServiceModel>> All(string key)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string key)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string key, IValidationCacheServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string key, int id)
        {
            throw new NotImplementedException();
        }

        public Task<IValidationCacheServiceModel> Get(string key, int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<IValidationCacheServiceModel>> Get(string key, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task Update(string key, IValidationCacheServiceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}

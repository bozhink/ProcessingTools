namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public class StringRepositoryDataServiceFactory : IDataService<string>, IDisposable
    {
        private readonly IGenericRepository<string> repository;

        public StringRepositoryDataServiceFactory(IGenericRepository<string> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public virtual async Task Add(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentNullException(nameof(model));
            }

            await this.repository.Add(model)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public virtual Task<IQueryable<string>> All()
        {
            return this.repository.All();
        }

        public virtual async Task Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await this.repository.Delete(id)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public virtual async Task Delete(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentNullException(nameof(model));
            }

            await this.repository.Delete(model)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public virtual Task<string> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.repository.Get(id);
        }

        public virtual Task<IQueryable<string>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return this.repository.All(skip, take);
        }

        public virtual async Task Update(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentNullException(nameof(model));
            }

            await this.repository.Update(model)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
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
                this.repository.TryDispose();
            }
        }
    }
}
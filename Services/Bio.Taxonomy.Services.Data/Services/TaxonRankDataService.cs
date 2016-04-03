namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions;

    public class TaxonRankDataService : ITaxonRankDataService, IDisposable
    {
        private readonly ITaxaRepository repository;

        public TaxonRankDataService(ITaxaRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task Add(TaxonRankServiceModel model)
        {
            var entity = this.MapServiceModelToDbModel(model);
            await this.repository.Add(entity)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public async Task<IQueryable<TaxonRankServiceModel>> All()
        {
            return (await this.repository.All())
                .SelectMany(e => this.MapDbModelToServiceModel(e));
        }

        public async Task Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await this.repository.Delete(id)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public async Task Delete(TaxonRankServiceModel model)
        {
            var entity = this.MapServiceModelToDbModel(model);
            await this.repository.Delete(entity)
                .ContinueWith(_ => this.repository.SaveChanges().Wait());
        }

        public async Task<TaxonRankServiceModel> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.repository.Get(id);
            return this.MapDbModelToServiceModel(entity).FirstOrDefault();
        }

        public async Task<IQueryable<TaxonRankServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return (await this.repository.All(skip, take))
                .SelectMany(e => this.MapDbModelToServiceModel(e));
        }

        public async Task Update(TaxonRankServiceModel model)
        {
            var entity = this.MapServiceModelToDbModel(model);
            await this.repository.Update(entity)
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

        private Taxon MapServiceModelToDbModel(TaxonRankServiceModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = new Taxon
            {
                Name = model.ScientificName,
                IsWhiteListed = model.IsWhiteListed,
                Ranks = new string[] { model.Rank }
            };

            return entity;
        }

        private IEnumerable<TaxonRankServiceModel> MapDbModelToServiceModel(Taxon entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var models = entity.Ranks.Select(rank => new TaxonRankServiceModel
            {
                IsWhiteListed = entity.IsWhiteListed,
                ScientificName = entity.Name,
                Rank = rank
            });

            return models;
        }
    }
}
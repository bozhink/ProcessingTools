namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class TaxonRankDataService : ITaxonRankDataService
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        private Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public TaxonRankDataService(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        private Func<ITaxonRankEntity, IEnumerable<ITaxonRank>> MapDbModelToServiceModel => t => t.Ranks.Select(r => new TaxonRankServiceModel
        {
            ScientificName = t.Name,
            Rank = r
        });

        private Func<ITaxonRank, ITaxonRankEntity> MapServiceModelToDbModel => t =>
        {
            var taxon = new TaxonRankEntity
            {
                Name = t.ScientificName,
                IsWhiteListed = !this.matchNonWhiteListedHigherTaxon.IsMatch(t.ScientificName)
            };

            taxon.Ranks.Add(t.Rank);

            return taxon;
        };

        public virtual async Task<object> Add(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Add(t))
                    .ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChanges();
                return result;
            });
        }

        public virtual async Task<object> Delete(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Delete(t.Name))
                    .ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChanges();
                return result;
            });
        }

        public virtual async Task<IEnumerable<ITaxonRank>> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var query = await repository.Find(t => t.Name == name);

                var result = query.ToList()
                    .SelectMany(this.MapDbModelToServiceModel)
                    .ToList();

                return result;
            });
        }

        public virtual async Task<IEnumerable<ITaxonRank>> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var query = await repository.Find(t => t.Name.ToLower().Contains(name.ToLower()));

                var result = query.ToList()
                    .SelectMany(this.MapDbModelToServiceModel)
                    .ToList();

                return result;
            });
        }

        public virtual async Task<object> Update(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Update(t))
                    .ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChanges();

                return result;
            });
        }

        private ITaxonRank[] ValidateTaxa(ITaxonRank[] taxa)
        {
            if (taxa == null || taxa.Length < 1)
            {
                throw new ArgumentNullException(nameof(taxa));
            }

            var validTaxa = taxa.Where(t => t != null).ToArray();

            if (validTaxa.Length < 1)
            {
                throw new ArgumentNullException(nameof(taxa));
            }

            return validTaxa;
        }
    }
}

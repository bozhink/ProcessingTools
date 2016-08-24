namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using DataModels.Publishers;
    using Models.Publishers;
    using Models.Publishers.Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Expressions;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public class PublishersDataService : IPublishersDataService
    {
        // TODO: Change to IPublishersRepositoryProvider
        private readonly IEntityPublishersRepositoryProvider repositoryProvider;

        public PublishersDataService(IEntityPublishersRepositoryProvider repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<object> Add(object userId, PublisherUpdateServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var modifiedByUser = userId.ToString();
            var entity = new PublisherEntity
            {
                Name = model.Name,
                AbbreviatedName = model.AbbreviatedName,
                CreatedByUser = modifiedByUser,
                ModifiedByUser = modifiedByUser
            };

            foreach (var address in model.Addresses)
            {
                entity.Addresses.Add(new AddressEntity
                {
                    Id = address.Id,
                    AddressString = address.AddressString,
                    CityId = address.CityId,
                    CountryId = address.CountryId,
                    CreatedByUser = modifiedByUser,
                    ModifiedByUser = modifiedByUser
                });
            }

            var repository = this.repositoryProvider.Create();

            await repository.Add(entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public async Task<IEnumerable<IPublisherListableServiceModel>> All()
        {
            var repository = this.repositoryProvider.Create();

            var result = (await repository.All())
                .Select(e => new PublisherListableServiceModel
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToList();

            repository.TryDispose();

            return result;
        }

        public async Task<IQueryable<PublisherServiceModel>> All(int pageNumber, int itemsPerPage)
        {
            if (pageNumber < 0)
            {
                throw new InvalidPageNumberException();
            }

            if (1 > itemsPerPage || itemsPerPage > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var repository = this.repositoryProvider.Create();

            var models = (await repository.All())
                .OrderByDescending(d => d.DateModified)
                .Skip(pageNumber * itemsPerPage)
                .Take(itemsPerPage)
                .Select(e => new PublisherServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    AbbreviatedName = e.AbbreviatedName,
                    CreatedByUser = e.CreatedByUser,
                    ModifiedByUser = e.ModifiedByUser,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                })
                .ToList();

            repository.TryDispose();

            return models.AsQueryable();
        }

        public async Task<long> Count()
        {
            var repository = this.repositoryProvider.Create();

            var count = await repository.Count();

            repository.TryDispose();

            return count;
        }

        public async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.repositoryProvider.Create();

            await repository.Delete(id);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public async Task<PublisherServiceModel> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.repositoryProvider.Create();

            var result = await repository.FindFirst(
                e => e.Id.ToString() == id.ToString(),
                e => new PublisherServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    AbbreviatedName = e.AbbreviatedName,
                    CreatedByUser = e.CreatedByUser,
                    ModifiedByUser = e.ModifiedByUser,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                });

            repository.TryDispose();

            if (result == null)
            {
                throw new EntityNotFoundException();
            }

            return result;
        }

        public async Task<PublisherDetailsServiceModel> GetDetails(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await repository.Get(id);
            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            var result = new PublisherDetailsServiceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                AbbreviatedName = entity.AbbreviatedName,
                CreatedByUser = entity.CreatedByUser,
                ModifiedByUser = entity.ModifiedByUser,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified
            };

            foreach (var address in entity.Addresses)
            {
                result.Addresses.Add(new AddressServiceModel
                {
                    Id = address.Id,
                    AddressString = address.AddressString,
                    CityId = address.CityId,
                    CountryId = address.CountryId
                });
            }

            repository.TryDispose();

            return result;
        }

        public async Task<object> Update(object userId, IPublisherUpdatableServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var repository = this.repositoryProvider.Create();

            await repository.Update(
                model.Id,
                ExpressionBuilder<IPublisherEntity>
                    .UpdateExpression
                        .Set(e => e.Name, model.Name)
                        .Set(e => e.AbbreviatedName, model.AbbreviatedName)
                        .Set(e => e.ModifiedByUser, userId.ToString())
                        .Set(e => e.DateModified, DateTime.UtcNow));
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }
    }
}

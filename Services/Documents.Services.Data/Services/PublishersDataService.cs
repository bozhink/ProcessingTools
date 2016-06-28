namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Publishers;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public class PublishersDataService : IPublishersDataService
    {
        private readonly IDocumentsRepositoryProvider<Publisher> repositoryProvider;

        public PublishersDataService(IDocumentsRepositoryProvider<Publisher> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<object> Add(object userId, PublisherMinimalServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = new Publisher
            {
                Name = model.Name,
                AbbreviatedName = model.AbbreviatedName,
                CreatedByUser = userId.ToString(),
                ModifiedByUser = userId.ToString()
            };

            var repository = this.repositoryProvider.Create();

            await repository.Add(entity);
            var result = await repository.SaveChanges();

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

            var publishers = (await repository.All())
                .OrderByDescending(d => d.DateModified)
                .Skip(pageNumber * itemsPerPage)
                .Take(itemsPerPage)
                .Select(p => new PublisherServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AbbreviatedName = p.AbbreviatedName,
                    CreatedByUser = p.CreatedByUser,
                    ModifiedByUser = p.ModifiedByUser,
                    DateCreated = p.DateCreated,
                    DateModified = p.DateModified
                })
                .ToList();

            repository.TryDispose();

            return publishers.AsQueryable();
        }

        public async Task<PublisherServiceModel> Get(object id)
        {
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var repository = this.repositoryProvider.Create();

                var entity = await repository.Get(id);

                repository.TryDispose();

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                var result = new PublisherServiceModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    AbbreviatedName = entity.AbbreviatedName,
                    CreatedByUser = entity.CreatedByUser,
                    ModifiedByUser = entity.ModifiedByUser,
                    DateCreated = entity.DateCreated,
                    DateModified = entity.DateModified
                };

                return result;
            }
        }

        public async Task<PublisherDetailsServiceModel> GetDetails(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.repositoryProvider.Create();

            var entity = (await repository.All())
                .FirstOrDefault(p => p.Id.ToString() == id.ToString());

            repository.TryDispose();

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

            result.Addresses = entity.Addresses?.Select(a => new AddressServiceModel
            {
                Id = a.Id,
                AddressString = a.AddressString
            }).ToList();

            result.Journals = entity.Journals?.Select(j => new JournalServiceModel
            {
                Id = j.Id,
                Name = j.Name
            }).ToList();

            return result;
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

        public async Task<object> Update(object userId, PublisherMinimalServiceModel model)
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

            var entity = await repository.Get(model.Id);
            if (entity == null)
            {
                repository.TryDispose();
                throw new EntityNotFoundException();
            }

            entity.Name = model.Name;
            entity.AbbreviatedName = model.AbbreviatedName;
            entity.ModifiedByUser = userId.ToString();
            entity.DateModified = DateTime.UtcNow;

            await repository.Update(entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }
    }
}

namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Publishers;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Common.Factories;

    public class PublishersDataService : MvcDataServiceFactory<PublisherMinimalServiceModel, PublisherServiceModel, PublisherDetailsServiceModel, Publisher>, IPublishersDataService
    {
        public PublishersDataService(IDocumentsRepositoryProvider<Publisher> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Publisher, PublisherServiceModel>> MapDbModelToServiceModel => p => new PublisherServiceModel
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUser = p.CreatedByUser,
            ModifiedByUser = p.ModifiedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified
        };


        public override async Task<object> Add(object userId, PublisherMinimalServiceModel model)
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

            var repository = this.RepositoryProvider.Create();

            await repository.Add(entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public async Task<IEnumerable<PublisherSimpleServiceModel>> All()
        {
            var repository = this.RepositoryProvider.Create();

            var result = (await repository.All())
                .Select(p => new PublisherSimpleServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

            repository.TryDispose();

            return result;
        }

        public override async Task<PublisherDetailsServiceModel> GetDetails(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.RepositoryProvider.Create();

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

        public override async Task<object> Update(object userId, PublisherMinimalServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var repository = this.RepositoryProvider.Create();

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

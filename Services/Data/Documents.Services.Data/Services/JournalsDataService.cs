namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Journals;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Data.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Common.Factories;

    public class JournalsDataService : MvcDataServiceFactory<JournalMinimalServiceModel, JournalServiceModel, JournalDetailsServiceModel, Journal>, IJournalsDataService
    {
        public JournalsDataService(IDocumentsRepositoryProvider<Journal> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Journal, JournalServiceModel>> MapDbModelToServiceModel => j => new JournalServiceModel
        {
            Id = j.Id,
            Name = j.Name,
            AbbreviatedName = j.AbbreviatedName,
            JournalId = j.JournalId,
            PrintIssn = j.PrintIssn,
            ElectronicIssn = j.ElectronicIssn,
            CreatedByUser = j.CreatedByUser,
            ModifiedByUser = j.ModifiedByUser,
            DateCreated = j.DateCreated,
            DateModified = j.DateModified,
            Publisher = new PublisherServiceModel
            {
                Id = j.Publisher.Id,
                Name = j.Publisher.Name,
                AbbreviatedName = j.Publisher.AbbreviatedName
            }
        };

        public override async Task<object> Add(object userId, JournalMinimalServiceModel model)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = new Journal
            {
                Name = model.Name,
                AbbreviatedName = model.AbbreviatedName,
                JournalId = model.JournalId,
                ElectronicIssn = model.ElectronicIssn,
                PrintIssn = model.PrintIssn,
                PublisherId = model.Publisher.Id,
                CreatedByUser = userId.ToString(),
                ModifiedByUser = userId.ToString()
            };

            var repository = this.RepositoryProvider.Create();

            await repository.Add(entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public override async Task<JournalDetailsServiceModel> GetDetails(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var repository = this.RepositoryProvider.Create();

            var entity = await repository.FindFirst(e => e.Id.ToString() == id.ToString());
            if (entity == null)
            {
                repository.TryDispose();
                throw new EntityNotFoundException();
            }

            var result = new JournalDetailsServiceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                AbbreviatedName = entity.AbbreviatedName,
                JournalId = entity.JournalId,
                PrintIssn = entity.PrintIssn,
                ElectronicIssn = entity.ElectronicIssn,
                CreatedByUser = entity.CreatedByUser,
                ModifiedByUser = entity.ModifiedByUser,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
                Publisher = new PublisherServiceModel
                {
                    Id = entity.Publisher.Id,
                    Name = entity.Publisher.Name,
                    AbbreviatedName = entity.Publisher.AbbreviatedName
                }
            };

            result.Articles = entity.Articles?.Select(a => new ArticleServiceModel
            {
                Id = a.Id,
                Title = a.Title
            }).ToList();

            repository.TryDispose();

            return result;
        }

        public override async Task<object> Update(object userId, JournalMinimalServiceModel model)
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
            entity.ElectronicIssn = model.ElectronicIssn;
            entity.JournalId = model.JournalId;
            entity.PrintIssn = model.PrintIssn;
            entity.PublisherId = model.Publisher.Id;

            entity.ModifiedByUser = userId.ToString();
            entity.DateModified = DateTime.UtcNow;

            await repository.Update(entity);
            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }
    }
}

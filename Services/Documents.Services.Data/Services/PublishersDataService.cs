namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Documents.Data.Common.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Services.Common.Factories;

    public class PublishersDataService : GenericRepositoryDataServiceFactory<Publisher, PublisherServiceModel>, IPublishersDataService
    {
        public PublishersDataService(IDocumentsRepository<Publisher> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Publisher, IEnumerable<PublisherServiceModel>>> MapDbModelToServiceModel => p => new PublisherServiceModel[]
        {
            new PublisherServiceModel
            {
                AbbreviatedName = p.AbbreviatedName,
                CreatedByUserId = p.CreatedByUserId,
                DateCreated = p.DateCreated,
                DateModified = p.DateModified,
                Id = p.Id,
                ModifiedByUserId = p.ModifiedByUserId,
                Name = p.Name
            }
        };

        protected override Expression<Func<PublisherServiceModel, IEnumerable<Publisher>>> MapServiceModelToDbModel => p => new Publisher[]
        {
            new Publisher
            {
                Name = p.Name,
                ModifiedByUserId = p.ModifiedByUserId,
                Id = p.Id,
                DateModified = p.DateModified,
                DateCreated = p.DateCreated,
                CreatedByUserId = p.CreatedByUserId,
                AbbreviatedName = p.AbbreviatedName
            }
        };

        protected override Expression<Func<Publisher, object>> SortExpression => p => p.Name;
    }
}
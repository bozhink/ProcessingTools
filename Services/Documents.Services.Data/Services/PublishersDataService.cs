namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class PublishersDataService : SimpleDataServiceWithRepositoryProviderFactory<Publisher, PublisherServiceModel>, IPublishersDataService
    {
        public PublishersDataService(IDocumentsRepositoryProvider<Publisher> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Publisher, PublisherServiceModel>> MapDbModelToServiceModel => p => new PublisherServiceModel
        {
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUserId = p.CreatedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified,
            Id = p.Id,
            ModifiedByUserId = p.ModifiedByUser,
            Name = p.Name,
            Journals = p.Journals.Select(j => new JournalServiceModel
            {
                AbbreviatedName = j.AbbreviatedName,
                CreatedByUserId = j.CreatedByUser,
                DateCreated = j.DateCreated,
                DateModified = j.DateModified,
                ElectronicIssn = j.ElectronicIssn,
                Id = j.Id,
                JournalId = j.JournalId,
                ModifiedByUserId = j.ModifiedByUser,
                Name = j.Name,
                PrintIssn = j.PrintIssn,
                PublisherId = j.PublisherId
            })
        };

        protected override Expression<Func<PublisherServiceModel, Publisher>> MapServiceModelToDbModel => p => new Publisher
        {
            Name = p.Name,
            ModifiedByUser = p.ModifiedByUserId,
            Id = p.Id,
            DateModified = p.DateModified,
            DateCreated = p.DateCreated,
            CreatedByUser = p.CreatedByUserId,
            AbbreviatedName = p.AbbreviatedName
        };

        protected override Expression<Func<Publisher, object>> SortExpression => p => p.Name;
    }
}
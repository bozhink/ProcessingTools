﻿namespace ProcessingTools.Journals.Data.Entity.Repositories
{
    using System;
    using System.Linq;
    using ProcessingTools.Contracts.Data.Journals.Models;
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Entity.Abstractions.Repositories;
    using ProcessingTools.Journals.Data.Entity.Contracts;
    using ProcessingTools.Journals.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Entity.Models;

    public class EntityPublishersRepository : AbstractEntityAddressableRepository<IPublisher, Publisher>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IGenericRepository<IJournalsDbContext, Publisher> repository)
            : base(repository)
        {
        }

        protected override Func<IPublisher, Publisher> MapEntityToDbModel => p => new Publisher
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedByUser = p.CreatedByUser,
            ModifiedByUser = p.ModifiedByUser,
            DateCreated = p.DateCreated,
            DateModified = p.DateModified,
            Addresses = p.Addresses.Select(this.MapAddressToAddress).ToList()
        };
    }
}

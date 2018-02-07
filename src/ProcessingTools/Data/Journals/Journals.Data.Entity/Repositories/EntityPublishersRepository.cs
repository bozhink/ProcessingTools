﻿namespace ProcessingTools.Journals.Data.Entity.Repositories
{
    using System;
    using System.Linq;
    using ProcessingTools.Models.Contracts.Journals;
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Entity.Abstractions.Repositories;
    using ProcessingTools.Journals.Data.Entity.Contracts;
    using ProcessingTools.Journals.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Journals.Data.Entity.Models;

    public class EntityPublishersRepository : AbstractEntityAddressableRepository<IPublisher, Publisher>, IEntityPublishersRepository
    {
        public EntityPublishersRepository(IEfRepository<IJournalsDbContext, Publisher> repository)
            : base(repository)
        {
        }

        protected override Func<IPublisher, Publisher> MapEntityToDbModel => p => new Publisher
        {
            Id = p.Id,
            Name = p.Name,
            AbbreviatedName = p.AbbreviatedName,
            CreatedBy = p.CreatedBy,
            ModifiedBy = p.ModifiedBy,
            CreatedOn = p.CreatedOn,
            ModifiedOn = p.ModifiedOn,
            Addresses = p.Addresses.Select(this.MapAddressToAddress).ToList()
        };
    }
}

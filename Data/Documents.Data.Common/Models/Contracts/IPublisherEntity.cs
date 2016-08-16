namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IPublisherEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string Name { get; }

        string AbbreviatedName { get; }

        ICollection<IAddressEntity> Addresses { get; }

        ICollection<IJournalEntity> Journals { get; }
    }
}

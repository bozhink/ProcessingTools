namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IPublisherEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string Name { get; set; }

        string AbbreviatedName { get; set; }

        ICollection<IAddressEntity> Addresses { get; }

        ICollection<IJournalEntity> Journals { get; }
    }
}

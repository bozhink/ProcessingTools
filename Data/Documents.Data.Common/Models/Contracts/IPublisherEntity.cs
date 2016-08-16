namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IPublisherEntity : IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
        ICollection<IAddressEntity> Addresses { get; }

        ICollection<IJournalEntity> Journals { get; }
    }
}

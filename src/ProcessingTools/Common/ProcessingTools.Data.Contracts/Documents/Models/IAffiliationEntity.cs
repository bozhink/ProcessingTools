namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using System;
    using ProcessingTools.Models.Contracts;

    public interface IAffiliationEntity : INameableGuidIdentifiable, IModelWithUserInformation
    {
        Guid InstitutionId { get; }

        Guid AddressId { get; }
    }
}

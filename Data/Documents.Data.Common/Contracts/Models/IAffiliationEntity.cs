namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using System;
    using ProcessingTools.Contracts;

    public interface IAffiliationEntity : INameableGuidIdentifiable, IModelWithUserInformation
    {
        Guid InstitutionId { get; }

        Guid AddressId { get; }
    }
}

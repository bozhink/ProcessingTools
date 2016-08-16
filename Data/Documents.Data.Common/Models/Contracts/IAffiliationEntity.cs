namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts;

    public interface IAffiliationEntity : INameableGuidIdentifiable, IModelWithUserInformation
    {
        Guid InstitutionId { get; }

        Guid AddressId { get; }
    }
}

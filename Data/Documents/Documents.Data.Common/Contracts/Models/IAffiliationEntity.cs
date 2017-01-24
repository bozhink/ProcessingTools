namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using System;
    using ProcessingTools.Contracts.Models;

    public interface IAffiliationEntity : INameableGuidIdentifiable, IModelWithUserInformation
    {
        Guid InstitutionId { get; }

        Guid AddressId { get; }
    }
}

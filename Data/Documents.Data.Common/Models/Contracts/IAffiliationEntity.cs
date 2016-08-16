namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts;

    public interface IAffiliationEntity : INameableGuidIdentifiable, IModelWithUserInformation
    {
        string Name { get; }

        Guid InstitutionId { get; }

        Guid AddressId { get; }
    }
}

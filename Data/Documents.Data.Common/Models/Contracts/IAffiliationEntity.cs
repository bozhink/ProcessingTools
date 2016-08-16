namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts;

    public interface IAffiliationEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string Name { get; }

        Guid InstitutionId { get; }

        Guid AddressId { get; }
    }
}

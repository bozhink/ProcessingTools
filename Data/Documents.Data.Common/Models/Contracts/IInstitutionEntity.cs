namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IInstitutionEntity : IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
        ICollection<IAddressEntity> Addresses { get; }
    }
}

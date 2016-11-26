namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IAuthorEntity : IPerson, IGuidIdentifiable, IModelWithUserInformation
    {
        ICollection<IAffiliationEntity> Affiliations { get; }

        ICollection<IArticleEntity> Articles { get; }
    }
}

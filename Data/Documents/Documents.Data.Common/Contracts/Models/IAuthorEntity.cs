namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IAuthorEntity : IPerson, IGuidIdentifiable, IModelWithUserInformation
    {
        ICollection<IAffiliationEntity> Affiliations { get; }

        ICollection<IArticleEntity> Articles { get; }
    }
}

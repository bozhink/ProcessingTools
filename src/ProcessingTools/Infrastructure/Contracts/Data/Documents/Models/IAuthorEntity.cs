namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IAuthorEntity : IPerson, IGuidIdentifiable, IModelWithUserInformation
    {
        IEnumerable<IAffiliationEntity> Affiliations { get; }

        IEnumerable<IArticleEntity> Articles { get; }
    }
}

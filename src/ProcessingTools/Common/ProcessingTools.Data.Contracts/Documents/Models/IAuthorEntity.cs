namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IAuthorEntity : IPerson, IGuidIdentifiable, IModelWithUserInformation
    {
        IEnumerable<IAffiliationEntity> Affiliations { get; }

        IEnumerable<IArticleEntity> Articles { get; }
    }
}

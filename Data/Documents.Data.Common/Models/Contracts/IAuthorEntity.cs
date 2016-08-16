namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IAuthorEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string Surname { get; }

        string GivenNames { get; }

        string Prefix { get; }

        string Suffix { get; }

        ICollection<IAffiliationEntity> Affiliations { get; }

        ICollection<IArticleEntity> Articles { get; }
    }
}

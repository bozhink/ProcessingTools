namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Data.Common.File.Contracts.Repositories;

    public interface IXmlBiotaxonomicBlackListIterableRepository : IFileIterableRepository<IBlackListEntity>, IBiotaxonomicBlackListIterableRepository
    {
    }
}

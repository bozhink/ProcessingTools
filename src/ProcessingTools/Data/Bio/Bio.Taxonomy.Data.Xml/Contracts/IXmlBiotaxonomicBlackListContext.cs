namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Data.Common.File.Contracts;

    public interface IXmlBiotaxonomicBlackListContext : IFileDbContext<IBlackListEntity>
    {
    }
}

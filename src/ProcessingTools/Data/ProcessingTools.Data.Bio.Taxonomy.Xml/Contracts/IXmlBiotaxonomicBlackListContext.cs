namespace ProcessingTools.Data.Bio.Taxonomy.Xml.Contracts
{
    using ProcessingTools.Data.Common.File.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public interface IXmlBiotaxonomicBlackListContext : IFileDbContext<IBlackListItem>
    {
    }
}

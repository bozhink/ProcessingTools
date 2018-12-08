namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public interface IXmlBiotaxonomicBlackListContext : IFileDbContext<IBlackListItem>
    {
    }
}

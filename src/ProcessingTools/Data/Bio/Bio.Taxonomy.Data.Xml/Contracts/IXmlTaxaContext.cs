namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using ProcessingTools.Data.Common.File.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    public interface IXmlTaxaContext : IFileDbContext<ITaxonRankEntity>
    {
    }
}

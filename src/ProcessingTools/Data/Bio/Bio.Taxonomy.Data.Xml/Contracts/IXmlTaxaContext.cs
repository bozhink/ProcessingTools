namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Common.File.Contracts;

    public interface IXmlTaxaContext : IFileDbContext<ITaxonRankEntity>
    {
    }
}

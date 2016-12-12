namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Common.File.Contracts;

    public interface IXmlTaxaContextProvider : IDatabaseProvider<IXmlTaxaContext>, IFileDbContextProvider<IXmlTaxaContext, ITaxonRankEntity>
    {
    }
}

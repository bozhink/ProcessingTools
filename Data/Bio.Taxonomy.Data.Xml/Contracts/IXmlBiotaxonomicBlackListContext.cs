namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.File.Contracts;

    public interface IXmlBiotaxonomicBlackListContext : IFileDbContext<IBlackListEntity>
    {
        Task<long> WriteItemsToFile();
    }
}

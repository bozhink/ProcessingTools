namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;

    public interface IXmlBiotaxonomicBlackListContext
    {
        Task<object> Add(IBlackListEntity entity);

        Task<IQueryable<IBlackListEntity>> All();

        Task<object> Delete(IBlackListEntity entity);

        Task<long> WriteItemsToFile();
    }
}

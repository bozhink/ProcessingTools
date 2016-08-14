namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IXmlBiotaxonomicBlackListContext
    {
        Task<object> Add(string entity);

        Task<IQueryable<string>> All();

        Task<object> Delete(string entity);

        Task<long> WriteItemsToFile();
    }
}

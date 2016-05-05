namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;

    public interface ITaxaContext
    {
        Task<object> Add(Taxon taxon);

        Task<IQueryable<Taxon>> All();

        Task<object> Delete(object id);

        Task<Taxon> Get(object id);

        Task<int> LoadTaxa(string fileName);

        Task<object> Update(Taxon taxon);

        Task<int> WriteTaxa(string fileName);
    }
}

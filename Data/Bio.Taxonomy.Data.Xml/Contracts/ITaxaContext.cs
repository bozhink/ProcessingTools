namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;

    public interface ITaxaContext
    {
        Task<object> Add(ITaxonRankEntity taxon);

        Task<IQueryable<ITaxonRankEntity>> All();

        Task<object> Delete(object id);

        Task<ITaxonRankEntity> Get(object id);

        Task<long> LoadTaxa(string fileName);

        Task<object> Update(ITaxonRankEntity taxon);

        Task<long> WriteTaxa(string fileName);
    }
}

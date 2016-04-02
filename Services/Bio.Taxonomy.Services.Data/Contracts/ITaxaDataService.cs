namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITaxaDataService<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> Resolve(params string[] scientificNames);
    }
}
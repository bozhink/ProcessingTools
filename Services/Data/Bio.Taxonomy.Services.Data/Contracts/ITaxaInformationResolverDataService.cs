namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITaxaInformationResolverDataService<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> Resolve(params string[] scientificNames);
    }
}
namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITaxaInformationResolverDataService<T>
    {
        Task<IQueryable<T>> Resolve(params string[] scientificNames);
    }
}
namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaxaInformationResolverDataService<T>
    {
        Task<IEnumerable<T>> Resolve(params string[] scientificNames);
    }
}

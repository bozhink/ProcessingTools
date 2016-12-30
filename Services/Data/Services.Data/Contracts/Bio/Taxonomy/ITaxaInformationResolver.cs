namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaxaInformationResolver<T>
    {
        Task<IEnumerable<T>> Resolve(params string[] scientificNames);
    }
}

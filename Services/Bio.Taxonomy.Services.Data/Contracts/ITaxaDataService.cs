namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;

    public interface ITaxaDataService<T>
    {
        IQueryable<T> Resolve(params string[] scientificNames);
    }
}
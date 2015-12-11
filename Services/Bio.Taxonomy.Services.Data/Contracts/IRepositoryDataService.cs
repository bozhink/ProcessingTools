namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;

    public interface IRepositoryDataService<T>
    {
        IQueryable<T> All();
    }
}

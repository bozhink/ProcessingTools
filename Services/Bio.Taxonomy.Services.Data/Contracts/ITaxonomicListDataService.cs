namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;

    public interface ITaxonomicListDataService<T>
    {
        IQueryable<T> All();
    }
}

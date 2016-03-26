namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;

    public interface ITaxaDataService<TServiceModel>
    {
        IQueryable<TServiceModel> Resolve(params string[] scientificNames);
    }
}
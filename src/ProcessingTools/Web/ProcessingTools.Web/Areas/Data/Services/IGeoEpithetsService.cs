namespace ProcessingTools.Web.Areas.Data.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Areas.Data.Models.GeoEpithets;

    public interface IGeoEpithetsService
    {
        Task<GeoEpithetsIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage);

        Task InsertAsync(GeoEpithetsRequestModel model);

        Task UpdateAsync(GeoEpithetRequestModel model);

        Task DeleteAsync(int id);
    }
}

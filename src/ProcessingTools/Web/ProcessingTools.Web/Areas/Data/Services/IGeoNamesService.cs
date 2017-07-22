namespace ProcessingTools.Web.Areas.Data.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Areas.Data.Models.GeoNames;

    public interface IGeoNamesService
    {
        Task<GeoNamesIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage);

        Task InsertAsync(GeoNamesRequestModel model);

        Task UpdateAsync(GeoNameRequestModel model);

        Task DeleteAsync(int id);
    }
}

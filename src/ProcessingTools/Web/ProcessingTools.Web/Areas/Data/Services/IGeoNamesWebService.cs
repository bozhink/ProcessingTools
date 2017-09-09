namespace ProcessingTools.Web.Areas.Data.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Geo.GeoNames;

    public interface IGeoNamesWebService
    {
        Task<GeoNamesIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage);

        Task InsertAsync(GeoNamesRequestModel model);

        Task UpdateAsync(GeoNameRequestModel model);

        Task DeleteAsync(int id);
    }
}
